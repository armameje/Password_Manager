using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Services
{
    public interface IPlatformService
    {
        Task AddPlatformAccountAsync(string username, string platformName, string platformUsername, string platformPassword);
        Task<PlatformDetails> GetPlatformAccountAsync(string username, string platformName, string platformUsername);
        Task ChangePlatformAccountPasswordAsync(string username, string platformName, string platformUsername, string platformPassword);
        Task DeletePlatformAccountAsync(string username, string platformName, string platformUsername);
    }
}
