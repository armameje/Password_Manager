using PasswordManagerAPI.Repository;
using PasswordManagerAPI.Repository.Model;
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
        private readonly IJwtProvider _jwtProvider;

        public UserService(IHashingService hashingService, IUserRepository userRepo, IJwtProvider jwtProvider)
        {
            _hashingService = hashingService;
            _userRepo = userRepo;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> RegisterUserAsync(UserRegistration user)
        {
            string token = string.Empty;

            try
            {
                var isUsernameTaken = await _userRepo.IsUsernameTakenAsync(user.Username);

                if (isUsernameTaken) throw new Exception();

                var salt = _hashingService.GenerateSalt();
                var numberOfSaltRounds = new Random().Next(2, 6);

                var saltedPassword = _hashingService.HashPassword(user.Password, numberOfSaltRounds, salt);

                await _userRepo.RegisterUserAsync(user.Username, saltedPassword.HashedPassword, saltedPassword.Salt, saltedPassword.NumberOfSaltRounds);

                token = _jwtProvider.Generate(user);
            }
            catch (Exception e)
            {

            }

            return token;
        }

        public async Task<string> LoginUserAsync(UserLogin user)
        {
            string token = string.Empty;

            try
            {
                var storedUserAccount = await _userRepo.RetrieveUserByUsernameAsync(user.Username);

                if (storedUserAccount.Password is null && storedUserAccount.Salt is null) throw new Exception();

                var validUser = _hashingService.VerifyPassword(user.Password, storedUserAccount);

                if (validUser) 
                {
                    token = _jwtProvider.Generate(user);

                    return token;
                }
            }
            catch (Exception e)
            { 
              
            }

            return token = "Incorrect username or password";
        }

        public async Task DeleteUserAsync(string username)
        {
            try
            {
                await _userRepo.DeleteUserByUsernameAsync(username);
            }
            catch (Exception e)
            { 
            
            }
        }

        public async Task ChangeUserPasswordAsync(string username, string password)
        {
            try
            {
                var salt = _hashingService.GenerateSalt();
                var numberOfRounds = new Random().Next(2, 6);
                var hashedPassword = _hashingService.HashPassword(password, numberOfRounds, salt);

                var newPassword = new ChangeUserPassword
                { 
                    Username = username,
                    Password = hashedPassword.HashedPassword,
                    Salt = hashedPassword.Salt,
                    NumberOfSaltRounds = hashedPassword.NumberOfSaltRounds
                };

                await _userRepo.ChangePasswordByUsernameAsync(newPassword);
            }
            catch (Exception e)
            {

            }
        }
    }
}
