using MS.Modular.AccountManagement.Domain.Dto;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.BuildingBlocks.Domain;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Domain
{
    public interface ITokenService
    {
        Task<TokenInfo> GenerateTokenAsync(User user);

        Task<TokenInfo> RefreshTokenAsync(string refreshToken);

        ReturnResponse<User> VerifyToken(string jwtToken);
    }
}