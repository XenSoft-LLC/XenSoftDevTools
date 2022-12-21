using XenDriver.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using XenDB.XenDriver.ConnectionThread;
using System;
using System.Reflection;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace XenDB.XenDriver.DBDriver {
    public class SQLQueryDriver {

        public SQLQueryDriver() {

        }

        public T SelectOne<T>(string commandText) where T : AbstractModel, new() {
            MySqlConnection connection = ConnectionThreadManager.ConnectionThread();
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = commandText;
            connection.Open();
            T t = new T();
            using (var reader = selectCmd.ExecuteReader()) {
                if (!reader.Read()) { return null; }
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string propertyName = reader.GetName(i);
                    object columnValue = reader.GetValue(i);

                    PropertyInfo property = typeof(T).GetProperty(propertyName);
                    try {
                        property.SetValue(t, columnValue);
                    } catch {
                        Console.WriteLine($"{property.Name} has no setter");
                    }
                }
            }
            connection.Close();
            return t;
        }

        public List<T> SelectMany<T>(string commandText) where T : AbstractModel, new()
        {
            DbConnection connection = ConnectionThreadManager.ConnectionThread();
            connection.Open();
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = commandText;

            List<T> results = new List<T>();

            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    T t = new T();

                    for (int i = 0; i < reader.FieldCount; i++) {
                        string propertyName = reader.GetName(i);
                        object columnValue = reader.GetValue(i);

                        PropertyInfo property = typeof(T).GetProperty(propertyName);
                        property.SetValue(t, columnValue);
                    }
                    results.Add(t);
                }
            }
            connection.Close();
            return results;
        }

        public string SelectString(string commandText)
        {
            DbConnection connection = ConnectionThreadManager.ConnectionThread();
            connection.Open();
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = commandText;
            string _resultString = (string)selectCmd.ExecuteScalar();
            connection.Close();
            return _resultString;
        }

        public void NonQuery(string commandText)
        {
            DbConnection connection = ConnectionThreadManager.ConnectionThread();
            connection.Open();
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = commandText;
            selectCmd.ExecuteNonQuery();
            connection.Close();
        }

        public T Insert<T>(T t) where T : AbstractModel
        {
            MySqlConnection connection = ConnectionThreadManager.ConnectionThread();
            connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            MySqlCommand insertCmd = t.InsertPreparedStatement(connection);

            insertCmd.ExecuteNonQuery();

            MySqlCommand getIdCmd = connection.CreateCommand();
            getIdCmd.CommandText = "SELECT LAST_INSERT_ID();";
            t.ID = Convert.ToInt32((ulong)getIdCmd.ExecuteScalar());

            transaction.Commit();
            connection.Close();
            return t;
        }

        public void Update<T>(T t) where T : AbstractModel {
            DbConnection connection = ConnectionThreadManager.ConnectionThread();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            DbCommand updateCmd = t.UpdatePreparedStatement(connection);
            updateCmd.ExecuteNonQuery();

            transaction.Commit();
            connection.Close();
        }

        public void Delete<T>(T t) where T : AbstractModel {
            MySqlConnection connection = ConnectionThreadManager.ConnectionThread();
            connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            DbCommand deleteCmd = t.DeletePreparedStatement(connection);

            deleteCmd.ExecuteNonQuery();

            transaction.Commit();
            connection.Close();
        }
    }
}
