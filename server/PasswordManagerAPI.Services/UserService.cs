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

        public async Task<RegistrationResponse> RegisterUserAsync(UserRegistration user)
        {
            var registration = new RegistrationResponse();

            try
            {
                var isUsernameTaken = await _userRepo.IsUsernameTakenAsync(user.Username);

                if (isUsernameTaken)
                {
                    registration.IsSuccess = false;
                    registration.Error = "Username is taken";

                    return registration;
                }

                var salt = _hashingService.GenerateSalt();
                var numberOfSaltRounds = new Random().Next(2, 6);

                var saltedPassword = _hashingService.HashPassword(user.Password, numberOfSaltRounds, salt);

                await _userRepo.RegisterUserAsync(user.Username, saltedPassword.HashedPassword, saltedPassword.Salt, saltedPassword.NumberOfSaltRounds);

                registration.Token = _jwtProvider.Generate(user);
                registration.IsSuccess = true;
            }
            catch (Exception e)
            {

            }

            return registration;
        }

        public async Task<LoginResponse> LoginUserAsync(UserLogin user)
        {
            var login = new LoginResponse();

            try
            {
                var storedUserAccount = await _userRepo.RetrieveUserByUsernameAsync(user.Username);

                if (storedUserAccount.Password is null && storedUserAccount.Salt is null) throw new Exception();

                var validUser = _hashingService.VerifyPassword(user.Password, storedUserAccount);

                if (validUser) 
                {
                    login.IsSuccess = true;
                    login.Token = _jwtProvider.Generate(user);

                    return login;
                }
            }
            catch (Exception e)
            { 
              
            }

            login.IsSuccess = false;
            login.Error = "Incorrect username or password";

            return login;
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
