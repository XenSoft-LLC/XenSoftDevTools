using System;
using MySql.Data.MySqlClient;
using XenDB.Factory;

namespace XenDB.Connection {
    public static class ConnectionManager {
        private static ConnectionStringConfig _config = ConnectionStringConfigFactory.GenerateSavedConnectionConfig();
        private static MySqlConnectionStringBuilder _connectionStringBuilder = new MySqlConnectionStringBuilder {
            Server = _config.Server,
            UserID = _config.UserID,
            Password = _config.Password
        };

        public static string Schema { get { return _config.Database; } }

        public static MySqlConnection ConnectionThread { get { return new MySqlConnection(_connectionStringBuilder.ConnectionString); } }

        public static MySqlCommand CreateDbCommand(string commandText) {
            MySqlCommand _command = ConnectionThread.CreateCommand();
            _command.CommandText = commandText;
            _command.Connection.Open();
            return _command;
        }

        public static void UpdateConnectionStringBuilder(ConnectionStringConfig config) {
            _connectionStringBuilder = new MySqlConnectionStringBuilder {
                Server = config.Server,
                UserID = config.UserID,
                Password = config.Password,
                Database = config.Database
            };
        }

        public static void SetUp() {
            MySqlCommand buildDbCmd = CreateDbCommand($"CREATE SCHEMA IF NOT EXISTS {Schema}; USE {Schema};");
            buildDbCmd.ExecuteNonQuery();
            buildDbCmd.Connection.Close();
            _connectionStringBuilder.Database = Schema;
        }
    }
}
