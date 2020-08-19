using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.Dto;
using MS.Modular.AccountManagement.Domain.Redis;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.BuildingBlocks.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.Domain.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _options;
        private const string _tokenKeyPattern = "sh_identity_tokenkey_";
        private readonly IRedisService _redisService;

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };

        public TokenService(IOptions<JwtOptions> options, IRedisService redisService)
        {
            _options = options.Value;
            _redisService = redisService;
        }

        public async Task<TokenInfo> GenerateTokenAsync(User user)
        {
            user.Password = string.Empty;
            user.HashSalt = string.Empty;
            var secretKey = Encoding.UTF8.GetBytes(_options.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512);

            var encryptionkey = Encoding.UTF8.GetBytes(_options.Encryptkey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey),
                                                                  SecurityAlgorithms.Aes128KW,
                                                                  SecurityAlgorithms.Aes128CbcHmacSha256);

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(AppConstant.ClaimAccount, JsonConvert.SerializeObject(user, _jsonSerializerSettings)),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, GetUserType(user.UserId))
            };
            var expires = now.AddMinutes(_options.ExpiryMinutes);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Audience = _options.ValidAudience,
                IssuedAt = DateTime.Now,
                NotBefore = now,
                Expires = expires,
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(jwtClaims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var encryptedJwt = tokenHandler.WriteToken(securityToken);

            var tokenInfo = new TokenInfo
            {
                AccessToken = encryptedJwt,
                ExpiredInMinute = _options.ExpiryMinutes,
                RefreshToken = GenerateRefreshToken(user.UserId)
            };
            var tokenKey = _tokenKeyPattern + tokenInfo.RefreshToken;
            await _redisService.SetAsync(tokenKey, tokenInfo);
            return tokenInfo;
        }

        public async Task<TokenInfo> RefreshTokenAsync(string refreshToken)
        {
            var tokenKey = _tokenKeyPattern + refreshToken;
            var tokenInfo = await _redisService.GetAsync<TokenInfo>(tokenKey);
            if (tokenInfo == null)
            {
                return default;
            }
            var accountInfo = GetAccountInfoByToken(tokenInfo.AccessToken);
            var result = await GenerateTokenAsync(accountInfo);
            await _redisService.RemoveAsync(tokenKey);
            return result;
        }

        public ReturnResponse<User> VerifyToken(string jwtToken)
        {
            var returnResponse = new ReturnResponse<User>();
            try
            {
                var tokenValidationParameters = GenerateTokenValidationParameters();

                var handler = new JwtSecurityTokenHandler();

                var calim = handler.ValidateToken(jwtToken, tokenValidationParameters, out SecurityToken validatedToken);
                string accountStr = calim.Claims.Where(c => c.Type.Equals(AppConstant.ClaimAccount)).Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(accountStr))
                {
                    returnResponse.Error = "Invalid Token";
                    returnResponse.Succeeded = false;
                }
                else
                {
                    returnResponse.Data = JsonConvert.DeserializeObject<User>(accountStr, _jsonSerializerSettings);
                    returnResponse.Succeeded = true;
                }
            }
            catch (Exception ex) when (ex is SecurityTokenException || ex is ArgumentException)
            {
                returnResponse.Error = ex.Message.ToString();
                returnResponse.Succeeded = false;
            }

            return returnResponse;
        }

        private User GetAccountInfoByToken(string accessToken)
        {
            var tokenValidationParameters = GenerateTokenValidationParameters();
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var calim = handler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken validatedToken);
                string accountStr = calim.Claims.Where(c => c.Type.Equals(AppConstant.ClaimAccount)).Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(accountStr))
                    return default;

                return JsonConvert.DeserializeObject<User>(accountStr);
            }
            catch (Exception ex) when (ex is SecurityTokenException || ex is ArgumentException)
            {
                return default;
            }
        }

        private string GetUserType(int userTypeId = 0)
        {
            return userTypeId > 0 ? "User" : "Admin";
        }

        private TokenValidationParameters GenerateTokenValidationParameters()
        {
            var secretKey = Encoding.UTF8.GetBytes(_options.SecretKey);
            var encryptionkey = Encoding.UTF8.GetBytes(_options.Encryptkey);
            var securityKey = new SymmetricSecurityKey(secretKey);
            var tokenDecryptionKey = new SymmetricSecurityKey(encryptionkey);
            return new TokenValidationParameters()
            {
                ValidAudiences = new string[] { _options.ValidAudience },
                ValidIssuers = new string[] { _options.Issuer },
                IssuerSigningKey = securityKey,
                TokenDecryptionKey = tokenDecryptionKey,
            };
        }

        private string GenerateRefreshToken(int userId)
        {
            var randomNumber = new byte[15];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber) + userId;
        }
    }
}