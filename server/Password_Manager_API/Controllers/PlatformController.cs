using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Password_Manager_API.Services;

namespace Password_Manager_API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("[controller]")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;

        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

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
            var platforms = await _platformService.GetAllPlatformsAsync();
            return Ok(platforms);
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
