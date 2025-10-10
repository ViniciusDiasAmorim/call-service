using CallServiceFlow.Dto.Tickets;
using FluentValidation;

namespace CallServiceFlow.Validators.Tickets
{
    public class UpdateStatusTicketDtoValidator : AbstractValidator<UpdateStatusTicketDto>
    {
        public UpdateStatusTicketDtoValidator()
        {
            RuleFor(x => x.TicketId)
                .GreaterThan(0);

            RuleFor(x => x.NewStatus)
                .IsInEnum();
        }
    }
}