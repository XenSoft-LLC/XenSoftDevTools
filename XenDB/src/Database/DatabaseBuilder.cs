using MySql.Data.MySqlClient;
using XenDB.Connection;
using XenDB.Driver;
using XenDB.System.Model;

namespace XenDB.Database {
    public static class DatabaseBuilder {
        public static void BuildDatabase() {

            //Schema Setup
            ConnectionManager.SetUp();

            //Table Setup
            TableManager.Build();

            //Seed System Data
            new SystemSeederGroup().Run();
            new SystemBridgeSeederGroup().Run();
        }
    }
}