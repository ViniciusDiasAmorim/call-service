using CallServiceFlow.Dto.TechnicalDTO;
using FluentValidation;

namespace CallServiceFlow.Validators.Technical
{
    public class TechnicalDtoValidator : AbstractValidator<TechnicalDto>
    {
        public TechnicalDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}