using CallServiceFlow.Context;
using CallServiceFlow.Dto.Tickets;
using CallServiceFlow.Dto.TicketsDTO;
using CallServiceFlow.Model;
using CallServiceFlow.Model.Enums;
using CallServiceFlow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CallServiceFlow.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByTechnicalAsync(int technicalId)
        {
            return await _context.Tickets
                .Where(ticket => ticket.TechnicalId == technicalId)
                .ToListAsync();
        }

        public async Task<int> GetActiveTicketsCountByTechnicalAsync(int technicalId)
        {
            return await _context.Tickets
                .Where(t => t.TechnicalId == technicalId && (t.Status == Status.Open || t.Status == Status.InProgress))
                .CountAsync();
        }

        public async Task<Technical> GetTechnicalByIdAsync(int technicalId)
        {
            return await _context.Technicals.FirstOrDefaultAsync(x => x.Id == technicalId && x.Active);
        }

        public void UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
