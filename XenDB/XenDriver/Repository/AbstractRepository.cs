using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.DBDriver;
using XenDriver.Model;

namespace XenDB.XenDriver.Repository {
    public abstract class AbstractRepository<T> where T : AbstractModel {

        protected SQLQueryDriver<T> _driver = new SQLQueryDriver<T>();
        public abstract string TableName { get; }

        public bool checkIfExistsByID(string objectId)
        {

            return _driver.executeQuery($"SELECT EXISTS(SELECT 1 FROM {TableName} WHERE {TableName}ID=\"{objectId}\");") == "1\n";

        }

        public string GetByID(string objectId)
        {

            return _driver.executeQuery($"SELECT 1 FROM {TableName} WHERE {TableName}ID=\"{objectId}\";");

        }

        public List<string> GetByColumnName(string objectId, string columnName)
        {

            return _driver.executeTableviewQuery($"SELECT * FROM {TableName} WHERE {columnName}={objectId};");

        }

        public bool checkIfExistsByColumnName(string objectId, string columnName)
        {
            MySqlConnection connection = _driver.getConnectionThread();
            connection.Open();
            string resultString = "";
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = $" SELECT EXISTS(SELECT 1 FROM {TableName} WHERE {columnName}=\"{objectId}\");";

            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    resultString += $"{reader.GetString(0)}\n";
                }
            }
            connection.Close();
            if (resultString == "1\n")
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
