using System.Collections.Generic;
using XenDB.Database;
using XenDB.Service;
using XenDB.src.System.Model.BridgeModel;

namespace XenDB.System.Model {
    public class SystemBridgeSeederGroup : BaseSeederGroup {
        private List<ISeeder> _systemBridgeSeeders = new List<ISeeder>() {
            new BaseSeeder<AuthUserUserRoleBridge>(
                new List<AuthUserUserRoleBridge>() {
                    new AuthUserUserRoleBridge(
                        new ModelService<AuthUserModel>().GetFirstByColumnName("UserName", "admin").ID,
                        new ModelService<UserRoleModel>().GetFirstByColumnName("Name", "Admin").ID
                    ),
                    new AuthUserUserRoleBridge(
                        new ModelService<AuthUserModel>().GetFirstByColumnName("UserName", "admin").ID,
                        new ModelService<UserRoleModel>().GetFirstByColumnName("Name", "User").ID
                    ),
                    new AuthUserUserRoleBridge(
                        new ModelService<AuthUserModel>().GetFirstByColumnName("UserName", "user").ID,
                        new ModelService<UserRoleModel>().GetFirstByColumnName("Name", "User").ID
                    )
                })
        };

        public new void Run() {
            Add(_systemBridgeSeeders);
            base.Run();
        }
    }
}