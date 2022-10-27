using System;
using System.Collections.Generic;
using System.Text;

namespace XenDB.XenDriver.ConnectionConfig {
    public class ConnectionStringConfig {

        private string _server;
        private string _userID;
        private string _password;
        private string _database;

        public ConnectionStringConfig(string server, string userID, string password, string database)
        {
            _server = server;
            _userID = userID;
            _password = password;
            _database = database;
        }

        public string Server { 
            get { return _server;  }
            set { _server = value; }
        }

        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }
    }
}
