using System;
using System.Collections.Generic;
using System.Linq;
using XenDB.XenDriver.DBBuilder;
using XenDB.XenDriver.DBDriver;
using XenDriver.Model;

namespace XenDB.XenDriver.DB {
    public static class DBBuilder {
        private static SQLTableDriver _driver = new SQLTableDriver();

        public static void BuildDatabase() {
            MasterTableList.LoadDefaultModels();
            MasterTableList.TableModels.ForEach(x => { _driver.CreateTable(x); });
            DefaultSeeders.RunDefaultSeeders();
        }
    }
}