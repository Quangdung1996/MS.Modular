using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;
using System;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.Domain.Accounts
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly AccountManagementContext _accountManagementContext;

        internal AccountRepository(AccountManagementContext accountManagementContext)
        {
            _accountManagementContext = accountManagementContext;
        }

        public async Task<ReturnResponse<int>> CreateAccountAsync(Account account)
        {
            var returnResponse = new ReturnResponse<int>();
            try
            {
                await _accountManagementContext.Accounts.AddAsync(account);
                await  _accountManagementContext.SaveChangesAsync();
                returnResponse.Data = account.AccountId;
                returnResponse.Succeeded = true;
            }
            catch (Exception ex)
            {
                returnResponse.Data = 0;
                returnResponse.Error = ex.Message.ToString();
            }
            return returnResponse;
        }

        public Task<ReturnResponse<Account>> GetAccountInformation(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResponse<bool>> UpdateAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}