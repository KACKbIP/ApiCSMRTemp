using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiCSMR.Models.AccountModel;

namespace ApiCSMR.Interfaces
{
    public interface IAccountInterface
    {
        public AccountLoginModel GetAccount(string login, string password);
        bool ChangePassword(string login, string newPassword);
    }
}
