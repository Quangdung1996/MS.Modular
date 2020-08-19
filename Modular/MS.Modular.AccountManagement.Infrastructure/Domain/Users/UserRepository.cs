using Microsoft.EntityFrameworkCore;
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
                    user.EmailAddress = AccountExetions.ToLower(user.EmailAddress);
                    await _accountManagementContext.Users.AddAsync(user);
                    await _accountManagementContext.SaveChangesAsync();
                    returnResponse.Data = user;
                    returnResponse.Successful = true;
                }
                else
                {
                    returnResponse.Error = $"Email {user.EmailAddress} exists!";
                    returnResponse.Successful = false;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Successful = false;
                returnResponse.Error = ex.Message.ToString();
            }
            return returnResponse;
        }

        public async Task<ReturnResponse<User>> GetUserByEmailAddressAsync(string emailAddress)
        {
            var returnResponse = new ReturnResponse<User>();
            try
            {
                emailAddress = string.IsNullOrEmpty(emailAddress) ? string.Empty : AccountExetions.ToLower(emailAddress);
                returnResponse.Data = await _accountManagementContext.Users.FirstOrDefaultAsync(x => x.EmailAddress.Equals(emailAddress));
                returnResponse.Successful = true;
            }
            catch (Exception ex)
            {
                returnResponse.Successful = false;
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
    }
}