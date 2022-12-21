using XenDriver.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.ConnectionThread;
using XenDB.XenDriver.Model;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace XenDB.XenDriver.DBDriver {
    public class SQLTableDriver {

        public void CreateTable (Type t)  {
            var instance = Activator.CreateInstance(t);
            PropertyInfo tableNameField = t.GetProperty("TableName", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo paramStringMethod = t.GetMethod("SQLParamString", BindingFlags.Instance | BindingFlags.Public);

            var _tableName = tableNameField.GetValue(instance);
            var _paramString = paramStringMethod.Invoke(instance, null);

            MySqlConnection connection = ConnectionThreadManager.ConnectionThread();
            connection.Open();

            var delTableCmd = connection.CreateCommand();
            delTableCmd.CommandText = $"DROP TABLE IF EXISTS {_tableName}";
            delTableCmd.ExecuteNonQuery();

            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = $"CREATE TABLE {_tableName } (ID INTEGER PRIMARY KEY auto_increment, {_paramString})";

            createTableCmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
