using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Models
{
    public class CabinetModel
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int QueryCount { get; set; }
        public int Query { get; set; }
        public bool IsTest { get; set; }
    }
}
