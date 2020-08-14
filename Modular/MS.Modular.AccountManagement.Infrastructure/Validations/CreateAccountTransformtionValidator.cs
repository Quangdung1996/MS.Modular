using FluentValidation;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Users;

namespace MS.Modular.AccountManagement.Infrastructure.Validations
{
    public class CreateAccountTransformtionValidator : AbstractValidator<AccountDataTransformation>
    {
        private readonly IUserRepository _userRepository;

        public CreateAccountTransformtionValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            RuleFor(m => m.EmailAddress).MustAsync(async (emailAddress, cancellation) =>
            {
                var user = await _userRepository.GetUserByEmailAddressAsync(emailAddress);
                return user != null;
            }).WithMessage("Email Address already exists.");
        }
    }
}