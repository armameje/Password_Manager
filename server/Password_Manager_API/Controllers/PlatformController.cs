using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Password_Manager_API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("[controller]")]
    public class PlatformController : ControllerBase
    {

        [HttpGet]
        [MapToApiVersion(1.0)]
        [Route("{user}")]
        public async Task<IActionResult> AllUserAccounts()
        {
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> AllPlatforms()
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddPlatform()
        {
            return Ok();

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAccount()
        {
            return Ok();
        }
    }
}
