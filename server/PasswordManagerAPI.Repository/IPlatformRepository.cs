using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Repository
{
    public interface IPlatformRepository
    {
        Task UpsertPlatformAsync(PlatformDetails platform);
        Task AddPlatformAsync(PlatformDetails platform);
        Task UpdatePlatformAsync(ModifyPlatform platform);
        Task<PlatformDetails> GetPlatformInfoForUserAsync(PlatformDetailsNoPassword platform);
        Task DeletePlatformAsync(PlatformDetailsNoPassword platform);
        Task ChangePlatformPasswordAsync(PlatformDetails platform);
        Task<List<PlatformDisplay>> GetAllPlatformsForUserAsync(string username);
    }
}
