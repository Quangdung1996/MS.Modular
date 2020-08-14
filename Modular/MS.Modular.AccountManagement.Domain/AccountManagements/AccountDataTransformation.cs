using System.ComponentModel.DataAnnotations;

namespace MS.Modular.AccountManagement.Domain.AccountManagements
{
    public class AccountDataTransformation
    {
        public AccountDataTransformation()
        {
        }

        public int UserId { get; set; }
        public int AccountId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirmation { get; set; }
        public string Token { get; set; }
        public bool IsAuthenicated { get; set; }
    }
}