using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services
{
    public interface IJwtProvider
    {
        string Generate(UserRegistration user);
    }
}
