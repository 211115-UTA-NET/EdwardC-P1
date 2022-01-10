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

        public async Task PostCustomer(List<string> customerInfo, bool IsManager)
        {
            using SqlConnection connection = new(_connectionString);

            // Add to Customer Table
            await connection.OpenAsync();
            using SqlCommand command = new(
                $"INSERT INTO Customers (FirstName, LastName, PhoneNumber, \"Address\") VALUES (@firstName, @lastName, @phoneNumber, @Address);",
                connection);
            command.Parameters.AddWithValue("@firstName", customerInfo[0]);
            command.Parameters.AddWithValue("@lastName", customerInfo[1]);
            command.Parameters.AddWithValue("@phoneNumber", customerInfo[2]);
            command.Parameters.AddWithValue("@Address", customerInfo[3]);
            command.ExecuteNonQuery();
            connection.Close();

            // Add to Login Table
            await connection.OpenAsync();
            using SqlCommand command2 = new(
                $"INSERT INTO \"Login\" (Username, \"Password\", IsManager) VALUES (@Username, @Password, @isManager);",
                connection);
            command2.Parameters.AddWithValue("@Username", customerInfo[4]);
            command2.Parameters.AddWithValue("@Password", customerInfo[5]);
            command2.Parameters.AddWithValue("@isManager", IsManager);
            command2.ExecuteNonQuery();
            connection.Close();
        }
    }
}