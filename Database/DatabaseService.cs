using MySqlConnector;
using Webcrawler.Models;

namespace Webcrawler.Database
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveExecutionInfo(SaveInfos info)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"
                INSERT INTO SaveInfos (StartTime, EndTime, TotalPages, TotalRowsExtracted, JsonFileName)
                VALUES (@StartTime, @EndTime, @TotalPages, @TotalRowsExtracted, @JsonFileName)";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@StartTime", info.StartTime);
            command.Parameters.AddWithValue("@EndTime", info.EndTime);
            command.Parameters.AddWithValue("@TotalPages", info.TotalPages);
            command.Parameters.AddWithValue("@TotalRowsExtracted", info.TotalRowsExtracted);
            command.Parameters.AddWithValue("@JsonFileName", info.JsonFileName);

            command.ExecuteNonQuery();
            Console.WriteLine("Dados de execução salvos no banco.");
        }
    }
}
