using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Models
{
    public class RequestClass<T>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public T Data { get; set; }
    }
    public class ReportData
    {
        public int AgentId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
    public class ChangeData
    {
        public string NewPassword { get; set; }
    }
    public class AgentData
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public bool IsTest { get; set; }
    }
    public class QueryData
    {
        public int AgentId { get; set; }
        public int QueryCount { get; set; }
        public string FileBite { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
    }
    public class QueryListData
    {
        public int AgentId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
