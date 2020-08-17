using FluentValidation;
using MS.Modular.AccountManagement.Domain.AccountManagements;

namespace MS.Modular.AccountManagement.Infrastructure.Validations
{
    internal class CreateAccountTransformtionValidator : AbstractValidator<AccountDataTransformation>
    {
        public CreateAccountTransformtionValidator()
        {
            RuleFor(m => m.CompanyName).NotEmpty().WithMessage("First Name is a required field.");
            RuleFor(m => m.LastName).NotEmpty().WithMessage("Last Name is a required field.");
            RuleFor(m => m.CompanyName).NotEmpty().WithMessage("Company Name is a required field.");
            RuleFor(m => m.Password).NotEmpty().WithMessage("Password is a required field.");
            RuleFor(x => x.PasswordConfirmation).NotEmpty().WithMessage("Please enter the confirmation password");
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.PasswordConfirmation)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match");
                }
            });

            RuleFor(m => m.EmailAddress).NotEmpty().WithMessage("EmailAddress is a required field.");
            RuleFor(m => m.EmailAddress).EmailAddress();
        }
    }
}