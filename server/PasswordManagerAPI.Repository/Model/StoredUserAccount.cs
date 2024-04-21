using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerAPI.Repository.Model
{
    public class StoredUserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int NumberOfSaltRounds { get; set; }
    }
}
