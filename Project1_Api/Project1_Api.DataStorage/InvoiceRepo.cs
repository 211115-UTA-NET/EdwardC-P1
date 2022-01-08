using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Api.DataStorage
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly string _connectionString;
        public InvoiceRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<string>> GetAllInvoice()
        {
            List<string> result = new();
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT Invoices.InvoiceId, InvoiceHistory.InvoiceDate, 
	                                  InvoiceList.ItemName, InvoiceList.ItemQuantity, 
	                                  InvoiceList.TotalPrice, StoreLocations.StoreLocation
                                      FROM Invoices
                                      INNER JOIN InvoiceHistory ON InvoiceHistory.InvoiceId = Invoices.InvoiceId
                                      INNER JOIN InvoiceList ON InvoiceList.InvoiceId = Invoices.InvoiceId
                                      INNER JOIN StoreLocations On StoreLocations.StoreId = Invoices.StoreId;";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                result = await GetInformation(reader);
            }
            return result;
        }

        public async Task<List<string>> GetInvoiceByStoreId(string num)
        {
            List<string> result = new();
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT Invoices.InvoiceId, InvoiceHistory.InvoiceDate, 
	                                  InvoiceList.ItemName, InvoiceList.ItemQuantity, 
	                                  InvoiceList.TotalPrice, StoreLocations.StoreLocation
                                      FROM Invoices
                                      INNER JOIN InvoiceHistory ON InvoiceHistory.InvoiceId = Invoices.InvoiceId
                                      INNER JOIN InvoiceList ON InvoiceList.InvoiceId = Invoices.InvoiceId
                                      INNER JOIN StoreLocations On StoreLocations.StoreId = Invoices.StoreId
                                      WHERE Invoices.StoreId = @storeId;";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                cmd.Parameters.AddWithValue("@storeId", num);
                using SqlDataReader reader = cmd.ExecuteReader();
                result = await GetInformation(reader);
            }
            return result;
        }

        public async Task<List<string>> GetInvoiceByCustomerId(string num)
        {
            List<string> result = new();
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT Invoices.InvoiceId, InvoiceHistory.InvoiceDate, 
	                                  InvoiceList.ItemName, InvoiceList.ItemQuantity, 
	                                  InvoiceList.TotalPrice, StoreLocations.StoreLocation
                                      FROM Invoices
                                      INNER JOIN InvoiceHistory ON InvoiceHistory.InvoiceId = Invoices.InvoiceId
                                      INNER JOIN InvoiceList ON InvoiceList.InvoiceId = Invoices.InvoiceId
                                      INNER JOIN StoreLocations On StoreLocations.StoreId = Invoices.StoreId
                                      WHERE Invoices.CustomerId = @customerId;";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                cmd.Parameters.AddWithValue("@customerId", num);
                using SqlDataReader reader = cmd.ExecuteReader();
                result = await GetInformation(reader);
            }
            return result;
        }

        public async Task<List<string>> GetInformation(SqlDataReader reader)
        {
            List<string> result = new();
            while (await reader.ReadAsync())
            {
                string Id = reader.GetInt32(0).ToString();
                string History = reader.GetDateTime(1).ToString();
                string ItemName = reader.GetString(2);
                string Quantity = reader.GetInt32(3).ToString();
                string Price = reader.GetDecimal(4).ToString();
                string Location = reader.GetString(5);
                string FullSentence = $"InvoiceId: {Id}\nDate: {History}\nItem Name: {ItemName}\nQuantity: {Quantity}\nTotal Price: {Price}\nLocation: {Location}";
                result.Add(FullSentence);
            }
            return result;
        }
    }
}
