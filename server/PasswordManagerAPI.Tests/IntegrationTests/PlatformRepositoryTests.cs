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

        [Fact]
        public async Task AddPlatformAsync_Success()
        {
            var platform = new PlatformDetails
            {
                Username = "jenisi",
                PlatformName = "Gmail",
                PlatformUsername = "jackyasuo15@gmail.com",
                PlatformPassword = "asds"
            };

            await _platformRepo.AddPlatformAsync(platform);
        }

        [Fact]
        public async Task ModifyPlatformAsync_Success()
        {
            var platform = new ModifyPlatform
            {
                Username = "jenisi",
                PlatformName = "Gmail",
                PlatformUsername = "test",
                NewPlatformUsername = "jackyasuo15@gmail.com",
                PlatformPassword = "asdsd"
            };

            await _platformRepo.UpdatePlatformAsync(platform);
        }
    }
}
