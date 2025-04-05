using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallServiceFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalledController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from CalledController");
        }

        [HttpPost]
        public IActionResult CreateTicket()
        {
            return Ok();
        }
    }
}
