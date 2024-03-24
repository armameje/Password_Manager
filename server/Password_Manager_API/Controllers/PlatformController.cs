using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Password_Manager_API.Model;
using Password_Manager_API.Services;

namespace Password_Manager_API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    [Route("api/v{v:apiVersion}/[controller]")]
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
        public async Task<IActionResult> AllUserAccounts(string user)
        {
            List<PlatformAccount> userAccounts = new List<PlatformAccount>(); ; 

            try
            {
                userAccounts.AddRange(await _platformService.GetAllAccountsOfUserAsync(user));
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok(userAccounts);
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
        public async Task<IActionResult> AddPlatform([FromBody] string platformName)
        {
            try
            {
                await _platformService.AddPlatformAsync(platformName);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAccount([FromBody] UserPlatformAccount account)
        {
            try
            {
                await _platformService.AddAccountToPlatformAsync(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
