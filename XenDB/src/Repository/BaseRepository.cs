using System.Collections.Generic;
using System.Linq;
using XenDB.Connection;
using XenDB.Driver;
using XenDB.Model;

namespace XenDB.Repository {
    public static class BaseRepository {

        //Exists Functions
        public static bool Exists<T>(this T model) where T : AbstractModel, new() {
            return SelectExistsByID<T>(model.ID);
        }

        public static bool SelectExists<T>(string commandText) where T : AbstractModel, new() {
            return QueryExecutor.SelectScalar($"SELECT EXISTS({commandText.TrimEnd(';')});") == "true";
        }

        public static bool SelectExistsByID<T>(int modelId) where T : AbstractModel, new() {
            return SelectExists<T>($"SELECT * FROM {new T().TableName} WHERE ID=\"{modelId}\";");
        }

        //Read Objects
        public static T SelectByID<T>(int modelId) where T : AbstractModel, new() {
            return QueryExecutor.SelectOne<T>($"SELECT * FROM {new T().TableName} WHERE ID=\"{modelId}\";");
        }

        public static List<T> SelectAll<T>() where T : AbstractModel, new() {
            return QueryExecutor.SelectMany<T>($"SELECT * FROM { ConnectionManager.Schema }.{ new T().TableName }");
        }

        public static List<T> SelectByColumnName<T>(string columnName, string value) where T : AbstractModel, new() {
            return QueryExecutor.SelectMany<T>($@"SELECT * FROM {new T().TableName} WHERE {columnName} = ""{value}"";");
        }

        public static T SelectFirstByColumnName<T>(string columnName, string value) where T : AbstractModel, new() {
            return QueryExecutor.SelectMany<T>($@"SELECT * FROM {new T().TableName} WHERE {columnName} = ""{value}"" limit 1;").First();
        }

        // Created, Update, and Delete
        internal static T Update<T>(this T model) where T : AbstractModel {
            return QueryExecutor.Update(model);
        }

        internal static T Insert<T>(this T model) where T : AbstractModel {
            return QueryExecutor.Insert(model);
        }

        internal static int Delete<T>(this T model) where T : AbstractModel {
            return QueryExecutor.Delete(model);
        }

        internal static T Upsert<T>(this T model) where T : AbstractModel, new() {
            if (model.Exists()) {
                return model.Insert();
            } else {
                return model.Update();
            }
        }
    }
}
