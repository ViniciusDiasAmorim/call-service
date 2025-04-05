using CallServiceFlow.Dto;
using CallServiceFlow.Repository;
using CallServiceFlow.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallServiceFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TechnicalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTechnical(TechinicalDto techinicalDto)
        {
            var result = await _unitOfWork.TechnicalRepository.CreateTechnical(techinicalDto);

            if (result.ok)
                return Created("", result.message);
            else
                return BadRequest(result.message);
        }
    }
}
