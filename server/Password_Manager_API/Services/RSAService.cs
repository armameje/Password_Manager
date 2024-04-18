using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Password_Manager_API.Model;
using System.Security.Cryptography;
using System.Text;

namespace Password_Manager_API.Services
{
    public class RSAService : IRSAService
    {
        private readonly ILogger<IRSAService> _logger;
        private readonly RSACryptoServiceProvider _publicKey;
        private readonly RSACryptoServiceProvider _privateKey;

        public RSAService(IOptions<KeysOption> options, ILogger<IRSAService> logger)
        {
            var optionsValue = options.Value;
            _publicKey = GetPublicKeyFromPem(optionsValue.PublicKey);
            _privateKey = GetPrivateKeyFromPem(optionsValue.PrivateKey);
            _logger = logger;
        }

        public string Encrypt(string text)
        {
            _logger.LogInformation("Encrypting text {text}", text);
            var encryptedBytes = _publicKey.Encrypt(Encoding.UTF8.GetBytes(text), false);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string text) 
        {
            var decryptedBytes = _privateKey.Decrypt(Convert.FromBase64String(text), false);
            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        }

        private static RSACryptoServiceProvider GetPublicKeyFromPem(string filePath)
        {
            using (TextReader reader = new StreamReader(filePath))
            {
                RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(reader).ReadObject();

                RSAParameters rSAParameters = DotNetUtilities.ToRSAParameters(publicKeyParam);
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rSAParameters);
                return csp;
            }
        }

        private static RSACryptoServiceProvider GetPrivateKeyFromPem(string filePath)
        {
            var privateText = File.ReadAllText(filePath);
            using (TextReader reader = new StringReader(privateText))
            {
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }
    }
}
