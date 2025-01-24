using PasswordManagerAPI.Repository;
using PasswordManagerAPI.Repository.Model;
using System.Text;

namespace PasswordManagerAPI.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository _platformRepo;
        private readonly IAsymmetricEncryption _encryption;

        public PlatformService(IPlatformRepository platformRepo, IAsymmetricEncryption encryption)
        {
            _platformRepo = platformRepo;
            _encryption = encryption;
        }

        public async Task AddPlatformAccountAsync(string username, string platformName, string platformUsername, string platformPassword)
        {
            var newPlatform = new PlatformDetails
            {
                Username = username,
                PlatformName = platformName,
                PlatformUsername = platformUsername
            };

            try
            {
                newPlatform.PlatformPassword = _encryption.Encrypt(platformPassword);

                await _platformRepo.AddPlatformAsync(newPlatform);
            }
            catch (Exception e)
            { 
            
            }
        }

        public async Task ChangePlatformAccountPasswordAsync(string username, string platformName, string platformUsername, string platformPassword)
        {
            var changePlatform = new PlatformDetails
            {
                Username = username,
                PlatformName = platformName,
                PlatformUsername = platformUsername,
                PlatformPassword = platformPassword
            };

            try
            {
                var storedUser = await GetPlatformAccountAsync(username, platformName, platformUsername);

                var storedPassword = _encryption.Decrypt(storedUser.PlatformPassword);

                if (storedPassword.Equals(platformPassword, StringComparison.Ordinal))
                {
                    throw new Exception("Same Password");
                }

                changePlatform.PlatformPassword = _encryption.Encrypt(platformPassword);

                await _platformRepo.ChangePlatformPasswordAsync(changePlatform);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task DeletePlatformAccountAsync(string username, string platformName, string platformUsername)
        {
            var platformToDelete = new PlatformDetailsNoPassword
            {
                Username = username,
                PlatformName = platformName,
                PlatformUsername = platformUsername
            };

            try
            {
                await _platformRepo.DeletePlatformAsync(platformToDelete);
            }
            catch (Exception e)
            { 
            
            }
        }

        public async Task<PlatformDetails> GetPlatformAccountAsync(string username, string platformName, string platformUsername)
        {
            var platformInfo = new PlatformDetails
            {
                Username = username,
                PlatformName = platformName,
                PlatformUsername = platformUsername
            };


            try
            {
                var details = await _platformRepo.GetPlatformInfoForUserAsync(platformInfo);

                if (details.PlatformPassword is null) throw new Exception();

                platformInfo.PlatformPassword = details.PlatformPassword;
            }
            catch (Exception e)
            {
                throw;
            }

            return platformInfo;
        }

        public async Task<List<PlatformDisplay>> GetAllPlatformsOfUserAsync(string username)
        {
            List<PlatformDisplay> allUserPlatforms = new();

            try
            {
                allUserPlatforms = await _platformRepo.GetAllPlatformsForUserAsync(username);

            }
            catch (Exception e)
            { 
            
            }

            return allUserPlatforms;
        }

        public async Task<string> RetrievePlatformPasswordAsync(string username, string platformName, string platformUsername)
        {
            string platformPassword = string.Empty;

            try
            {
                var details = await GetPlatformAccountAsync(username, platformName, platformUsername);
                var storedPassword = _encryption.Decrypt(details.PlatformPassword);

                platformPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(storedPassword));
            }
            catch (Exception e)
            {
                throw;
            }

            return platformPassword;
        }

        public string WrapPasswordByUsernameLength(int usernameLength, string password)
        {
            string wrappedPassword = password;

            if (usernameLength == 0)
            {
                return wrappedPassword;
            }

            usernameLength--;
            wrappedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            return WrapPasswordByUsernameLength(usernameLength, wrappedPassword);
        }
    }
}
