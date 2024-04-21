using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(UserRegistration user);
        Task LoginUser(UserLogin user);
    }
}
