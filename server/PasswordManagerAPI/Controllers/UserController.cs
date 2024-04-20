using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace PasswordManagerAPI.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

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