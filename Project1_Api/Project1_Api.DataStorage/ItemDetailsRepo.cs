using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public class ItemDetailsRepo : IItemDetailsRepo
    {
        private readonly string _connectionString;
        public ItemDetailsRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<string>> GetItems()
        {
            List<string> result = new();
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT ItemName, ItemDetail, Price
                               FROM ItemDetails;";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    string Name = reader.GetString(0);
                    string Detail = reader.GetString(1);
                    decimal Price = reader.GetDecimal(2);
                    string fullSentence = $"Name: {Name}\nDetail: {Detail}\nPrice: ${Price}";
                    result.Add(fullSentence);
                }
            }
            return result;
        }
    }
}
