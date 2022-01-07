using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public class StoreInventoryRepo : IStoreInventoryRepo
    {
        private readonly string _connectionString;
        public StoreInventoryRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<string>> GetAllStoreInventory()
        {
            List<string> result = new();
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT ItemName, ItemQuantity, StoreLocation
                               FROM StoreItems
                               INNER JOIN StoreLocations ON StoreLocations.StoreId = StoreItems.StoreId";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    string ItemName = reader.GetString(0);
                    string ItemQuantity = reader.GetInt32(1).ToString();
                    string StoreLocation = reader.GetString(2);
                    string FullSentence = $"Item: {ItemName}\nQuantity: {ItemQuantity}\nLocation: {StoreLocation}";
                    result.Add(FullSentence);
                }
            }
            return result;
        }

        public async Task<List<string>> GetStoreInventoryById(string id)
        {
            List<string> result = new();
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT ItemName, ItemQuantity, StoreLocation
                               FROM StoreItems
                               INNER JOIN StoreLocations ON StoreLocations.StoreId = StoreItems.StoreId
                               WHERE StoreItems.StoreId = @storeId";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                cmd.Parameters.AddWithValue("@storeId", id);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    string ItemName = reader.GetString(0);
                    string ItemQuantity = reader.GetInt32(1).ToString();
                    string StoreLocation = reader.GetString(2);
                    string FullSentence = $"Item: {ItemName}\nQuantity: {ItemQuantity}\nLocation: {StoreLocation}";
                    result.Add(FullSentence);
                }
            }
            return result;
        }
    }
}

