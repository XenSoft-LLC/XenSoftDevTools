using XenDB.Model;

namespace XenDB.System.Model {
    public class UserRoleModel : AbstractModel {
        public override string TableName => "userrole";
        public string Name { get; set; }

        public UserRoleModel() { }

        public UserRoleModel(string name) {
            Name = name;
        }
    }
}
