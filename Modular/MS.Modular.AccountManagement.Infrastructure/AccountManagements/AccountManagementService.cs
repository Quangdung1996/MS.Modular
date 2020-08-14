using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.AccountManagement.Infrastructure.Validations;
using MS.Modular.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.AccountManagements
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        public AccountManagementService(IAccountRepository accountRepository,IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<ReturnResponse<AccountDataTransformation>> LoginAsync(AccountDataTransformation accountDataTransformation)
        {
            var returnResponse =new ReturnResponse<AccountDataTransformation>();
            var validator = new CreateAccountTransformtionValidator(_userRepository);
            var result = await validator.ValidateAsync(accountDataTransformation);
            return default;
        }

        public Task<ReturnResponse<AccountDataTransformation>> RegisterAsync(AccountDataTransformation accountDataTransformation)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResponse<AccountDataTransformation>> UpdateUserAsync(AccountDataTransformation accountDataTransformation)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResponse<User>> UpdateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
