using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace XenDB.XenDriver.Util {
    public class SQLParamUtil {

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
                    return MySqlDbType.Int32;
                default:
                    throw new Exception("Unknown table param type.");
            }
        }

    }
}
