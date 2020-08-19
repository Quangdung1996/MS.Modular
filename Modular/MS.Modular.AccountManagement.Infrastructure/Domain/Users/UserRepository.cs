using Microsoft.EntityFrameworkCore;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.BuildingBlocks.Domain;
using MS.Modular.BuildingBlocks.Domain.Extenstions;
using System;
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

        public async Task<ReturnResponse<User>> CreateUserAsync(User user)
        {
            var returnResponse = new ReturnResponse<User>();
            try
            {
                var checkEmailExists = await this.GetUserByEmailAddressAsync(user.EmailAddress).ConfigureAwait(false);
                if (checkEmailExists.Data is null)
                {
                    user.EmailAddress.ToLowerEmail();
                    await _accountManagementContext.Users.AddAsync(user);
                    await _accountManagementContext.SaveChangesAsync();
                    returnResponse.Data = user;
                    returnResponse.Succeeded = true;
                }
                else
                {
                    returnResponse.Error = $"Email {user.EmailAddress} exists!";
                    returnResponse.Succeeded = false;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Succeeded = false;
                returnResponse.Error = ex.Message.ToString();
            }
            return returnResponse;
        }

        public async Task<User> GetAndValidateAsync(AccountSignIn accountSignIn)
        {
            accountSignIn.EmailAddress.ToLowerEmail();
            var accountInfo = await _accountManagementContext.Users.FirstOrDefaultAsync(x => x.EmailAddress.Equals(accountSignIn.EmailAddress) && x.UserTypeId == accountSignIn.UserType);
            if (accountInfo != null && VerifyPassword(accountInfo, accountSignIn.Password))
            {
                accountInfo.Password = string.Empty;
                accountInfo.HashSalt = string.Empty;
                return accountInfo;
            }

            return default;
        }

        public async Task<ReturnResponse<User>> GetUserByEmailAddressAsync(string emailAddress)
        {
            var returnResponse = new ReturnResponse<User>();
            try
            {
                emailAddress.ToLowerEmail();
                returnResponse.Data = await _accountManagementContext.Users.FirstOrDefaultAsync(x => x.EmailAddress.Equals(emailAddress));
                returnResponse.Succeeded = true;
            }
            catch (Exception ex)
            {
                returnResponse.Succeeded = false;
                returnResponse.Error = ex.Message.ToString();
            }
            return returnResponse;
        }

        public Task<ReturnResponse<User>> GetUserByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResponse<bool>> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public static bool VerifyPassword(User source, string originalPassword)
        {
            return source.Password.Equals(AccountExetions.HashPassword(originalPassword, source.HashSalt));
        }
    }
}