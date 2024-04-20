using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services
{
    public interface IUserService
    {
        Task RegisterUser(UserRegistration user);
        void LoginUser();
    }
}
