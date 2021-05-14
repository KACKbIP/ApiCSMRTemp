using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public bool IsTest { get; set; }

        public class AccountLoginModel
        {
            public string Login { get; set; }
            public string Token { get; set; }
        }
    }
}
