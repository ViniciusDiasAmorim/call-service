using CallServiceFlow.Dto.Tickets;
using FluentValidation;

namespace CallServiceFlow.Validators.Tickets
{
    public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Priority)
                .IsInEnum();

            RuleFor(x => x.CompletionDeadline)
                .Must(BeInTheFutureOrNull)
                .WithMessage("Completion deadline must be in the future");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0);

            RuleFor(x => x.TechnicalId)
                .GreaterThan(0);
        }

        private bool BeInTheFutureOrNull(DateTime? date) => !date.HasValue || date.Value > DateTime.UtcNow;
    }
}