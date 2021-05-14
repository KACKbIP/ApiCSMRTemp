using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Interfaces
{
    public interface IGuardInterface
    {
        public bool IsValidAdmin(string token);
        public bool IsValid(string login, string password, string token);
        public bool IsValidApi(string login, string password, string token);
    }
}
