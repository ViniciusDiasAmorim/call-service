using CallServiceFlow.Dto;
using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<(bool ok, string message)> CreateTicket(TicketDto ticketDto);
    }
}
