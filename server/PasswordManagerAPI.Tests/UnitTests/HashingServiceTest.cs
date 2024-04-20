using PasswordManagerAPI.Services.Utils;

namespace PasswordManagerAPI.Tests.UnitTests
{
    public class HashingServiceTest
    {
        private readonly IHashingService _hashingService;

        public HashingServiceTest()
        {
            _hashingService = new HashingService();
        }

        [Fact]
        public void SuccessHashing_HashPassword()
        {
            var unecryptedPassword = "Unsafe password";

            _hashingService.HashPassword(unecryptedPassword, 1);
        }
    }
}
