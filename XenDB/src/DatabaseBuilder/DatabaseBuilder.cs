using MySql.Data.MySqlClient;
using XenDB.Connection;
using XenDB.Driver;
using XenDB.System.Model;

namespace XenDB.Database {
    public static class DatabaseBuilder {
        public static void BuildDatabase() {
            ConnectionManager.SetUp();
            TableManager.Build();
            new SystemSeederGroup().Run();
            new SystemBridgeSeederGroup().Run();
        }
    }
}