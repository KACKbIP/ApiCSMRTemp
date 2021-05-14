using ApiCSMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Interfaces
{
    public interface IIdentityInterface
    {
        List<ReportModel> GetReport(string login, DateTime dateBegin, DateTime dateTo);
        List<ReportModel> GetReportByAgent(ReportData report);
        int GetIdentificationId(string IIN, string login);
        decimal GetIdentification(IdentificationModel identification, int key);
        void UpdateIdentification(int key, decimal result);
    }
}
