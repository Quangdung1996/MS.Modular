using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.Dto;
using MS.Modular.AccountManagement.Domain.Redis;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.BuildingBlocks.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        //public TokenService(IRedisService redisService)
        //{
        //    _redisService = redisService;
        //}

        public async Task<TokenInfo> GenerateTokenAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_options.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512);

            var encryptionkey = Encoding.UTF8.GetBytes(_options.Encryptkey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey),
                                                                  SecurityAlgorithms.Aes128KW,
                                                                  SecurityAlgorithms.Aes128CbcHmacSha256);

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(AppConstant.ClaimAccount, JsonConvert.SerializeObject(new UserViewModel
                {
                    UserId=user.UserId,
                    EmailAddress=user.EmailAddress,
                    LastName=user.LastName,
                    FirstName=user.FirstName
                }, _jsonSerializerSettings)),
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

        public Task<TokenInfo> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public ReturnResponse<User> VerifyToken(string jwtToken)
        {
            throw new NotImplementedException();
        }

        private string GetUserType(int userTypeId = 0)
        {
            return userTypeId > 0 ? "User" : "Admin";
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