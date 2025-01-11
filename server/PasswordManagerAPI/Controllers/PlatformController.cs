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

        /// <summary>
        /// Add a platform for a User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="platform"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("{user}/{platform}")]
        public async Task<IActionResult> AddPlatform(string user, string platform, [FromBody] PlatformAccount account)
        {
            await _platformService.AddPlatformAccountAsync(user, platform, account.Username, account.Password);

            return Ok();
        }

        /// <summary>
        /// Get a platform's credential from a User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="platform"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion(1.0)]
        [Route("{user}/{platform}/{username}")]
        public async Task<IActionResult> GetPlatformInfoForUser(string user, string platform, string username)
        {
            var platformAccount = await _platformService.GetPlatformAccountAsync(user, platform, username);

            return Ok(platformAccount);
        }

        /// <summary>
        /// Delete a platform from a User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="platform"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpDelete]
        [MapToApiVersion(1.0)]
        [Route("{user}/delete/{platform}/{username}")]
        public async Task<IActionResult> DeletePlatform(string user, string platform, string username)
        {
            await _platformService.DeletePlatformAccountAsync(user, platform, username);

            return Ok();
        }

        /// <summary>
        /// Change a User's platform password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="platform"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut]
        [MapToApiVersion(1.0)]
        [Route("{user}/{platform}/changepassword")]
        public async Task<IActionResult> ChangePlatformPassword(string user, string platform, [FromBody] PlatformAccount account)
        {
            await _platformService.ChangePlatformAccountPasswordAsync(user, platform, account.Username, account.Password);

            return Ok();
        }

        /// <summary>
        /// Get all platform names of a User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion(1.0)]
        [Route("{user}/platforms")]
        public async Task<IActionResult> GetAllPlatformsForUser(string user)
        {
            var allPlatforms = await _platformService.GetAllPlatformsOfUserAsync(user);

            return Ok(allPlatforms);
        }
    }
}
