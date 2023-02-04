using System.Collections.Generic;
using XenDB.Database;

namespace XenDB.System.Model {
    public class SystemSeederGroup : BaseSeederGroup {
        private readonly List<ISeeder> _systemSeeders = new List<ISeeder>() {
            new BaseSeeder<UserRoleModel>(new List<UserRoleModel>() { new UserRoleModel("User"), new UserRoleModel("Admin") }),
            new BaseSeeder<AuthUserModel>(new List<AuthUserModel>() { new AuthUserModel("admin", "password", 2, false), new AuthUserModel("user", "password", 1, false) })
        };

        public void Run() {
            Add(_systemSeeders);
            base.Run();
        }
    }
}