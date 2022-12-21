using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using XenDB.XenDriver.ConnectionConfig;
using XenDB.XenDriver.Factory;

namespace XenDB.XenDriver.ConnectionThread {
    public static class ConnectionThreadManager {

        private static ConnectionStringConfigFactory _cscFactory = new ConnectionStringConfigFactory();
        private static ConnectionStringConfig _config = _cscFactory.generateSavedConnectionConfig();
        private static MySqlConnectionStringBuilder _connectionStringBuilder = new MySqlConnectionStringBuilder {
            Server = _config.Server,
            UserID = _config.UserID,
            Password = _config.Password,
            Database = _config.Database
        };
        public static string ConnectionString { get; set; } = null;

        public static string GameSchema { get { return _connectionStringBuilder.Database; } }

        static public void UpdateConnectionStringBuilder(ConnectionStringConfig config)
        {
            _connectionStringBuilder = new MySqlConnectionStringBuilder {
                Server = config.Server,
                UserID = config.UserID,
                Password = config.Password,
                Database = config.Database
            };
        }


        public static MySqlConnection ConnectionThread() {
            ConnectionStringConfig config = new ConnectionStringConfigFactory().generateSavedConnectionConfig();
            return new MySqlConnection(new MySqlConnectionStringBuilder
            {
                Server = config.Server,
                UserID = config.UserID,
                Password = config.Password,
                Database = config.Database

            }.ConnectionString);
        }

    }
}
