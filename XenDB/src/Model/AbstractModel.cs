using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using XenDB.XenDriver.DB;
using XenDB.XenDriver.Model;
using XenDB.XenDriver.Util;

namespace XenDriver.Model
{
    public abstract class AbstractModel
    {
        public abstract string TableName { get; }

        public int ID { get; set; }

        public AbstractModel()  { }

        public string SQLParamString() { return string.Join(", ", GetSQLParameters().Select(x => { 
            return $"{x.ParameterName.Substring(1)} {x.MySqlDbType}"; 
        }).ToList());  }

        public List<MySqlParameter> GetSQLParameters() {
            return GetType().GetProperties().Where(x => { return x.Name != "TableName" && x.Name != "ID"; }).ToList().Select(property => {
                    return new MySqlParameter($"@{property.Name}", SQLParamUtil.GetPropertySQLType(property), 100) { Value = property.GetValue(this) };
            }).ToList();
        }

        public MySqlCommand InsertPreparedStatement(MySqlConnection connection)
        {
            MySqlCommand insertCmd = connection.CreateCommand();

            var parameters = GetSQLParameters();
            var columnNamesString = string.Join(", ", parameters.Select(param => param.ParameterName.Substring(1)));
            var valuesString = string.Join(", ", parameters.Select(param => param.ParameterName));

            insertCmd.CommandText = $"INSERT INTO {TableName} ({columnNamesString}) VALUES ({valuesString});";

            insertCmd.Parameters.AddRange(parameters.ToArray());

            insertCmd.Prepare();
            return insertCmd;
        }

        public DbCommand UpdatePreparedStatement(DbConnection connection)
        {
            DbCommand updateCmd = connection.CreateCommand();
            updateCmd.CommandText = $"UPDATE {TableName} SET {string.Join(", ", GetSQLParameters().Select(param => $"{param.ParameterName.Substring(1)} = {param.ParameterName}"))} WHERE ID = @idValue";

            updateCmd.Parameters.AddRange(GetSQLParameters().ToArray());
            updateCmd.Parameters.Add(new MySqlParameter("@idValue", MySqlDbType.VarChar, 100) { Value = ID });

            updateCmd.Prepare();
            return updateCmd;
        }

        public DbCommand DeletePreparedStatement(DbConnection connection)
        {
            DbCommand deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = $"DELETE FROM {TableName} WHERE ID = @idValue";

            deleteCmd.Parameters.Add(new MySqlParameter("@idValue", MySqlDbType.VarChar, 100) { Value = ID });

            deleteCmd.Prepare();
            return deleteCmd;
        }


    }
}
