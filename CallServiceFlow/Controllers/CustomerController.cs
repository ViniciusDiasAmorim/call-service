using CallServiceFlow.Dto.CustomerDTO;
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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserRegistrationService _userRegistrationService;

        public CustomerController(IUnitOfWork unitOfWork, UserRegistrationService userRegistrationService)
        {
            _unitOfWork = unitOfWork;
            _userRegistrationService = userRegistrationService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            var model = new RegisterModel()
            {
                ConfirmPassword = customerDto.ConfirmPassword,
                Email = customerDto.Email,
                Name = customerDto.Name,
                Password = customerDto.Password
            };

            var resultAuthRegister = await _userRegistrationService.Register(model, "Cliente");

            if (!resultAuthRegister.ok)
                return BadRequest(resultAuthRegister.message);

            var result = await _unitOfWork.CustomerRepository.CreateCustomer(customerDto);

            if (result.ok)
                return Created(result.message, result.responseDto);
            else
                return BadRequest(result.message);
        }
    }
}
