using CallServiceFlow.Dto.Tickets;
using CallServiceFlow.Dto.TicketsDTO;
using CallServiceFlow.Model;
using CallServiceFlow.Model.Enums;
using CallServiceFlow.Repository;
using CallServiceFlow.Repository.Interfaces;
using CallServiceFlow.Services.Interfaces;

namespace CallServiceFlow.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool ok, string message, CreateTicketResponseDto responseDto)> CreateTicketAsync(CreateTicketDto ticketDto)
        {
            var technical = await _unitOfWork.TicketRepository.GetTechnicalByIdAsync(ticketDto.TechnicalId);

            if (technical == null)
                return (false, "Técnico não encontrado ou inativo", null);

            var activeTicketsCount = await _unitOfWork.TicketRepository.GetActiveTicketsCountByTechnicalAsync(ticketDto.TechnicalId);

            if (activeTicketsCount + 1 > technical.MaxTickets)
                return (false, $"O Técnico já atingiu o máximo de chamados que pode atender. Máximo de chamados: {technical.MaxTickets}", null);

            var ticket = new Ticket
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

            await _unitOfWork.TicketRepository.AddTicketAsync(ticket);
            await _unitOfWork.TicketRepository.SaveChangesAsync();

            var responseDto = new CreateTicketResponseDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                CompletionDeadline = ticket.CompletionDeadline,
                CustomerId = ticket.CustomerId,
                TechnicalId = ticket.TechnicalId
            };

            return (true, "Chamado criado com sucesso", responseDto);
        }

        public async Task<(bool ok, string message)> UpdateTicketStatusAsync(UpdateStatusTicketDto statusDto)
        {
            var ticket = await _unitOfWork.TicketRepository.GetTicketByIdAsync(statusDto.TicketId);

            if (ticket == null)
                return (false, "Chamado não encontrado");

            ticket.Status = statusDto.NewStatus;

            _unitOfWork.TicketRepository.UpdateTicket(ticket);
            await _unitOfWork.TicketRepository.SaveChangesAsync();

            return (true, "Chamado atualizado com sucesso");
        }

        public async Task<TicketDto> GetTicketByIdAsync(int id)
        {
            var ticket = await _unitOfWork.TicketRepository.GetTicketByIdAsync(id);

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

        public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
        {
            var tickets = await _unitOfWork.TicketRepository.GetAllTicketsAsync();

            return tickets.Select(ticket => new TicketDto
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
            });
        }

        public async Task<(bool ok, string message)> DeleteTicketAsync(int id)
        {
            var ticket = await _unitOfWork.TicketRepository.GetTicketByIdAsync(id);

            if (ticket == null)
                return (false, "Ticket not found");

            ticket.Active = false;
            _unitOfWork.TicketRepository.UpdateTicket(ticket);
            await _unitOfWork.TicketRepository.SaveChangesAsync();

            return (true, "Ticket deleted successfully");
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsByTechnicalAsync(int technicalId)
        {
            var tickets = await _unitOfWork.TicketRepository.GetTicketsByTechnicalAsync(technicalId);

            return tickets.Select(ticket => new TicketDto
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
            });
        }
    }
}
