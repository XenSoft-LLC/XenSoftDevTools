using System;
using System.Collections.Generic;
using System.Linq;
using XenDB.XenDriver.DBBuilder.Default.Model;
using XenDB.XenDriver.Default.Model;
using XenDB.XenDriver.Model;
using XenDB.XenDriver.Util;
using XenDriver.Model;

namespace XenDB.XenDriver.DB {
    public static class MasterTableList {

        public static List<Type> TableModels = new List<Type>() {

        };

        public static void LoadDefaultModels() {
            TableModels.AddRange(new List<Type>() { typeof(AuthUserModel), typeof(UserRoleModel) }.Where(x => x.IsSubclassOf(typeof(AbstractModel))).ToList());
        }

        public static void AddTable(Type type) {
            if (type.IsSubclassOf(typeof(AbstractModel))) {
                TableModels.Add(type);
            } else {
                throw new Exception("Type does not implement AbstractModel");
            }
        }

    }
}
