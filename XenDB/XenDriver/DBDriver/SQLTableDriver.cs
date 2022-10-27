using XenDriver.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.ConnectionThread;

namespace XenDB.XenDriver.DBDriver {
    public static class SQLTableDriver {

        static public void createTable(string tableName, string tableParamString)
        {
            MySqlConnection connection = ConnectionThreadManager.getConnectionThread();
            connection.Open();

            //Create table (drop if already exists first):
            var delTableCmd = connection.CreateCommand();
            delTableCmd.CommandText = $"DROP TABLE IF EXISTS {tableName}";
            delTableCmd.ExecuteNonQuery();

            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = $"CREATE TABLE {tableName} ({tableParamString})";
            createTableCmd.ExecuteNonQuery();
            connection.Close();
        }

        static public void createTable(AbstractModel objectModel)
        {
            MySqlConnection connection = ConnectionThreadManager.getConnectionThread();
            connection.Open();

            //Create table (drop if already exists first):
            var delTableCmd = connection.CreateCommand();
            delTableCmd.CommandText = $"DROP TABLE IF EXISTS {objectModel.TableName}";
            delTableCmd.ExecuteNonQuery();


            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = $"CREATE TABLE {objectModel.TableName} (id INTEGER NOT NULL AUTO_INCREMENT, {buildTableParamsString(objectModel)}, PRIMARY KEY (ID))";
            Console.WriteLine(createTableCmd.CommandText);
            createTableCmd.ExecuteNonQuery();
            connection.Close();
        }

        //Todo: MOVE TO NEW STRING UTIL CLASS?
        private static string buildTableParamsString(AbstractModel objectModel)
        {
            string tableParamsString = "";
            objectModel.TableParams.ForEach(x => {
               switch (x.Item2){
                    case "string":
                        tableParamsString += $" {x.Item1} tinytext, ";
                        break;
                    case "int":
                        tableParamsString += $" {x.Item1} integer, ";
                        break;
                    case "float":
                        tableParamsString += $" {x.Item1} float, ";
                        break;
                    case "bool":
                        tableParamsString += $" {x.Item1} tinyint(1), ";
                        break;
                    default:
                    throw new Exception("Unknown table param type.");
               }
            });
            return tableParamsString.Trim().TrimEnd(',');
        }
    }
}
