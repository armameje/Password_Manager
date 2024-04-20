using PasswordManagerAPI.Services.Models;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerAPI.Services.Utils
{
    public class HashingService : IHashingService
    {
        public SaltedPassword HashPassword(string password, int numberOfRounds, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            //byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            //Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            //Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            //byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            var hashedPassword = Convert.ToBase64String(ApplySaltRounds(passwordBytes, salt, numberOfRounds).passwordBytes);

            return new SaltedPassword
            {
                HashedPassword = hashedPassword,
                NumberOfSaltRounds = numberOfRounds,
                Salt = Convert.ToBase64String(salt)
            };
        }

        public void VerifyPassword(string password)
        {
            // Get UserAccount info from db
            // Generate the hashpassword; and compare
        }

        /// <summary>
        /// Generate random 16 length salt
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateSalt()
        {
            var rng = RandomNumberGenerator.Create();

            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Generate salted passwords based on numbers of salt rounds
        /// </summary>
        /// <param name="passwordBytes">Byte[] of salted password</param>
        /// <param name="salt"></param>
        /// <param name="numberOfRounds"></param>
        /// <param name="currentRound">Memoization of salt rounds</param>
        /// <returns></returns>
        private (byte[] passwordBytes, byte[] salt, int numberOfRounds) ApplySaltRounds(byte[] passwordBytes, byte[] salt, int numberOfRounds, int currentRound = 0)
        {
            if (currentRound == numberOfRounds) return (passwordBytes, salt, numberOfRounds);

            var sha256 = SHA256.Create();

            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            saltedPassword = sha256.ComputeHash(saltedPassword);
            currentRound++;

            return ApplySaltRounds(saltedPassword, salt, numberOfRounds, currentRound);
        }
    }
}
