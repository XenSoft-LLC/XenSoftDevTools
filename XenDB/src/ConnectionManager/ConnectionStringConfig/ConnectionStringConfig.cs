namespace XenDB.Connection {
    public class ConnectionStringConfig {
        public string Server { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public ConnectionStringConfig(string server, string userID, string password, string database)
        {
            Server = server;
            UserID = userID;
            Password = password;
            Database = database;
        }
    }
}
