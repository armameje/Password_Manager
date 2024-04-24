﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
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
            var token = await _userServce.RegisterUserAsync(user);

            return Ok(token);
        }

        [HttpPost]
        [MapToApiVersion(1.0)]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var token = await _userServce.LoginUserAsync(user);

            return Ok(token);
        }
    }
}