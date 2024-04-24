using PasswordManagerAPI.Repository.Model;
using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Services.Utils
{
    public interface IHashingService
    {
        SaltedPassword HashPassword(string password, int numberOfRounds, byte[] salt);
        bool VerifyPassword(string password, StoredUserAccount storedUserAccount);
        byte[] GenerateSalt();
    }
}
