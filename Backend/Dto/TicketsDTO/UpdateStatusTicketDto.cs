using CallServiceFlow.Model.Enums;

namespace CallServiceFlow.Dto.Tickets
{
    public class UpdateStatusTicketDto
    {
        public int TicketId { get; set; }
        public Status NewStatus { get; set; }
    }
}
