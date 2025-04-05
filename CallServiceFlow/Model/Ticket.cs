using CallServiceFlow.Model.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CallServiceFlow.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public Customer Customer { get; set; }
        public Technical Technical { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDeadline { get; set; }
    }
}
