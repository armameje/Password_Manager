﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PasswordManagerAPI.Services.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerAPI.Services
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public string Generate()
        {
            var claims = new Claim[] 
            { 
                new(JwtRegisteredClaimNames.UniqueName, )
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                null);

            string tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }
    }
}
