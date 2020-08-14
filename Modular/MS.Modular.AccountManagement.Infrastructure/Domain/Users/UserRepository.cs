using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.Domain.Users
{
    internal class UserRepository : IAccountRepository
    {
        private readonly AccountManagementContext _accountManagementContext;
        internal UserRepository(AccountManagementContext accountManagementContext)
        {
            _accountManagementContext = accountManagementContext;
        }

        public async Task<ReturnResponse<Account>> CreateAccountAsync(Account account)
        {
            throw new NotImplementedException();
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
