﻿using PasswordManagerAPI.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Tests.IntegrationTests
{
    public class UserRepositoryTest
    {
        private readonly IUserRepository _userRepo;

        public UserRepositoryTest()
        {
            var passwordManagerCS = new PasswordManagerCS
            {
                ConnectionString = "Server=PSUEDOENGINEERD\\TEW_SQLEXPRESS;Database=PasswordManager;Trusted_Connection=True;Encrypt=False;"
            };

            _userRepo = new UserRepository(Options.Create<PasswordManagerCS>(passwordManagerCS));
        }

        [Fact]
        public async Task RetrieveUserByUsernameAsync_Success()
        {
            await _userRepo.RetrieveUserByUsernameAsync("FirstUser");
        }

        [Fact]
        public async Task RegisterUserAsync_Fail()
        {
            await _userRepo.RegisterUserAsync("FirstUser", "testsalt", "sdasd", 3);
        }

        [Fact]
        public async Task IsUsernameTaken()
        {
            await _userRepo.IsUsernameTakenAsync("FirstUser");
        }
    }
}