using ApiCSMR.Interfaces;
using ApiCSMR.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Repositories
{
    public class AdminRepository : IAdminInterface
    {
        private readonly string _connectionStringAdmin;
        public AdminRepository(IConfiguration configuration)
        {
            _connectionStringAdmin = configuration.GetConnectionString("MainConnection");
        }
        public AgentResponse AddAgent(string login, string password, string name, bool isTest)
        {
            string token = HelperRepository.EncrypteText(login + DateTime.Now + password + name).Substring(0,60);
            bool isAdd = false;
            AgentResponse agent = new AgentResponse();
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.AddAgent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", HelperRepository.EncrypteText(password));
                cmd.Parameters.AddWithValue("@isTest", isTest);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@token", token);
                isAdd= Convert.ToBoolean(cmd.ExecuteScalar());
            }
            if (isAdd)
            {
                agent.Login = login;
                agent.Token = token;
                agent.Password  = password;
                return agent;
            }
            else
            {
                throw new Exception("Не удалось добавить агента");
            }
               
        }
        public List<AgentModel> GetAgents()
        {
            List<AgentModel> agents = new List<AgentModel>();
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetAgents", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AgentModel agent = new AgentModel();
                    agent.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    agent.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    agent.Login = reader["Login"] != DBNull.Value ? Convert.ToString(reader["Login"]) : string.Empty;
                    agent.Query = reader["Query"] != DBNull.Value ? Convert.ToInt32(reader["Query"]) : 0;
                    agent.QueryCount = reader["QueryCount"] != DBNull.Value ? Convert.ToInt32(reader["QueryCount"]) : 0;
                    agent.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false;
                    agent.IsTest = reader["IsTest"] != DBNull.Value ? Convert.ToBoolean(reader["IsTest"]) : false;
                    agents.Add(agent);
                }
            }
            return agents;
        }
        public bool AddQuery(QueryData query)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.AddQuery", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@agentId", query.AgentId);
                cmd.Parameters.AddWithValue("@queryCount", query.QueryCount);
                cmd.Parameters.AddWithValue("@fileBite", query.FileBite);
                cmd.Parameters.AddWithValue("@fileName", query.FileName);
                cmd.Parameters.AddWithValue("@fileType", query.FileType);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            } 
        }
        public List<QueryModel> GetQuery(QueryListData queryData)
        {
            List<QueryModel> queries = new List<QueryModel>();
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetQuery", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@agentId", queryData.AgentId);
                cmd.Parameters.AddWithValue("@from", queryData.From);
                cmd.Parameters.AddWithValue("@to", queryData.To);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    QueryModel query = new QueryModel();
                    query.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    query.AgentId = reader["AgentId"] != DBNull.Value ? Convert.ToInt32(reader["AgentId"]) : 0;
                    query.AgentName = reader["AgentName"] != DBNull.Value ? Convert.ToString(reader["AgentName"]) : string.Empty;
                    query.AddDate = reader["AddDate"] != DBNull.Value ? Convert.ToDateTime(reader["AddDate"]) : DateTime.MinValue;
                    query.QueryCount = reader["QueryCount"] != DBNull.Value ? Convert.ToInt32(reader["QueryCount"]) : 0;
                    query.FileBite = reader["FileBite"] != DBNull.Value ? Convert.ToString(reader["FileBite"]) : string.Empty;
                    query.FileName = reader["FileName"] != DBNull.Value ? Convert.ToString(reader["FileName"]) : string.Empty;
                    query.FileType = reader["FileType"] != DBNull.Value ? Convert.ToString(reader["FileType"]) : string.Empty;
                    queries.Add(query);
                }
            }
            return queries;
        }
        public List<TarifModel> GetTarifs()
        {
            List<TarifModel> tarifs = new List<TarifModel>();
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetTarifs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TarifModel tarif = new TarifModel();
                    tarif.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    tarif.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    tarif.QueryCount = reader["QueryCount"] != DBNull.Value ? Convert.ToInt32(reader["QueryCount"]) : 0;
                    tarifs.Add(tarif);
                }
            }
            return tarifs;
        }

        public List<TarifAgentModel> GetAgentTarifs()
        {
            List<TarifAgentModel> agentTarifs = new List<TarifAgentModel>();
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetAgentTarifs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TarifAgentModel agentTarif = new TarifAgentModel();
                    agentTarif.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    agentTarif.TarifId = reader["TarifId"] != DBNull.Value ? Convert.ToInt32(reader["TarifId"]) : 0;
                    agentTarif.AgentId = reader["AgentId"] != DBNull.Value ? Convert.ToInt32(reader["AgentId"]) : 0;
                    agentTarif.TarifSum = reader["TarifSum"] != DBNull.Value ? Convert.ToInt32(reader["TarifSum"]) : 0;
                    agentTarif.TarifName = reader["TarifName"] != DBNull.Value ? Convert.ToString(reader["TarifName"]) : string.Empty;
                    agentTarif.AgentName = reader["AgentName"] != DBNull.Value ? Convert.ToString(reader["AgentName"]) : string.Empty;
                    agentTarifs.Add(agentTarif);
                }
            }
            return agentTarifs;
        }

        public bool AddTarif(TarifModel tarif)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.AddTarif", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", tarif.Name);
                cmd.Parameters.AddWithValue("@queryCount", tarif.QueryCount);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public bool AddTarifAgent(TarifAgentModel tarif)
        {
            if (tarif.Id == null)
            {
                using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("dbo.AddTarifAgent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@agentId", tarif.AgentId);
                    cmd.Parameters.AddWithValue("@tarifId", tarif.TarifId);
                    cmd.Parameters.AddWithValue("@tarifSum", tarif.TarifSum);
                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("dbo.ChangeTarifAgent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", tarif.Id);
                    cmd.Parameters.AddWithValue("@tarifSum", tarif.TarifSum);
                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
        }
        public bool DeleteTarif(TarifModel tarif)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.DeleteTarif", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", tarif.Id);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public void UpdateAgent(UpdateAgent data)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStringAdmin))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.UpdateAgent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", data.Id);
                cmd.Parameters.AddWithValue("@check", data.Check);
                cmd.Parameters.AddWithValue("@isActive", data.IsActive);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
