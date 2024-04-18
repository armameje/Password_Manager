using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services
{
    public interface IHashingService
    {
        SaltedPassword HashPassword(string password, int numberOfRounds);
        void VerifyPassword(string password);
    }
}
