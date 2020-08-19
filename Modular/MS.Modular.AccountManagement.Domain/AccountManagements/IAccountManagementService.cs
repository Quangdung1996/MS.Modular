using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.BuildingBlocks.Domain;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Domain.AccountManagements
{
    public interface IAccountManagementService
    {
        Task<ReturnResponse<RegisterAccountViewModel>> RegisterAsync(CreateAccount createAccount);

        Task<ReturnResponse<AccountDataTransformation>> LoginAsync(AccountDataTransformation accountDataTransformation);

        Task<ReturnResponse<AccountDataTransformation>> UpdateUserAsync(AccountDataTransformation accountDataTransformation);

        Task<ReturnResponse<User>> UpdateUserAsync(int userId);
    }
}