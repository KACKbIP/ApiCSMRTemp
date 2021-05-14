using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Models
{
    public class AgentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public int QueryCount { get; set; }
        public int Query { get; set; }
        public bool IsActive { get; set; }
        public bool IsTest { get; set; }
    }
    public class AgentResponse
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
    public class UpdateAgent
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool Check { get; set; }
    }
}
