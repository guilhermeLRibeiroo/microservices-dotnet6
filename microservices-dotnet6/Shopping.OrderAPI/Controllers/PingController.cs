using Microsoft.AspNetCore.Mvc;

// Temporary Controller
namespace Shopping.OrderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController 
        : ControllerBase
    {
        public PingController() { }

        [HttpGet(Name = "Ping")]
        public string Get()
        {
            return "Pong";
        }
    }
}