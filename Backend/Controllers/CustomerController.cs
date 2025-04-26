using CallServiceFlow.Dto.CustomerDTO;
using CallServiceFlow.Model;
using CallServiceFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CallServiceFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IUserRegistrationService _userRegistrationService;

        public CustomerController(ICustomerService customerService, IUserRegistrationService userRegistrationService)
        {
            _customerService = customerService;
            _userRegistrationService = userRegistrationService;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await  _customerService.DeleteCustomer(id);

            if(result.ok)
                return Ok(result.message);
            else
                return BadRequest(result.message);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            var model = new Register()
            {
                ConfirmPassword = customerDto.ConfirmPassword,
                Email = customerDto.Email,
                Name = customerDto.Name,
                Password = customerDto.Password
            };

            var resultAuthRegister = await _userRegistrationService.Register(model, "Cliente");

            if (!resultAuthRegister.ok)
                return BadRequest(resultAuthRegister.message);

            var result = await _customerService.CreateCustomer(customerDto);

            if (result.ok)
                return Created(result.message, result.responseDto);
            else
                return BadRequest(result.message);
        }
    }
}
