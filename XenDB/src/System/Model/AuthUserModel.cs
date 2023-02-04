using XenDB.Model;

namespace XenDB.System.Model {
    public class AuthUserModel : AbstractModel {
        private static string _tableName = "authuser";
        public override string TableName => _tableName;
        public string UserName { get; set; }
        private readonly string _password;
        public bool InBattle { get; set; }


        public AuthUserModel(){ }

        public AuthUserModel(string userName, string password)
        {
            UserName = userName;
            _password = password;
            InBattle = false;
        }

        public AuthUserModel(int id, string userName, int userRoleID, bool inBattle)
        {
            ID = id;
            UserName = userName;
            InBattle = inBattle;
        }

        public AuthUserModel(string userName, string password, int userRoleID, bool inBattle)
        {
            UserName = userName;
            _password = password;
            InBattle = inBattle;
        }
    }
}
