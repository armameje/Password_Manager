using Password_Manager_API.Model;

namespace Password_Manager_API.Services
{
    public interface IPlatformService
    {
        Task<Dictionary<string, int>> GetAllPlatformsAsync();
        Task<List<PlatformAccount>> GetAllAccountsOfUserAsync(string username);
        Task AddPlatformAsync(string platformName);
        Task AddAccountToPlatformAsync(UserPlatformAccount account);
    }
}
