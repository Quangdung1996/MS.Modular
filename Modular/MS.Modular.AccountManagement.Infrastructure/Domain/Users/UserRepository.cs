using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.Domain.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly AccountManagementContext _accountManagementContext;
        internal UserRepository(AccountManagementContext accountManagementContext)
        {
            _accountManagementContext = accountManagementContext;
        }

        public Task<ReturnResponse<User>> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResponse<User>> GetUserByEmailAddressAsync(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResponse<User>> GetUserByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResponse<bool>> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
