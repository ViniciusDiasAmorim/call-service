using CallServiceFlow.Model;
using FluentValidation;

namespace CallServiceFlow.Validators
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}