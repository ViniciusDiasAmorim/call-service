using CallServiceFlow.Dto.CustomerDTO;
using FluentValidation;

namespace CallServiceFlow.Validators.Customer
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}