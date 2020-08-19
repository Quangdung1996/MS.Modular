using FluentValidation;
using MS.Modular.AccountManagement.Domain.Accounts;

namespace MS.Modular.AccountManagement.Infrastructure.Validations
{
    public class AccountSignInValidator : AbstractValidator<AccountSignIn>
    {
        public AccountSignInValidator()
        {
            RuleFor(m => m.Password).NotEmpty().WithMessage("Password is a required field.");

            RuleFor(m => m.EmailAddress).NotEmpty().WithMessage("EmailAddress is a required field.");
            RuleFor(m => m.EmailAddress).EmailAddress();
        }
    }
}