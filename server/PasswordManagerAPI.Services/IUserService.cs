using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(UserRegistration user);
        Task<string> LoginUserAsync(UserLogin user);
    }
}
