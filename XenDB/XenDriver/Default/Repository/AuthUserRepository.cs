using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.DB.Default.CoreModel;
using XenDB.XenDriver.Model;

namespace XenDB.XenDriver.Repository {
    public class AuthUserRepository : AbstractRepository<AuthUserModel>, IRepository {
        private string _tableName = "authuser";
        public override string TableName{ get { return _tableName; } }

        public void Signup(string userName, string password) {

            _driver.executeQuery($"INSERT INTO {TableName} (username, password, userroleid) VALUES (\"{userName}\", \"{password}\", 0);");

        }
    }
}
