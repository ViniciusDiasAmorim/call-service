namespace CallServiceFlow.Model
{
    public class Technical
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool Active { get; set; } = true;
        public short? MaxTickets { get; set; }
    }
}
