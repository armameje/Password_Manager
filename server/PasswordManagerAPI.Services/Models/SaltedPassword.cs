using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerAPI.Services.Models
{
    public class SaltedPassword
    {
        public string HashedPassword { get; set; }
        public int NumberOfSaltRounds { get; set; }

        // Convert byte array to Base64String to store; convert from Base64String when to use
        public string Salt { get; set; }
    }
}
