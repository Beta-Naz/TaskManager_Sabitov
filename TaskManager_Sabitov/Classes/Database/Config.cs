using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace TaskManager_Sabitov.Classes.Database
{
    public class Connection
    {
        private readonly static string _connectionData =
              @"server=localhost;
              database=taskmanager;
              uid=root;
              pwd=1234";

        public static readonly MySqlServerVersion version = new MySqlServerVersion(new Version(8,0,11));

        public static MySqlConnection CreateConnection()
        {
            MySqlConnection connection = new MySqlConnection(_connectionData);
            connection.Open();
            return connection;
        }
        public static MySqlDataReader Query(string sql, MySqlConnection connection)
        {
            return new MySqlCommand(sql, connection).ExecuteReader();
        }
        public static void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
            MySqlConnection.ClearPool(connection);
        }
    }
}
