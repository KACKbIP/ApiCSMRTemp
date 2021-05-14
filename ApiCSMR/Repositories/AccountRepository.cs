using ApiCSMR.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static ApiCSMR.Models.AccountModel;

namespace ApiCSMR.Repositories
{
    public class AccountRepository : IAccountInterface
    {
        private readonly string _connectionStringAccount;
        public AccountRepository(IConfiguration configuration)
        {
            _connectionStringAccount = configuration.GetConnectionString("MainConnection");
        }
        public AccountLoginModel GetAccount(string login, string password)
        {
            AccountLoginModel account = new AccountLoginModel();
            using (SqlConnection conn = new SqlConnection(_connectionStringAccount))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetAccount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", HelperRepository.EncrypteText(password));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    account.Login  = reader["Login"] != DBNull.Value ? Convert.ToString(reader["Login"]) : string.Empty;
                    account.Token = HelperRepository.EncrypteText(reader["Token"] != DBNull.Value ? Convert.ToString(reader["Token"]) : string.Empty);
                }
            }
            return account;
        }
        public bool ChangePassword(string login, string newPassword)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStringAccount))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.ChangePassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@newPassword", HelperRepository.EncrypteText(newPassword));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
