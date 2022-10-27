using XenDriver.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using XenDB.XenDriver.ConnectionThread;

namespace XenDB.XenDriver.DBDriver {
    public class SQLQueryDriver<T> where T : AbstractModel {
        public MySqlConnection getConnectionThread()
        {
            return ConnectionThreadManager.getConnectionThread();
        }

        public MySqlCommand getInsertPrepareStatement(T t, MySqlCommand insertCmd)
        {

            //Move to Abstract Class
            insertCmd.CommandText = $"INSERT INTO {t.TableName} VALUES (null, {string.Join(", ", t.TableParams.Select(tableParam => $"@{tableParam}"))})";

            t.TableParams.ForEach(tableParam =>
            {
                insertCmd.Parameters.Add(new MySqlParameter($"@{tableParam}", MySqlDbType.Text, 100) { Value = t.getTableParam(tableParam.Item1) });
            });

            insertCmd.Prepare();
            return insertCmd;
        }

        public List<string> executeTableviewQuery(string commandText)
        {
            MySqlConnection connection = getConnectionThread();
            connection.Open();
            List<string> resultList = new List<string>();
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = commandText;
            int _iterator = 0;
            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    while (true)
                    {
                        try
                        {
                            resultList.Add(reader.GetString(_iterator++));
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }
            connection.Close();
            return resultList;
        }

        public string executeQuery(string commandText)
        {
            MySqlConnection connection = getConnectionThread();
            connection.Open();
            string resultString = "";
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = commandText;
            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    resultString += $"{reader.GetString(0)}\n";
                }
            }
            connection.Close();
            return resultString;
        }

        public void executeNonQuery(string commandText)
        {
            MySqlConnection connection = getConnectionThread();
            connection.Open();
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = commandText;
            selectCmd.ExecuteNonQuery();
            connection.Close();
        }

        public string insert(T t)
        {
            MySqlConnection connection = getConnectionThread();
            connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            MySqlCommand insertCmd = connection.CreateCommand();
            insertCmd = getInsertPrepareStatement(t, insertCmd);
            insertCmd.Prepare();
            insertCmd.ExecuteNonQuery();

            transaction.Commit();
            connection.Close();

            return insertCmd.LastInsertedId.ToString();
        }
    }
}
