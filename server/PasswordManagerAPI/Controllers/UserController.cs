using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace PasswordManagerAPI.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("[action]")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }

        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("[action]")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}