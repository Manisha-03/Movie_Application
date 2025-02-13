using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MovieAppRepository.Repository
{
    public class CommonMethods
    {
        private readonly string _connectionString;

        public CommonMethods(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Executes a stored procedure and returns a list of results.
        /// </summary>
        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedure, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Executes a stored procedure for insert/update/delete operations.
        /// </summary>
        public int ExecuteStoredProcedureNonQuery(string storedProcedure, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Executes the sp_InsertCustomer stored procedure and returns the inserted CustomerId.
        /// </summary>
        public int ExecuteStoredProcedureScalar(string storedProcedure, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.ExecuteScalar<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
