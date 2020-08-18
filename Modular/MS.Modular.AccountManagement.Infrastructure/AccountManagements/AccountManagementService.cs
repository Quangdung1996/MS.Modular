using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Internal;
using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.AccountManagement.Infrastructure.Validations;
using MS.Modular.BuildingBlocks.Domain;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.AccountManagements
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public AccountManagementService(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<ReturnResponse<AccountDataTransformation>> LoginAsync(AccountDataTransformation accountDataTransformation)
        {
            var returnResponse = new ReturnResponse<AccountDataTransformation>();
            var validator = new CreateAccountTransformtionValidator();
            var result = await validator.ValidateAsync(accountDataTransformation);
            return default;
        }

        public async Task<ReturnResponse<AccountDataTransformation>> RegisterAsync(AccountDataTransformation accountDataTransformation)
        {
            var returnResponse = new ReturnResponse<AccountDataTransformation>();
            var validator = new CreateAccountTransformtionValidator();
            ValidationResult validatorResult = await validator.ValidateAsync(accountDataTransformation);
            if (validatorResult.Errors.Any())
            {
                returnResponse.Error = validatorResult.Errors.Join().ToString();
                returnResponse.Successful = false;
                return returnResponse;
            }
            var account = new Account()
            {
                Name = accountDataTransformation.CompanyName
            };
            var result=await _accountRepository.CreateAccountAsync(account);
            return default;
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