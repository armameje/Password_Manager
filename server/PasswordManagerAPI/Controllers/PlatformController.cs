using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManagerAPI.Repository.Model;
using PasswordManagerAPI.Services;
using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion(1.0)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class PlatformController : ControllerBase
    {
        private readonly ILogger<PlatformController> _logger;
        private readonly IPlatformService _platformService;

        public PlatformController(ILogger<PlatformController> logger, IPlatformService platformService)
        {
            _logger = logger;
            _platformService = platformService;
        }

        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("{user}/{platform}")]
        public async Task<IActionResult> AddPlatform(string user, string platform, [FromBody] PlatformAccount account)
        {
            await _platformService.AddPlatformAccountAsync(user, platform, account.Username, account.Password);

            return Ok();
        }

        [HttpGet]
        [MapToApiVersion(1.0)]
        [Route("{user}/{platform}/{username}")]
        public async Task<IActionResult> GetPlatformInfoForUser(string user, string platform, string username)
        {
            var platformAccount = await _platformService.GetPlatformAccountAsync(user, platform, username);

            return Ok(platformAccount);
        }

        [HttpDelete]
        [MapToApiVersion(1.0)]
        [Route("{user}/delete/{platform}/{username}")]
        public async Task<IActionResult> DeletePlatform(string user, string platform, string username)
        {
            await _platformService.DeletePlatformAccountAsync(user, platform, username);

            return Ok();
        }

        [HttpPut]
        [MapToApiVersion(1.0)]
        [Route("{user}/{platform}/changepassword")]
        public async Task<IActionResult> ChangePlatformPassword(string user, string platform, [FromBody] PlatformAccount account)
        {
            await _platformService.ChangePlatformAccountPasswordAsync(user, platform, account.Username, account.Password);

            return Ok();
        }

        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("{user}/paltforms")]
        public async Task<IActionResult> GetAllPlatformsForUser(string user)
        {
            var allPlatforms = await _platformService.GetAllPlatformsOfUserAsync(user);

            return Ok(allPlatforms);
        }
    }
}
