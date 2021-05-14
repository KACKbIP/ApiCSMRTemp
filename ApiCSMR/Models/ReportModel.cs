using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Models
{
    public class ReportModel
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string IIN { get; set; }
        public decimal Result { get; set; }
        public DateTime SendDate { get; set; }
    }
}
