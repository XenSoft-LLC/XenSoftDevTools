using System.Collections.Generic;
using System.Linq;
using XenDB.Connection;
using XenDB.Driver;
using XenDB.Model;

namespace XenDB.Repository {
    public static class BaseRepository {

        public static T SelectByID<T>(int modelId) where T : AbstractModel, new() {
            return SQLQueryDriver.SelectByID<T>(modelId);
        }

        public static List<T> SelectAll<T>() where T : AbstractModel, new() {
            return SQLQueryDriver.SelectMany<T>($"SELECT * FROM { ConnectionManager.Schema }.{ new T().TableName }");
        }

        public static List<T> SelectByColumnName<T>(string columnName, string value) where T : AbstractModel, new() {
            return SQLQueryDriver.SelectMany<T>($@"SELECT * FROM {new T().TableName} WHERE {columnName} = ""{value}"";");
        }

        public static T SelectFirstByColumnName<T>(string columnName, string value) where T : AbstractModel, new() {
            return SQLQueryDriver.SelectMany<T>($@"SELECT * FROM {new T().TableName} WHERE {columnName} = ""{value}"" limit 1;").First();
        }

        internal static void Update<T>(this T model) where T : AbstractModel {
            SQLQueryDriver.Update(model);
        }

        internal static T Insert<T>(this T model) where T : AbstractModel {
            return SQLQueryDriver.Insert(model);
        }

        internal static int Delete<T>(this T model) where T : AbstractModel {
            return SQLQueryDriver.Delete(model);
        }

        internal static T Upsert<T>(this T model) where T : AbstractModel {
            return SQLQueryDriver.Upsert(model);
        }
    }
}
