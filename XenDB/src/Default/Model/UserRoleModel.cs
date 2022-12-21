using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.DB;
using XenDB.XenDriver.Model;
using XenDriver.Model;

namespace XenDB.XenDriver.DBBuilder.Default.Model {
    public class UserRoleModel : AbstractModel {
        public override string TableName => "userrole";
        public string Name { get; set; }

        public UserRoleModel() { }

        public UserRoleModel(string name) {
            Name = name;
        }
    }
}
