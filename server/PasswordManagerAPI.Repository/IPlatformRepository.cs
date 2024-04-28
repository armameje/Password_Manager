using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Repository
{
    public interface IPlatformRepository
    {
        Task AddPlatformAsync(PlatformDetails platform);
        Task GetPlatformInfoForUserAsync(PlatformDetails platform);
        Task DeletePlatformAsync(PlatformDetails platform);
        Task ChangePlatformPasswordAsync(PlatformDetails platform);
    }
}
