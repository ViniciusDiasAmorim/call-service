using CallServiceFlow.Dto.Tickets;
using CallServiceFlow.Dto.TicketsDTO;

namespace CallServiceFlow.Services.Interfaces
{
    public interface ITicketService
    {
        Task<(bool ok, string message, CreateTicketResponseDto responseDto)> CreateTicketAsync(CreateTicketDto ticketDto);
        Task<(bool ok, string message)> UpdateTicketStatusAsync(UpdateStatusTicketDto statusDto);
        Task<TicketDto> GetTicketByIdAsync(int id);
        Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
        Task<(bool ok, string message)> DeleteTicketAsync(int id);
        Task<IEnumerable<TicketDto>> GetTicketsByTechnicalAsync(int technicalId);
    }
}
