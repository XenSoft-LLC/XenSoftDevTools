using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.DBBuilder.Default.Model;
using XenDB.XenDriver.Repository;

namespace XenDB.XenDriver.Default.Repository {
    class UserRoleRepository : AbstractRepository<UserRoleModel>, IRepository {
        public string _tableName = "userrole";
        public override string TableName { get { return _tableName; } }
    }
}
