using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace PasswordManagerAPI.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class PlatformController : ControllerBase
    {
        private readonly ILogger<PlatformController> _logger;

        public PlatformController(ILogger<PlatformController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("[action]")]
        public async Task<IActionResult> AddPlatform()
        { 
            return Ok();
        }

        [HttpGet]
        [MapToApiVersion(1.0)]
        [Route("{user}/{platform}")]
        public async Task<IActionResult> GetPlatformInfoForUser(string user, string platform)
        {
            return Ok();
        }
    }
}
