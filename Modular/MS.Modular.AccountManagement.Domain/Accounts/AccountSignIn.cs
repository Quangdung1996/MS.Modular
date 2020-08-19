using MS.Modular.AccountManagement.Domain.Users;

namespace MS.Modular.AccountManagement.Domain.Accounts
{
    public class AccountSignIn
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }
        public int UserType { get; set; }
    }
}