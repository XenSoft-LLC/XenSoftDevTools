using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using XenDB.Model;

namespace XenDB.Util {
    public static class SQLParamUtil {

        public static MySqlDbType GetPropertySQLType(PropertyInfo propertyInfo) {
            switch (propertyInfo.PropertyType)
            {
                case Type stringType when stringType == typeof(string):
                    return MySqlDbType.Text;
                case Type intType when intType == typeof(int):
                    return MySqlDbType.Int32;
                case Type longType when longType == typeof(long):
                    return MySqlDbType.Int64;
                case Type ulongType when ulongType == typeof(ulong):
                    return MySqlDbType.UInt64;
                case Type floatType when floatType == typeof(float):
                    return MySqlDbType.Float;
                case Type boolType  when boolType == typeof(bool):
                    return MySqlDbType.Int16;
                default:
                    throw new Exception("Unknown table param type.");
            }
        }

        public static string GetSQLTypeString(MySqlDbType dbType) {
            switch (dbType) {
                case MySqlDbType.Text:
                    return "TEXT";
                case MySqlDbType.Int32:
                    return "INT";
                case MySqlDbType.Int64:
                    return "BIGINT";
                case MySqlDbType.UInt64:
                    return "BIGINT";
                case MySqlDbType.Float:
                    return "FLOAT";
                case MySqlDbType.Int16:
                    return "BOOLEAN";
                default:
                    throw new Exception("Unknown table param type.");
            }
        }

        public static string GetSQLParamString<T>(this T model) where T : AbstractModel {
            return string.Join(", ", model.GetSQLParameters().Select(x => {
                return $"{x.ParameterName.Substring(1)} {GetSQLTypeString(x.MySqlDbType)}";
            }).ToList());
        }

        public static List<MySqlParameter> GetSQLParameters<T>(this T model) where T : AbstractModel {
            return model.GetType().GetProperties().Where(property => { return property.PropertyType.IsPrimitive && property.Name != "TableName" && property.Name != "ID"; }).ToList().Select(property => {
                return new MySqlParameter($"@{property.Name}", GetPropertySQLType(property), 100) { Value = property.GetValue(model) };
            }).ToList();
        }

        public static MySqlCommand InsertPreparedStatement<T>(this T model, MySqlConnection connection) where T : AbstractModel {
            MySqlCommand insertCmd = connection.CreateCommand();

            var parameters = model.GetSQLParameters();
            var columnNamesString = string.Join(", ", parameters.Select(param => param.ParameterName.Substring(1)));
            var valuesString = string.Join(", ", parameters.Select(param => param.ParameterName));

            insertCmd.CommandText = $"INSERT INTO {model.TableName} ({columnNamesString}) VALUES ({valuesString});";

            insertCmd.Parameters.AddRange(parameters.ToArray());

            insertCmd.Connection.Open();
            insertCmd.Prepare();
            return insertCmd;
        }

        public static MySqlCommand UpdatePreparedStatement<T>(this T model, MySqlConnection connection) where T : AbstractModel {
            MySqlCommand updateCmd = connection.CreateCommand();
            updateCmd.CommandText = $"UPDATE {model.TableName} SET {string.Join(", ", model.GetSQLParameters().Select(param => $"{param.ParameterName.Substring(1)} = {param.ParameterName}"))} WHERE ID = @idValue";

            updateCmd.Parameters.AddRange(model.GetSQLParameters().ToArray());
            updateCmd.Parameters.Add(new MySqlParameter("@idValue", MySqlDbType.VarChar, 100) { Value = model.ID });

            updateCmd.Connection.Open();
            updateCmd.Prepare();
            return updateCmd;
        }

        public static MySqlCommand DeletePreparedStatement<T>(this T model, MySqlConnection connection) where T : AbstractModel {
            MySqlCommand deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = $"DELETE FROM {model.TableName} WHERE ID = @idValue";

            deleteCmd.Parameters.Add(new MySqlParameter("@idValue", MySqlDbType.VarChar, 100) { Value = model.ID });

            deleteCmd.Connection.Open();
            deleteCmd.Prepare();
            return deleteCmd;
        }

    }
}
