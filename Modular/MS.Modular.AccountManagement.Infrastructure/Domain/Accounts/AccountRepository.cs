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

        public async Task<ReturnResponse<Account>> CreateAccountAsync(Account account)
        {
            var returnResponse = new ReturnResponse<Account>();
            try
            {
                await _accountManagementContext.Accounts.AddAsync(account);
                returnResponse.Data = account;
                returnResponse.Successful = true;
            }
            catch (Exception ex)
            {
                returnResponse.Data = account;
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