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
    public class CabinetRepository : ICabinetInterface
    {
        private readonly string _connectionStringCabinet;
        public CabinetRepository(IConfiguration configuration)
        {
            _connectionStringCabinet = configuration.GetConnectionString("MainConnection");
        }
        public CabinetModel GetCabinet(string login)
        {
            CabinetModel cabinet = new CabinetModel();
            using (SqlConnection conn = new SqlConnection(_connectionStringCabinet))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetAgentByLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    cabinet.AgentId = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    cabinet.AgentName = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    cabinet.QueryCount = reader["QueryCount"] != DBNull.Value ? Convert.ToInt32(reader["QueryCount"]) : 0;
                    cabinet.Query = reader["Query"] != DBNull.Value ? Convert.ToInt32(reader["Query"]) : 0;
                    cabinet.IsTest = reader["IsTest"] != DBNull.Value ? Convert.ToBoolean(reader["IsTest"]) : false;
                }
            }
            return cabinet;
        }
        public List<QueryModel> GetQuery(QueryListData queryData, string login)
        {
            List<QueryModel> queries = new List<QueryModel>();
            using (SqlConnection conn = new SqlConnection(_connectionStringCabinet))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetQueryForAgent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
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

        public List<TarifModel> GetTarifByAgent(string login)
        {
            List<TarifModel> tarifs = new List<TarifModel>();
            using (SqlConnection conn = new SqlConnection(_connectionStringCabinet))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetTarifByAgent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TarifModel tarif = new TarifModel();
                    tarif.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    tarif.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty;
                    tarif.QueryCount = reader["QueryCount"] != DBNull.Value ? Convert.ToInt32(reader["QueryCount"]) : 0;
                    tarif.Sum = reader["Sum"] != DBNull.Value ? Convert.ToInt32(reader["Sum"]) : 0;
                    tarifs.Add(tarif);
                }
            }
            return tarifs;
        }
    }
}
