using XenDB.Model;

namespace XenDB.System.Model {
    public class AuthUserForm : BaseModelForm<AuthUserModel> {
        public string UserName { get; set; }
        public string Password { get; set; }

        public AuthUserForm(string userName, string password) {
            UserName = userName;
            Password = password;
        }
    }
}
