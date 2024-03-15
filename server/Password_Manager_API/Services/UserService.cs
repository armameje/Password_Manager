using Password_Manager_API.Controllers;
using Password_Manager_API.Model;
using Password_Manager_API.Repository;

namespace Password_Manager_API.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;

        public UserService(ILogger<UserController> logger, IUserRepository userRepo, IHashingService hashingService)
        {
            _logger = logger;
            _userRepository = userRepo;
            _hashingService = hashingService;
        }

        public async Task Register(UserLogin user)
        {
            try
            {
                var hashedPassword = _hashingService.HashPassword(user.Password);
                await _userRepository.RegisterUserAsync(user.Username, hashedPassword);

                _logger.LogInformation($"Registered User: {user.Username}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public async Task Login(UserLogin user)
        {
            try
            {
                var storedUserInfo = await _userRepository.RetrieveUserAsync(user.Username);

            }
            catch (Exception e)
            { 
                
            }
        }
    }
}
