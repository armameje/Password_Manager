using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Password_Manager_API.Model;
using Password_Manager_API.Repository;
using Password_Manager_API.Services;

namespace Password_Manager_API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [MapToApiVersion(1.0)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(IOptions<KeysOption> options)
        {
            var x = options.Value;
            return Ok(new { PublicKey = x.PublicKey, PrivateKey = x.PrivateKey });
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
