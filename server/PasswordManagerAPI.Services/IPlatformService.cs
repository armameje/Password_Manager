using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Services
{
    public interface IPlatformService
    {
        Task AddPlatformAccountAsync(string username, string platformName, string platformUsername, string platformPassword);
        Task<PlatformDetails> GetPlatformAccountAsync(string username, string platformName, string platformUsername);
        Task ChangePlatformAccountPasswordAsync(string username, string platformName, string platformUsername, string platformPassword);
        Task DeletePlatformAccountAsync(string username, string platformName, string platformUsername);
        Task<List<PlatformDisplay>> GetAllPlatformsOfUserAsync(string username);
        Task<string> RetrievePlatformPasswordAsync(string username, string platformName, string platformUsername);
        string WrapPasswordByUsernameLength(int usernameLength, string password);
    }
}
