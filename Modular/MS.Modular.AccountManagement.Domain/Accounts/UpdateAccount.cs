namespace MS.Modular.AccountManagement.Domain.Accounts
{
    public class UpdateAccount : EntityModel
    {
        public string Password { get; set; }

        private UpdateAccount()
        {
        }

        public UpdateAccount(string firstName, string lastName, string companyName, string email, string password, string passwordConfirmation)
        {
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
            EmailAddress = email;
            Password = password;
        }
    }
}