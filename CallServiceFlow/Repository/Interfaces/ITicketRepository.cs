using CallServiceFlow.Dto.Tickets;
using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<(bool ok, string message, CreateTicketResponseDto responseDto)> CreateTicket(CreateTicketDto dto);

        Task<(bool ok, string message)> UpdateTicketStatus(UpdateStatusTicketDto dto);
    }
}
