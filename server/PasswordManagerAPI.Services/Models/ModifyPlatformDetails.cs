using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerAPI.Services.Models
{
    public class ModifyPlatformDetails : PlatformAccount
    {
        public string NewUsername { get; set; }
    }
}
