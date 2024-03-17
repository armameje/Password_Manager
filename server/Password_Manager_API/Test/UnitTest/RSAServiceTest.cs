using Microsoft.Extensions.Options;
using NUnit.Framework;
using NSubstitute;
using Password_Manager_API.Model;
using Password_Manager_API.Services;

namespace Password_Manager_API.Test.UnitTest
{
    public class RSAServiceTest
    {
        private IOptions<KeysOption> _options;
        private RSAService _rsaService;

        [OneTimeSetUp]
        public void SetUp()
        {
            var mockedKeyOption = new KeysOption
            {
                PublicKey = "C:\\Users\\Admin\\Downloads\\ProjectKeys\\public.pem",
                PrivateKey = "C:\\Users\\Admin\\Downloads\\ProjectKeys\\keep.pem"
            };
            _options = Options.Create<KeysOption>(mockedKeyOption);
            _rsaService = new RSAService(_options);
        }

        [Test]
        public void Encrypt_Success()
        {
            var encryptedText = _rsaService.Encrypt("Test string");
        }

        [Test]
        public void Decrypt_Success() 
        {
            var encryptedText = @"mIRoVUFCnhTfKoTUeJDsCBudZ4WeWCzNBYEewwJYMCmEotbOFDBSV9/Q/oGIFvpL7CXaAbkF+eqtI7dLvfViuFVE5W73y/1Cdfys/aQbDdaUixhCEcx+Umvgeo+lNFxcFLlCiLwDlLK98Z3ZE7dfY0pjInjHhhOzJI0L+DEMcsY=";

            var expectedText = "Test string";

            var result = _rsaService.Decrypt(encryptedText);
            Assert.That(result.Equals(expectedText));
        }
    }
}
