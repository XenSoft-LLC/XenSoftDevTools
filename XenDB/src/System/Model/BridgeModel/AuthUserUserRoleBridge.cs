using XenDB.src.Model;
using XenDB.System.Model;

namespace XenDB.src.System.Model.BridgeModel {
    public class AuthUserUserRoleBridge : BaseBridgeModel<AuthUserModel, UserRoleModel> {
        public AuthUserUserRoleBridge() { }
        public AuthUserUserRoleBridge(int authUserId, int userRoleId) {
            AuthUserID = authUserId;
            UserRoleID = userRoleId;
        }

        public int UserRoleID { get; set; }
        public int AuthUserID { get; set; }
    }
}
