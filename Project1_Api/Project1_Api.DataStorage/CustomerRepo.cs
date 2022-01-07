using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly string _connectionString;
        public CustomerRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> GetCustomerByName(string Name)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT FirstName
                               FROM Customers
                               WHERE FirstName = @name OR LastName = @name";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                cmd.Parameters.AddWithValue("@name", Name);

                using SqlDataReader reader = cmd.ExecuteReader();
                return await reader.ReadAsync();
            }
        }
    }
}
