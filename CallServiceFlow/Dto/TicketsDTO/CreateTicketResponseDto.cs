using CallServiceFlow.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace CallServiceFlow.Dto.Tickets
{
    public class CreateTicketResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime? CompletionDeadline { get; set; }
        public int CustomerId { get; set; }
        public int TechnicalId { get; set; }
    }
}
