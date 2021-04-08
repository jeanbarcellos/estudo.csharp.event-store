using Microsoft.AspNetCore.Mvc;

namespace TestEventStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("funcionou");
        }
    }
}
