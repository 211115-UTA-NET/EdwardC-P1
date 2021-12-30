using Project_1.Api.DataStorage;
using System.Data.SqlClient;

namespace Project_1Api.DataStorage
{
    public class SqlRepository : IRepository
    {
        private readonly string _connectionString;

        public SqlRepository(string? connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // FIXME: fix query section
        public async Task<bool> CustomerExistAsync(string name)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            // returns 1 if exists, nothing if not
            string cmdText = @"SELECT 1
                               FROM Customers
                               WHERE FirstName = @customername";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                cmd.Parameters.AddWithValue("@customername", name);

                using SqlDataReader reader = cmd.ExecuteReader();

                // true if at least one row
                return await reader.ReadAsync();
            }
        }
    }
}