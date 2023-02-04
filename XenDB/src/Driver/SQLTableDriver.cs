using XenDB.Model;
using XenDB.Connection;
using XenDB.Util;

namespace XenDB.Driver {
    public static class SQLTableDriver {
        public static void CreateTable(this AbstractModel model) {
            var delTableCmd = ConnectionManager.CreateDbCommand($"DROP TABLE IF EXISTS {model.TableName}");
            delTableCmd.ExecuteNonQuery();
            delTableCmd.Connection.Close();

            var createTableCmd = ConnectionManager.CreateDbCommand($"CREATE TABLE {model.TableName} (ID INTEGER PRIMARY KEY auto_increment, {model.GetSQLParamString()})");
            createTableCmd.ExecuteNonQuery();
            createTableCmd.Connection.Close();
        }
    }
}
