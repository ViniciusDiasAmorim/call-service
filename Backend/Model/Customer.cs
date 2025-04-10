﻿namespace CallServiceFlow.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Ticket> Calls { get; set; } = new List<Ticket>();
    }
}