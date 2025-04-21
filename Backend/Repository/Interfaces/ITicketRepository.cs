using CallServiceFlow.Model;

namespace CallServiceFlow.Repository.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task AddTicketAsync(Ticket ticket);
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<IEnumerable<Ticket>> GetTicketsByTechnicalAsync(int technicalId);
        Task<int> GetActiveTicketsCountByTechnicalAsync(int technicalId);
        Task<Technical> GetTechnicalByIdAsync(int technicalId);
        void UpdateTicket(Ticket ticket);
    }
}
