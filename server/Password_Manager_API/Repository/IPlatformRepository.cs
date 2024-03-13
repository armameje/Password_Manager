using Password_Manager_API.Model;

namespace Password_Manager_API.Repository
{
    public interface IPlatformRepository
    {
        Task<Dictionary<string, int>> RetrieveAllPlatformAsync();
        Task AddPlatformAsync(string platformName);
        Task AddAccountToPlatformAsync(UserPlatformAccount account);
        Task<PlatformAccount> GetAllAccountsOfUserAsync(string username);
    }
}
