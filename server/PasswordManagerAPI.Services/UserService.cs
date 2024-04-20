using PasswordManagerAPI.Services.Models;
using PasswordManagerAPI.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IHashingService _hashingService;

        public UserService(IHashingService hashingService)
        {
            _hashingService = hashingService;
        }

        public void RegisterUser(UserRegistration user)
        {
            try
            {
                
            }
            catch (Exception e)
            {

            }

        }

        public void LoginUser()
        { 
        
        }
    }
}
