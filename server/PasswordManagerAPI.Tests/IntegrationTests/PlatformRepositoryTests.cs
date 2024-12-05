using Microsoft.Extensions.Options;
using PasswordManagerAPI.Repository;
using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.Tests.IntegrationTests
{
    public class PlatformRepositoryTests
    {
        private readonly IPlatformRepository _platformRepo;


        public PlatformRepositoryTests()
        {
            var passwordManagerCS = new PasswordManagerCSOptions
            {
                ConnectionString = "Server=PSUEDOENGINEERD\\TEW_SQLEXPRESS;Database=PasswordManager;Trusted_Connection=True;Encrypt=False;"
            };

            _platformRepo = new PlatformRepository(Options.Create<PasswordManagerCSOptions>(passwordManagerCS));
        }

        [Fact]
        public async Task GetPlatformInfoForUserAsync_Success()
        {
            var test = new PlatformDetailsNoPassword
            {
                Username = "erenboi",
                PlatformName = "Facebook",
                PlatformUsername = "xdd"
            };

            var x = await _platformRepo.GetPlatformInfoForUserAsync(test);
        }

        [Fact]
        public async Task GetAllPlatformsForUserAsync_Success()
        {
            string username = "erenboi";

            var platformsForUser = await _platformRepo.GetAllPlatformsForUserAsync(username);

            Assert.True(platformsForUser.Count.Equals(2));
        }
    }
}
