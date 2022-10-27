using System;
using System.Collections.Generic;
using XenDB.XenDriver.DBDriver;

namespace XenDB.XenDriver.DB {
    public static class DBBuilder {

        public static void BuildTables() {
            MasterTableList.loadCoreModels();
            MasterTableList.Tables.ForEach(x => SQLTableDriver.createTable(x));
        }
    }
}