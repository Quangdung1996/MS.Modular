namespace MS.Modular.AccountManagement.Domain.Accounts
{
    public class CreateAccount : EntityModel
    {
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public CreateAccount()
        {
        }

        public CreateAccount(string firstName, string lastName, string companyName, string email, string password, string passwordConfirmation)
        {
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
            EmailAddress = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
        }
    }
}