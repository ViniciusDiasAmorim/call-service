using CallServiceFlow.Dto.CustomerDTO;
using CallServiceFlow.Repository;
using CallServiceFlow.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallServiceFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
        {
            var result = await _unitOfWork.CustomerRepository.CreateCustomer(customerDto);

            if (result.ok)
            {
                return Created(result.message, result.responseDto);
            }
            else
            {
                return BadRequest(result.message);
            }
        }
    }
}
