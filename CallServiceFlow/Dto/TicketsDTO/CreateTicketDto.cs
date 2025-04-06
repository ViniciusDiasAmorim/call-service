using CallServiceFlow.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace CallServiceFlow.Dto.Tickets
{
    public class CreateTicketDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CompletionDeadline { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int TechnicalId { get; set; }
    }
}
