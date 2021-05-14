using ApiCSMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Interfaces
{
    public interface IAdminInterface
    {
        AgentResponse AddAgent(string login, string password, string name, bool isTest);
        List<AgentModel> GetAgents();
        bool AddQuery(QueryData query);
        List<QueryModel> GetQuery(QueryListData queryData);
        List<TarifAgentModel> GetAgentTarifs();
        List<TarifModel> GetTarifs();
        bool AddTarif(TarifModel tarif);
        bool AddTarifAgent(TarifAgentModel tarif);
        bool DeleteTarif(TarifModel tarif);
        void UpdateAgent(UpdateAgent data);
    }
}
