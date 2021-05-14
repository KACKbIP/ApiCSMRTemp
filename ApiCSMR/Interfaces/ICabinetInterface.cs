using ApiCSMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Interfaces
{
    public interface ICabinetInterface
    {
        public CabinetModel GetCabinet(string login);
        List<QueryModel> GetQuery(QueryListData queryData, string login);
        List<TarifModel> GetTarifByAgent(string login);
    }
}
