using CallServiceFlow.Model.Enums;

namespace CallServiceFlow.Dto.TicketsDTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime? CompletionDeadline { get; set; }
        public Status Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int CustomerId { get; set; }
        public int TechnicalId { get; set; }
    }
}
