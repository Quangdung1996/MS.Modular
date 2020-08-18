using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Domain
{
    public interface IAccountRepository
    {
        Task<ReturnResponse<int>> CreateAccountAsync(Account account);

        Task<ReturnResponse<bool>> UpdateAccountAsync(Account account);

        Task<ReturnResponse<Account>> GetAccountInformation(int accountId);
    }
}