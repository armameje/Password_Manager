using PasswordManagerAPI.Repository;
using PasswordManagerAPI.Services.Models;
using PasswordManagerAPI.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IHashingService _hashingService;
        private readonly IUserRepository _userRepo;

        public UserService(IHashingService hashingService, IUserRepository userRepo)
        {
            _hashingService = hashingService;
            _userRepo = userRepo;
        }

        public async Task RegisterUser(UserRegistration user)
        {
            try
            {
                var isUsernameTaken = await _userRepo.IsUsernameTakenAsync(user.Username);

                if (isUsernameTaken) return;

                var salt = _hashingService.GenerateSalt();
                var numberOfSaltRounds = new Random().Next(2, 6);

                var saltedPassword = _hashingService.HashPassword(user.Password, numberOfSaltRounds, salt);

                await _userRepo.RegisterUserAsync(user.Username, saltedPassword.HashedPassword, saltedPassword.Salt, saltedPassword.NumberOfSaltRounds);
            }
            catch (Exception e)
            {

            }

        }

        public void LoginUser()
        { 
        
        }
    }
}
