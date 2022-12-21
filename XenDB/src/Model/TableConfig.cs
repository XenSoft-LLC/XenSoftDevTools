using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace XenDB.XenDriver.Model {
    public class TableConfig {
        public string TableName { get; set; }

        public List<MySqlParameter> SQLParams { get; set; }

        public TableConfig(string tableName, List<MySqlParameter> tableParams)
        {
            TableName = tableName;
            SQLParams = tableParams;
        }
    }
}
