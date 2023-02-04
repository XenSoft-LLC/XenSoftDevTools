using XenDB.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using XenDB.Connection;
using System;
using System.Reflection;
using XenDB.Util;
using MySqlX.XDevAPI.Common;

namespace XenDB.Driver {
    public static class QueryExecutor {

        // Read Objects
        public static string SelectScalar(string commandText) {
            var selectCmd = ConnectionManager.CreateDbCommand(commandText);
            //TODO: Does this need exception?
            string result = (string)selectCmd.ExecuteScalar();
            selectCmd.Connection.Close();
            return result;
        }

        public static T SelectOne<T>(string commandText) where T : AbstractModel, new() {
            if(!commandText.Contains("limit 1;")) {
                commandText = $"{commandText.TrimEnd(';')} limit 1;";
            }

            MySqlCommand selectCmd = ConnectionManager.CreateDbCommand(commandText);
            T model = new T();

            using (var reader = selectCmd.ExecuteReader()) {
                while (reader.Read()) {
                    for (int i = 0; i < reader.FieldCount; i++) {
                        PropertyInfo property = model.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(model, reader.GetValue(i));
                    }
                    return model;
                }
            }
            throw new Exception($"No result for query.");
        }

        public static List<T> SelectMany<T>(string commandText) where T : AbstractModel, new() {
            var selectCmd = ConnectionManager.CreateDbCommand(commandText);
            List<T> results = new List<T>();

            using (var reader = selectCmd.ExecuteReader()) {
                while (reader.Read()) {
                    T model = new T();
                    for (int i = 0; i < reader.FieldCount; i++) {
                        PropertyInfo property = model.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(model, reader.GetValue(i));
                    }
                    results.Add(model);
                }
            }

            selectCmd.Connection.Close();
            return results;
        }

        //Create, Update, and Delete
        public static T Insert<T>(this T model) where T : AbstractModel {
            MySqlCommand insertCmd = model.InsertPreparedStatement(ConnectionManager.ConnectionThread);
            MySqlCommand getIdCmd = insertCmd.Connection.CreateCommand();
            getIdCmd.CommandText = "SELECT LAST_INSERT_ID();";

            MySqlTransaction transaction = insertCmd.Connection.BeginTransaction();
            insertCmd.ExecuteNonQuery();

            model.ID = Convert.ToInt32((ulong)getIdCmd.ExecuteScalar());
            transaction.Commit();
            insertCmd.Connection.Close();
            return model;
        }

        public static T Update<T>(this T model) where T : AbstractModel {
            if(model.ID != 0) {
                MySqlCommand updateCmd = model.UpdatePreparedStatement(ConnectionManager.ConnectionThread);
                MySqlTransaction transaction = updateCmd.Connection.BeginTransaction();

                updateCmd.ExecuteNonQuery();

                transaction.Commit();
                updateCmd.Connection.Close();
                return model;
            } else {
                throw new Exception("Update Failed: Object does not exist in Database.");
            }
        }

        public static int Delete<T>(this T model) where T : AbstractModel {
            MySqlCommand deleteCmd = model.DeletePreparedStatement(ConnectionManager.ConnectionThread);
            MySqlTransaction transaction = deleteCmd.Connection.BeginTransaction();

            deleteCmd.ExecuteNonQuery();

            transaction.Commit();
            deleteCmd.Connection.Close();
            return model.ID;
        }
    }
}
