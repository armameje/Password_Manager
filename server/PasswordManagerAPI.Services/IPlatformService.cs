using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Services
{
    public interface IPlatformService
    {
        Task AddPlatformAccountAsync(string username, string platformName, string platformUsername, string platformPassword);
        Task ModifyPlatformAccountAsync(string username, string platformName, string platformUsername, string newPlatformUsername, string platformPassword);
        Task<PlatformDetails> GetPlatformAccountAsync(string username, string platformName, string platformUsername);
        Task DeletePlatformAccountAsync(string username, string platformName, string platformUsername);
        Task<List<PlatformDisplay>> GetAllPlatformsOfUserAsync(string username);
        Task<string> RetrievePlatformPasswordAsync(string username, string platformName, string platformUsername);
        string GetKagi();
    }
}
