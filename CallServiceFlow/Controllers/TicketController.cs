using CallServiceFlow.Dto;
using CallServiceFlow.Repository;
using CallServiceFlow.Repository.Interfaces;
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
        public async Task<IActionResult> CreateTicket(TicketDto ticketDto)
        {
           var result = await _unitOfWork.TicketRepository.CreateTicket(ticketDto);

            if(result.ok)
                return Created("", result.message);
            else
                return BadRequest(result.message);
        }
    }
}
