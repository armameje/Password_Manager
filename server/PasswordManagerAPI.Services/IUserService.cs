using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services
{
    public interface IUserService
    {
        void RegisterUser(UserRegistration user);
        void LoginUser();
    }
}
