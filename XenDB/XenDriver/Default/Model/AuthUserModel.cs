using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XenDriver.Model;

namespace XenDB.XenDriver.DB.Default.CoreModel {
    public class AuthUserModel : AbstractModel {
        override public string TableName { get; set; } = "AuthUser";
        public string UserName { get; set; }
        public int UserRoleID { get; set; }
        public bool InBattle { get; set; }

        //TODO: SPLIT THIS OUT INTO SEPARATE TABLE BUILDER CLASS

        override public List<Tuple<string, string>> TableParams { get; set; } = new List<Tuple<string, string>>() {
            Tuple.Create("username", "string"),
            Tuple.Create("password", "string"),
            Tuple.Create("userroleid", "int"),
            Tuple.Create("inbattle", "bool")
        };

        public AuthUserModel()
        {

        }

    }
}
