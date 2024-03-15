using System.Security.Cryptography;

namespace Password_Manager_API.Services
{
    public interface IRSAService
    {
        string Encrypt(string text);
        string Decrypt(string text);
        RSACryptoServiceProvider GetPublicKeyFromPem(string filePath);
        RSACryptoServiceProvider GetPrivateKeyFromPem(string filePath);
    }
}
