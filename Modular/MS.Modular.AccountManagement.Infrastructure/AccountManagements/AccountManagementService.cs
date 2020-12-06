using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Internal;
using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.Dto;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.AccountManagement.Infrastructure.Validations;
using MS.Modular.BuildingBlocks.Domain;
using MS.Modular.BuildingBlocks.Domain.Extenstions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.AccountManagements
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountManagementService(IAccountRepository accountRepository,
                                        IUserRepository userRepository,
                                        IMapper mapper,
                                        ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ReturnResponse<RegisterAccountViewModel>> LoginAsync(AccountSignIn accountSignIn)
        {
            var returnResponse = new ReturnResponse<RegisterAccountViewModel>();
            var validator = new AccountSignInValidator();
            var validatorResult = await validator.ValidateAsync(accountSignIn);
            if (validatorResult.Errors.Any())
            {
                returnResponse.Error = validatorResult.Errors.Join().ToString();
                returnResponse.Succeeded = false;
                return returnResponse;
            }
            var user = await _userRepository.GetAndValidateAsync(accountSignIn);

            if (user != null)
            {
                var tokenInfo = await _tokenService.GenerateTokenAsync(user);

                returnResponse.Data = new RegisterAccountViewModel()
                {
                    TokenInfo = new TokenInfo
                    {
                        AccessToken = tokenInfo.AccessToken,
                        ExpiredInMinute = tokenInfo.ExpiredInMinute,
                        RefreshToken = tokenInfo.RefreshToken
                    },
                    EmailAddress = user.EmailAddress,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                returnResponse.Succeeded = true;
                return returnResponse;
            }
            returnResponse.Succeeded = false;
            return returnResponse;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
        public async Task<ReturnResponse<RegisterAccountViewModel>> RegisterAsync(CreateAccount createAccount)
        {
            var returnResponse = new ReturnResponse<RegisterAccountViewModel>();
            var validator = new CreateAccountValidator(_userRepository);
            ValidationResult validatorResult = await validator.ValidateAsync(createAccount);
            if (validatorResult.Errors.Any())
            {
                returnResponse.Error = validatorResult.Errors.Join().ToString();
                returnResponse.Succeeded = false;
                return returnResponse;
            }

            var account = new Account(createAccount.CompanyName, 1);
            var responseAccount = await _accountRepository.CreateAccountAsync(account);
            returnResponse.Succeeded = responseAccount.Succeeded;
            returnResponse.Error = responseAccount.Error;

            if (responseAccount.Succeeded)
            {
                var salt = AccountExetions.GeneraSalt();
                var user = _mapper.Map<CreateAccount, User>(createAccount);
                user.AccountId = account.AccountId;
                user.UserTypeId = 1;
                user.Password = AccountExetions.HashPassword(user.Password, salt);
                user.HashSalt = salt;
                var responseUser = await _userRepository.CreateUserAsync(user);
                returnResponse.Data = _mapper.Map<User, RegisterAccountViewModel>(responseUser.Data);
                returnResponse.Data.CompanyName = createAccount.CompanyName;
                returnResponse.Data.TokenInfo = await _tokenService.GenerateTokenAsync(user);
                returnResponse.Succeeded = responseUser.Succeeded;
                returnResponse.Error = responseUser.Error;
            }
            return returnResponse;
        }

        public async Task<ReturnResponse<UpdateAccount>> UpdateAccountAsync(UpdateAccount updateAccount)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResponse<User>> UpdateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}