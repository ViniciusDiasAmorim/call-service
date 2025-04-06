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

        public async Task<(bool, string, CreateTicketResponseDto)> CreateTicket(CreateTicketDto ticketDto)
        {
            var maxTickets = _context.Technicals.Where(x => x.Id == ticketDto.TechnicalId && x.Active).FirstOrDefault().MaxTickets;
            var activedTickets = _context.Tickets.Where(t => t.TechnicalId == ticketDto.TechnicalId && (t.Status == Status.Open || t.Status == Status.InProgress)).Count();

            if (activedTickets + 1 > maxTickets)
                return (false, $"O Técnico ja atingiu o maximo de chamados que pode atender. maximo de chamados: {maxTickets}", null);

            var ticket = new Ticket()
            {
                Title = ticketDto.Title,
                Description = ticketDto.Description,
                Priority = ticketDto.Priority,
                CompletionDeadline = ticketDto.CompletionDeadline,
                Status = Status.Open,
                CreationDate = DateTime.Now,
                CustomerId = ticketDto.CustomerId,
                TechnicalId = ticketDto.TechnicalId
            };

            try
            {
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();

                var dtoResponse = new CreateTicketResponseDto()
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    Priority = ticket.Priority,
                    CompletionDeadline = ticket.CompletionDeadline,
                    CustomerId = ticket.CustomerId,
                    TechnicalId = ticket.TechnicalId
                };

                return (true, "Chamado criado com sucesso", dtoResponse);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar chamado: {ex.Message}", null);
            }
        }

        public async Task<(bool ok, string message)> UpdateTicketStatus(UpdateStatusTicketDto dto)
        {
            var ticket = await _context.Tickets.Where(t => t.Id == dto.TicketId).FirstOrDefaultAsync();

            if (ticket == null)
                return (false, "Chamado não encontrado");

            ticket.Status = dto.NewStatus;

            try
            {
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
                return (true, "Chamado atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar chamado: {ex.Message}");
            }
        }

        public async Task<TicketDto> GetTicketById(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return null;

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                CompletionDeadline = ticket.CompletionDeadline,
                Status = ticket.Status,
                CreationDate = ticket.CreationDate,
                CustomerId = ticket.CustomerId,
                TechnicalId = ticket.TechnicalId
            };
        }

        public async Task<IEnumerable<TicketDto>> GetAllTickets()
        {
            return await _context.Tickets
                .Select(ticket => new TicketDto
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    Priority = ticket.Priority,
                    CompletionDeadline = ticket.CompletionDeadline,
                    Status = ticket.Status,
                    CreationDate = ticket.CreationDate,
                    CustomerId = ticket.CustomerId,
                    TechnicalId = ticket.TechnicalId
                })
                .ToListAsync();
        }

        public async Task<(bool ok, string message)> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (ticket == null)
                return (false, "Ticket not found");

            ticket.Active = false;
            _context.Entry(ticket).Property(x => x.Active).IsModified = true;
            _context.Update(ticket);
            await _context.SaveChangesAsync();

            return (true, "Ticket deleted successfully");
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsByTechnical(int technicalId)
        {
            return await _context.Tickets
                .Where(ticket => ticket.TechnicalId == technicalId)
                .Select(ticket => new TicketDto
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    Priority = ticket.Priority,
                    CompletionDeadline = ticket.CompletionDeadline,
                    Status = ticket.Status,
                    CreationDate = ticket.CreationDate,
                    CustomerId = ticket.CustomerId,
                    TechnicalId = ticket.TechnicalId
                })
                .ToListAsync();
        }
    }
}
