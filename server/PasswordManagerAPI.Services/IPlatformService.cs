using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerAPI.Services
{
    public interface IPlatformService
    {
        void AddPlatformAccount();
        void GetPlatformAccount();
    }
}
