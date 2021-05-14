using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Models
{
    public class TarifModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QueryCount { get; set; }
        public int Sum { get; set; }
    }
    public class TarifAgentModel
    {
        public int Id { get; set; }
        public int TarifId { get; set; }
        public string TarifName { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int TarifSum { get; set; }
    }
}
