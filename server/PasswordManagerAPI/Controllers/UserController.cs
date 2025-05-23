﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PasswordManagerAPI.Services;
using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userServce;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userServce = userService;
            _logger = logger;
        }

        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] UserRegistration user)
        {
            _logger.LogInformation($"Registering user: {user.Username}");

            var registration = await _userServce.RegisterUserAsync(user);

            if (string.IsNullOrEmpty(registration.Token)) _logger.LogInformation($"Successfully registered: {user.Username}");

            return Ok(registration);
        }

        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var token = await _userServce.LoginUserAsync(user);

            return Ok(token);
        }

        [HttpDelete]
        [Authorize]
        [MapToApiVersion(1.0)]
        [Route("[action]/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            await _userServce.DeleteUserAsync(username);

            return Ok();
        }

        [HttpPut]
        [MapToApiVersion(1.0)]
        [Route("[action]")]
        [Authorize]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserLogin user)
        {
            await _userServce.ChangeUserPasswordAsync(user.Username, user.Password);

            return Ok();
        }
    }
}