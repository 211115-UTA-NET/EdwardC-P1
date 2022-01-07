using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public class StoreLocationRepo : IStoreLocationRepo
    {
        private readonly string _connectionString;
        public StoreLocationRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<string>> GetStoreLocation()
        {
            List<string> info = new();
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT * 
                               FROM StoreLocations";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                while(await reader.ReadAsync())
                {
                    string StoreId = reader.GetInt32(0).ToString();
                    string Location = reader.GetString(1);
                    string GetAddress = $"{StoreId}. {Location}";
                    info.Add(GetAddress);
                }
            }
            await connection.CloseAsync();
            return info;
        }
    }
}
