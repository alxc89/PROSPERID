using Microsoft.AspNetCore.Mvc;

namespace PROSPERID.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelthCheck : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Online");
        }
    }
}
