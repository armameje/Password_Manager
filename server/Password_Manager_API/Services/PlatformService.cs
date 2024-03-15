using Password_Manager_API.Model;
using Password_Manager_API.Repository;

namespace Password_Manager_API.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository _platformRepo;

        public PlatformService(IPlatformRepository platformRepository)
        {
            _platformRepo = platformRepository;
        }

        public async Task<Dictionary<string, int>> GetAllPlatformsAsync() => await _platformRepo.RetrieveAllPlatformAsync();

        public async Task<List<PlatformAccount>> GetAllAccountsOfUserAsync(string username)
        {
            var userAccounts = new List<PlatformAccount>();
            try
            {
                userAccounts = await _platformRepo.GetAllAccountsOfUserAsync(username);
            }
            catch (Exception e)
            {
                
            }

            return userAccounts;
        }

        public async Task AddPlatformAsync(string platformName)
        {
            try
            {
                await _platformRepo.AddPlatformAsync(platformName);
            }
            catch (Exception e)
            { 

            }
        }

        public async Task AddAccountToPlatformAsync(UserPlatformAccount account)
        {

        }
    }
}
