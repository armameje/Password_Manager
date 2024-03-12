using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Password_Manager_API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [MapToApiVersion(1.0)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [MapToApiVersion(1.0)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
