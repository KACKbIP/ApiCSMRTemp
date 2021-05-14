using ApiCSMR.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCSMR.Repositories
{
    public class GuardRepository : IGuardInterface
    {
        private readonly string _connectionStringGuard;
        public static string adminToken = "P7ktrav5PL5BgPHTxF7eTFTmbtaMWpPpvzZJmtDSDwEgACQcqdxaNLta8CcZ";
        public GuardRepository(IConfiguration configuration)
        {
            _connectionStringGuard = configuration.GetConnectionString("MainConnection");
        }
        public bool IsValidAdmin(string token)
        {
            if (token != adminToken)
                return false;
            else
                return true;
        }
        public bool IsValid(string login, string password, string token)
        {            
            if (token == adminToken)
                return true;
            return IsValidApi(login, HelperRepository.EncrypteText(password), token);
        }

        //public bool IsValidLogin(string login, string password)
        //{
        //    bool isValid=false;
        //    using (SqlConnection conn = new SqlConnection(_connectionStringGuard))
        //    {
        //        conn.Open();

        //        SqlCommand cmd = new SqlCommand("dbo.GetValidateLoginPassword", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@login", login);
        //        cmd.Parameters.AddWithValue("@password", password);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {                    
        //            isValid = reader["isValid"] != DBNull.Value ? Convert.ToBoolean(reader["isValid"]) : false;
        //        }
        //    }
        //    return isValid;
        //}
        public bool IsValidApi(string login, string password, string token)
        {
            bool isValid = false;
            using (SqlConnection conn = new SqlConnection(_connectionStringGuard))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetValidateApi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@token", token);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    isValid = reader["isValid"] != DBNull.Value ? Convert.ToBoolean(reader["isValid"]) : false;
                }
            }
            return isValid;
        }
    }
}
