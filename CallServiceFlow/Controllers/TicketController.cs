using CallServiceFlow.Dto.Tickets;
using CallServiceFlow.Repository;
using CallServiceFlow.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTicket(CreateTicketDto ticketDto)
        {
           var result = await _unitOfWork.TicketRepository.CreateTicket(ticketDto);

            if(result.ok)
                return Created(result.message, result.responseDto);
            else
                return BadRequest(result.message);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTicketStatus(UpdateStatusTicketDto doneTicketDto)
        {
            var result = await _unitOfWork.TicketRepository.UpdateTicketStatus(doneTicketDto);

            if (result.ok)
                return Ok(result.message);
            else
                return BadRequest(result.message);
        }
    }
}
