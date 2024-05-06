﻿using PasswordManagerAPI.Repository;
using PasswordManagerAPI.Repository.Model;

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
                var storedUser = await _platformRepo.GetPlatformInfoForUserAsync(changePlatform);
            }
            catch (Exception e)
            { 
                
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
            var platformInfo = new PlatformDetailsNoPassword
            {
                Username = username,
                PlatformName = platformName,
                PlatformUsername = platformUsername
            };

            var platformDetails = new PlatformDetails();

            try
            {
                platformDetails = await _platformRepo.GetPlatformInfoForUserAsync(platformInfo);
            }
            catch (Exception e)
            { 
            
            }

            return platformDetails;
        }
    }
}
