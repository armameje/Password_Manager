using PasswordManagerAPI.Repository;
using PasswordManagerAPI.Services;
using NSubstitute;

namespace PasswordManagerAPI.Tests.UnitTests
{
    public class PlatformServiceTest
    {
        private readonly IPlatformService _platformService;
        private readonly IPlatformRepository _platformRepo;
        private readonly IAsymmetricEncryption _encryption;

        public PlatformServiceTest()
        {
            _platformRepo = Substitute.For<IPlatformRepository>();
            _encryption = Substitute.For<IAsymmetricEncryption>();
            _platformService = new PlatformService(_platformRepo, _encryption);
        }

        [Fact]
        public void WrapPasswordByUsernameLength_Success()
        {
            _platformService.WrapPasswordByUsernameLength(5, "test");

            var expectedText = "VjJ0V2ExWXlWblJWYTBwUlZrUkJPUT09";

            Assert.True(expectedText.Equals(expectedText));
        }
    }
}
