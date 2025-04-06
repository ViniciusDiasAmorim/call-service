using CallServiceFlow.Dto.Tickets;
using CallServiceFlow.Repository;
using CallServiceFlow.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CallServiceFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TicketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> CreateTicket(CreateTicketDto ticketDto)
        {
            var result = await _unitOfWork.TicketRepository.CreateTicket(ticketDto);

            if (result.ok)
                return Created(result.message, result.responseDto);
            else
                return BadRequest(result.message);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Tecnico")]
        public async Task<IActionResult> UpdateTicketStatus(UpdateStatusTicketDto doneTicketDto)
        {
            var result = await _unitOfWork.TicketRepository.UpdateTicketStatus(doneTicketDto);

            if (result.ok)
                return Ok(result.message);
            else
                return BadRequest(result.message);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Tecnico,Cliente")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            //TODO: Implementar o retorno do ticket de acordo com o usuario logado
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userRole = User.FindFirstValue(ClaimTypes.Role);

            var result = await _unitOfWork.TicketRepository.GetTicketById(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound("Ticket not found");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTickets()
        {
            var result = await _unitOfWork.TicketRepository.GetAllTickets();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var result = await _unitOfWork.TicketRepository.DeleteTicket(id);

            if (result.ok)
                return Ok(result.message);
            else
                return BadRequest(result.message);
        }

        [HttpGet("technical/{technicalId}")]
        [Authorize(Roles = "Admin,Tecnico")]
        public async Task<IActionResult> GetTicketsByTechnical(int technicalId)
        {
            var result = await _unitOfWork.TicketRepository.GetTicketsByTechnical(technicalId);

            return Ok(result);
        }
    }
}
