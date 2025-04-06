using CallServiceFlow.Dto.Tickets;
using CallServiceFlow.Dto.TicketsDTO;
using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<(bool ok, string message, CreateTicketResponseDto responseDto)> CreateTicket(CreateTicketDto ticketDto);
        Task<(bool ok, string message)> UpdateTicketStatus(UpdateStatusTicketDto doneTicketDto);
        Task<TicketDto> GetTicketById(int id);
        Task<IEnumerable<TicketDto>> GetAllTickets();
        Task<(bool ok, string message)> DeleteTicket(int id);
        Task<IEnumerable<TicketDto>> GetTicketsByTechnical(int technicalId);
    }
}
