using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public class LoginRepo : ILoginRepo
    {
        private readonly string _connectionString;
        public LoginRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<string>> checkUsernamePassword(string username, string password)
        {
            List<string> result = new();
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT CustomerId, Username, Password, IsManager
                               FROM Login
                               WHERE Username = @username AND Password = @password";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    result.Add(reader.GetInt32(0).ToString());
                    result.Add(reader.GetBoolean(3).ToString());
                }
            }
            return result;
        }
    }
}

