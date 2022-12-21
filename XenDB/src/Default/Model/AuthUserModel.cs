using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.DB;
using XenDB.XenDriver.Model;
using XenDriver.Model;

namespace XenDB.XenDriver.Default.Model {
    public class AuthUserModel : AbstractModel {
        private static string _tableName = "authuser";
        public override string TableName => _tableName;

        public string UserName { get; set; }
        private readonly string _password;
        public int UserRoleID { get; set; }
        public bool InBattle { get; set; }


        public AuthUserModel(){ }

        public AuthUserModel(string userName, string password)
        {
            UserName = userName;
            _password = password;
            UserRoleID = 1;
            InBattle = false;
        }

        public AuthUserModel(int id, string userName, int userRoleID, bool inBattle)
        {
            ID = id;
            UserName = userName;
            UserRoleID = userRoleID;
            InBattle = inBattle;
        }

        public AuthUserModel(string userName, string password, int userRoleID, bool inBattle)
        {
            UserName = userName;
            _password = password;
            UserRoleID = userRoleID;
            InBattle = inBattle;
        }
    }
}
