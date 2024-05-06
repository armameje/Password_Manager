namespace PasswordManagerAPI.Services
{
    public interface IAsymmetricEncryption
    {
        string Encrypt(string text);
        string Decrypt(string text);
    }
}
