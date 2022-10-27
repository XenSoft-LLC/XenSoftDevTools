using System;
using System.Collections.Generic;
using System.Text;
using XenDriver.Model;

namespace XenDB.XenDriver.DBBuilder.Default.Model {
    class UserRoleModel : AbstractModel {
        override public string TableName { get; set; } = "UserRole";
        public string Name { get; set; }
        override public List<Tuple<string, string>> TableParams { get; set; } = new List<Tuple<string, string>>() {
            Tuple.Create("name", "string")
        };
    }
}
