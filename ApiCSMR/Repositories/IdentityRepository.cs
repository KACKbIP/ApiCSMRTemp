using ApiCSMR.Interfaces;
using ApiCSMR.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ApiCSMR.Repositories
{
    public class IdentityRepository : IIdentityInterface
    {
        private readonly string _connectionStringCabinet;
        private readonly string _certPath;
        private readonly string _certName;
        private readonly string _certPassword;
        private readonly string _token;
        private readonly string _username;
        private readonly string _password;
        private readonly string _tokenUrl;
        private readonly string _identityUrl;
        private readonly string _vendor;
        public IdentityRepository(IConfiguration configuration)
        {
            _connectionStringCabinet = configuration.GetConnectionString("MainConnection");
            _certPath = configuration["csmr-cert path"];
            _certName = configuration["csmr-cert name"];
            _certPassword = configuration["csmr-cert password"];
            _token = configuration["csmr-cert token"];
            _username = configuration["csmr username"];
            _password = configuration["csmr password"];
            _tokenUrl = configuration["csmr token url"];
            _identityUrl = configuration["csmr identity url"];
            _vendor = configuration["vendor"];
        }
        public List<ReportModel> GetReport(string login, DateTime dateBegin, DateTime dateTo)
        {
            List<ReportModel> reports = new List<ReportModel>();
            using (SqlConnection conn = new SqlConnection(_connectionStringCabinet))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetReportByLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@dateBegin", dateBegin);
                cmd.Parameters.AddWithValue("@dateTo", dateTo);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ReportModel report = new ReportModel();
                    report.IIN = reader["IIN"] != DBNull.Value ? Convert.ToString(reader["IIN"]) : string.Empty;
                    report.Result = reader["Result"] != DBNull.Value ? Convert.ToDecimal(reader["Result"]) : 0;
                    report.SendDate = reader["SendDate"] != DBNull.Value ? Convert.ToDateTime(reader["SendDate"]) : DateTime.MinValue;
                    reports.Add(report);
                }
            }
            return reports;
        }

        public List<ReportModel> GetReportByAgent(ReportData report)
        {
            List<ReportModel> reports = new List<ReportModel>();
            using (SqlConnection conn = new SqlConnection(_connectionStringCabinet))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.GetReportByAgent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@agentId", report.AgentId);
                cmd.Parameters.AddWithValue("@from", report.DateFrom);
                cmd.Parameters.AddWithValue("@to", report.DateTo);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ReportModel r = new ReportModel();
                    r.AgentId = reader["AgentId"] != DBNull.Value ? Convert.ToInt32(reader["AgentId"]) : 0;
                    r.AgentName = reader["AgentName"] != DBNull.Value ? Convert.ToString(reader["AgentName"]) : string.Empty;
                    r.IIN = reader["IIN"] != DBNull.Value ? Convert.ToString(reader["IIN"]) : string.Empty;
                    r.Result = reader["Result"] != DBNull.Value ? Convert.ToDecimal(reader["Result"]) : 0;
                    r.SendDate = reader["SendDate"] != DBNull.Value ? Convert.ToDateTime(reader["SendDate"]) : DateTime.MinValue;
                    reports.Add(r);
                }
            }
            return reports;
        }
        public void UpdateIdentification(int key, decimal result)
        {
            using (var cnn = new SqlConnection(_connectionStringCabinet))
            {
                using (SqlConnection conn = new SqlConnection(_connectionStringCabinet))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("dbo.UpdateIdentification", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@result", result);
                    cmd.Parameters.AddWithValue("@keyCSMR", "hggKey-" + key);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int GetIdentificationId(string IIN, string login)
        {
            using (var cnn = new SqlConnection(_connectionStringCabinet))
            {
                using (SqlConnection conn = new SqlConnection(_connectionStringCabinet))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("dbo.GetIdentificationId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IIN", IIN);
                    cmd.Parameters.AddWithValue("@login", login);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public decimal GetIdentification(IdentificationModel identification, int key)
        {
            try
            {
                if (key == 0)
                {
                    return 0.995125M;
                }
                else
                {
                    var handler = new HttpClientHandler();
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cert\\hgg.p12");
                    handler.ClientCertificates.Add(new X509Certificate2(path, _certPassword));

                    using (var httpClient = new HttpClient(handler))
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), _identityUrl + "iin=" + identification.IIN + "&vendor=" + _vendor))
                        {
                            string token = GetToken();
                            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                            request.Headers.TryAddWithoutValidation("x-idempotency-key", "key:" + "hggKey-" + key);
                            request.Method = new HttpMethod("POST");
                            //request.Content = new StringContent(identification.Photo);
                            request.Content = new StreamContent(new MemoryStream(Convert.FromBase64String(identification.Photo)));
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");

                            var response = httpClient.SendAsync(request);
                            string data = response.Result.Content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<dynamic>(data).result;
                        }
                    }
                }
            }
            catch 
            {
                throw new Exception("Не удалось провести идентификацию");
            }
        }
        public string GetToken()
        {
            var handler = new HttpClientHandler();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cert\\hgg.p12");
            handler.ClientCertificates.Add(new X509Certificate2(path, _certPassword));

                using (var httpClient = new HttpClient(handler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), _tokenUrl))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", "Basic " + _token);
                        request.Method = new HttpMethod("POST");
                        request.Content = new StringContent("grant_type=password&username=" + _username + "&password=" + _password + "&scope=identkey");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                        var response = httpClient.SendAsync(request);

                        return JsonConvert.DeserializeObject<dynamic>(response.Result.Content.ReadAsStringAsync().Result).access_token;
                    }
                }
        }
    }
}
