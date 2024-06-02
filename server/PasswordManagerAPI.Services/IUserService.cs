using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services
{
    public interface IUserService
    {
        Task<RegistrationResponse> RegisterUserAsync(UserRegistration user);
        Task<LoginResponse> LoginUserAsync(UserLogin user);
        Task DeleteUserAsync(string username);
        Task ChangeUserPasswordAsync(string username, string password);
    }
}
