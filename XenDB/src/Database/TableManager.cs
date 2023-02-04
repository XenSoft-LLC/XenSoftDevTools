using System;
using System.Collections.Generic;
using System.Linq;
using XenDB.Model;
using XenDB.Driver;
using XenDB.Util;
using XenDB.System.Model;
using XenDB.src.System.Model.BridgeModel;

namespace XenDB.Database {
    public static class TableManager {
        private static List<Type> CustomTables = new List<Type>() {};
        private static List<Type> SystemTables = new List<Type>() {
            typeof(AuthUserModel),
            typeof(UserRoleModel),
            typeof(AuthUserUserRoleBridge)
        };
        public static List<Type> AllTables { get { return SystemTables.Concat(CustomTables).ToList(); } }

        public static void AddTable(Type type) {
            if (type.IsAbstractModel()) {
                CustomTables.Add(type);
            } else {
                throw new Exception("Type does not implement AbstractModel");
            }
        }

        public static void AddTables(List<Type> types) {
            if ( types.TrueForAll(type => { return type.IsAbstractModel(); })) {
                CustomTables.AddRange(types);
            } else {
                throw new Exception("Not all types implement AbstractModel");
            }
        }

        public static void Build() {
            AllTables.Select(x => (AbstractModel)Activator.CreateInstance(x)).ToList().ForEach(tableType => { tableType.CreateTable(); });
        }

    }
}
