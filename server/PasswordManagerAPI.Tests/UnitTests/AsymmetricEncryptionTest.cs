using Microsoft.Extensions.Options;
using PasswordManagerAPI.Services;
using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.Tests.UnitTests
{
    public class AsymmetricEncryptionTest
    {
        private readonly IAsymmetricEncryption _encryption;

        public AsymmetricEncryptionTest()
        {
            var pemOptions = new PEMOptions
            {
                PublicAddress = "C:\\Users\\Admin\\Downloads\\ProjectKeys\\public.pem",
                PrivateAddress = "C:\\Users\\Admin\\Downloads\\ProjectKeys\\keep.pem"
            };

            _encryption = new AsymmetricEncryption(Options.Create<PEMOptions>(pemOptions));
        }

        [Fact]
        public void Encrypt_Success()
        {
            var sampleText = "SampleEncrypt";
            var encryptedMessage = _encryption.Encrypt(sampleText);
        }

        [Fact]
        public void Decrypt_Success()
        {
            var toDecryptText = "botVoY68Exa38JGBzbof+0zgh8ndjvl0KZQLDqMyaUwuYiLrR3GvNxil3toqwluPNRX+rE6XMV4X3jz0HsR+Y2icIBQ88hkhtoKfdk/QB1huEskqfdqMB/4Kl2TyHSMi0RzVqJDv8VDNV4Wy67NZePFI5eN+shcqIEzslUhh7i8=";
            var decryptedText = _encryption.Decrypt(toDecryptText);
        }
    }
}
