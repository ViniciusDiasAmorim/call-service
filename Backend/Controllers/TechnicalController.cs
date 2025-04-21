using CallServiceFlow.Dto.TechnicalDTO;
using CallServiceFlow.Model;
using CallServiceFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CallServiceFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalController : ControllerBase
    {
        private readonly ITechnicalService _technicalService;
        private readonly IUserRegistrationService _userRegistrationService;

        public TechnicalController(ITechnicalService technicalService, IUserRegistrationService userRegistrationService)
        {
            _technicalService = technicalService;
            _userRegistrationService = userRegistrationService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTechnical(TechnicalDto techinicalDto)
        {
            var model = new Register()
            {
                Name = techinicalDto.Name,
                Email = techinicalDto.Email,
                Password = techinicalDto.Password,
                ConfirmPassword = techinicalDto.ConfirmPassword
            };

            var resultAuthRegister = await _userRegistrationService.Register(model, "Tecnico");

            if (!resultAuthRegister.ok)
                return BadRequest(resultAuthRegister.message);

            var result = await _technicalService.CreateTechnicalAsync(techinicalDto);

            if (result.ok)
                return Created(result.message, result.responseDto);
            else
                return BadRequest(result.message);
        }
    }
}
