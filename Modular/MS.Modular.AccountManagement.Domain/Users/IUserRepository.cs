using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Domain.Users
{
    public interface IUserRepository
    {
        Task<ReturnResponse<User>> CreateUserAsync(User user);

        Task<ReturnResponse<User>> GetUserByEmailAddressAsync(string emailAddress);

        Task<ReturnResponse<bool>> UpdateUserAsync(User user);

        Task<ReturnResponse<User>> GetUserByUserIdAsync(int userId);
        Task<User> GetAndValidateAsync(AccountSignIn accountSignIn);
    }
}