using CallServiceFlow.Dto.TechnicalDTO;
using CallServiceFlow.Model;
using CallServiceFlow.Repository;
using CallServiceFlow.Repository.Interfaces;
using CallServiceFlow.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallServiceFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserRegistrationService _userRegistrationService;

        public TechnicalController(IUnitOfWork unitOfWork, UserRegistrationService userRegistrationService)
        {
            _unitOfWork = unitOfWork;
            _userRegistrationService = userRegistrationService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTechnical(TechnicalDto techinicalDto)
        {
            var model = new RegisterModel()
            {
                Name = techinicalDto.Name,
                Email = techinicalDto.Email,
                Password = techinicalDto.Password,
                ConfirmPassword = techinicalDto.ConfirmPassword
            };

            var resultAuthRegister = await _userRegistrationService.Register(model, "Tecnico");

            if (!resultAuthRegister.ok)
                return BadRequest(resultAuthRegister.message);

            var result = await _unitOfWork.TechnicalRepository.CreateTechnical(techinicalDto);

            if (result.ok)
                return Created(result.message, result.responseDto);
            else
                return BadRequest(result.message);
        }
    }
}
