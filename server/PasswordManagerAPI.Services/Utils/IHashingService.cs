using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services.Utils
{
    public interface IHashingService
    {
        SaltedPassword HashPassword(string password, int numberOfRounds, byte[] salt);
        void VerifyPassword(string password);
        byte[] GenerateSalt();
    }
}
