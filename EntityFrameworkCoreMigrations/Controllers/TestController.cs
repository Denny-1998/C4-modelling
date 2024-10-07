using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreMigrations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/test
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This controller is just for testing if the Api is reachable.");
        }
    }
}
