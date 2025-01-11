using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Repository
{
    public interface IPlatformRepository
    {
        Task AddPlatformAsync(PlatformDetails platform);
        Task<PlatformDetails> GetPlatformInfoForUserAsync(PlatformDetailsNoPassword platform);
        Task DeletePlatformAsync(PlatformDetailsNoPassword platform);
        Task ChangePlatformPasswordAsync(PlatformDetails platform);
        Task<List<PlatformDisplay>> GetAllPlatformsForUserAsync(string username);
    }
}
