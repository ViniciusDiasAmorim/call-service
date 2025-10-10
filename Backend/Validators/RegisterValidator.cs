using CallServiceFlow.Model;
using FluentValidation;

namespace CallServiceFlow.Validators
{
    public class RegisterValidator : AbstractValidator<Register>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Must(HasUpper).WithMessage("Password must contain at least one uppercase letter")
                .Must(HasLower).WithMessage("Password must contain at least one lowercase letter")
                .Must(HasDigit).WithMessage("Password must contain at least one digit")
                .Must(HasNonAlnum).WithMessage("Password must contain at least one non-alphanumeric char");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }

        private bool HasUpper(string v) => v.Any(char.IsUpper);
        private bool HasLower(string v) => v.Any(char.IsLower);
        private bool HasDigit(string v) => v.Any(char.IsDigit);
        private bool HasNonAlnum(string v) => v.Any(ch => !char.IsLetterOrDigit(ch));
    }
}