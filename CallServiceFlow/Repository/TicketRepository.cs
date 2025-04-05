using CallServiceFlow.Context;
using CallServiceFlow.Dto;
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

        public async Task<(bool, string)> CreateTicket(TicketDto ticketDto)
        {
            var customer = await _context.Customers.Where(x => x.Id == ticketDto.CustomerId).FirstOrDefaultAsync();
            var technical = await _context.Technicals.Where(x => x.Id == ticketDto.TechnicalId).FirstOrDefaultAsync();

            if (customer == null)
                return (false, "Cliente não encontrado");

            if (technical == null)
                return (false, "Técnico não encontrado");
            
            if(technical.ActiveTickets + 1 > technical.MaxTickets)
                return (false, $"Técnico não pode atender mais chamados ele possui um total de {technical.ActiveTickets} chamados ativos.");

            var ticket = new Ticket()
            {
                Title = ticketDto.Title,
                Description = ticketDto.Description,
                Priority = ticketDto.Priority,
                CompletionDeadline = ticketDto.CompletionDeadline,
                Status = Status.Open,
                CreationDate = DateTime.Now,
                Customer = customer,
                Technical = technical
            };

            try
            {
                await _context.Tickets.AddAsync(ticket);
                
                technical.ActiveTickets += 1;

                _context.Entry(technical).State = EntityState.Modified;
                _context.Set<Technical>().Update(technical);

                await _context.SaveChangesAsync();
           
                return (true, "Chamado criado com sucesso");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar chamado: {ex.Message}");
            }
        }
    }
}
