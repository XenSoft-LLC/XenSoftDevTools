using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.ConnectionConfig;
using XenDB.XenDriver.Factory;

namespace XenDB.XenDriver.ConnectionThread {
    public static class ConnectionThreadManager {

        private static MySqlConnectionStringBuilder _connectionStringBuilder = null;
        private static ConnectionStringConfigFactory _cscFactory = new ConnectionStringConfigFactory();

        static public void updateConnectionStringBuilder(ConnectionStringConfig config)
        {
            _connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = config.Server,
                UserID = config.UserID,
                Password = config.Password,
                Database = config.Database
            };
        }


        static internal MySqlConnection getConnectionThread()
        {
            if (_connectionStringBuilder == null)
            {
                ConnectionStringConfig config = _cscFactory.generateSavedConnectionConfig();
                _connectionStringBuilder = new MySqlConnectionStringBuilder
                {
                    Server = config.Server,
                    UserID = config.UserID,
                    Password = config.Password,
                    Database = config.Database
                };
            }
            return new MySqlConnection(_connectionStringBuilder.ConnectionString);
        }

    }
}
