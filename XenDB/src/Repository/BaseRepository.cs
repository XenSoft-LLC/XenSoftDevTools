using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XenDB.XenDriver.ConnectionThread;
using XenDB.XenDriver.DBDriver;
using XenDB.XenDriver.Default.Model;
using XenDB.XenDriver.Model;
using XenDB.XenDriver.Util;
using XenDriver.Model;

namespace XenDB.XenDriver.Repository {
    public static class BaseRepository {

        public static SQLQueryDriver _driver = new SQLQueryDriver();

        public static T Insert<T>(T model) where T : AbstractModel {
            _driver.Insert(model);
            return model;
        }

        public static T SelectByID<T>(string objectId) where T : AbstractModel, new() {
            return _driver.SelectOne<T>($"SELECT * FROM { new T().TableName } WHERE ID=\"{objectId}\";");
        }

        public static List<T> SelectAll<T>() where T : AbstractModel, new() {
            return _driver.SelectMany<T>($"SELECT * FROM { ConnectionThreadManager.GameSchema }.{ new T().TableName }");
        }

        public static List<T> SelectByColumnName<T>(string columnName, string columnValue) where T : AbstractModel, new() {
            return _driver.SelectMany<T>($"SELECT * FROM {new T().TableName } WHERE {columnName}={columnValue};");
        }
    }
}
