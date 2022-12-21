using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.Default.Model;
using XenDB.XenDriver.Repository;
using XenDriver.Model;

namespace XenDB.XenDriver.Service {
    public class AuthUserService : BaseService<AuthUserModel> {
        public AuthUserModel Signup(string userName, string password) {
            AuthUserModel _userModel = new AuthUserModel(userName, password);
            return BaseRepository.Insert(_userModel);
        }

        public AuthUserModel Login(string userName, string password)
        {
            AuthUserModel result = BaseRepository.SelectByColumnName<AuthUserModel>("password", password)[0];
            if(result.UserName == userName) {
                return result;
            } else {
                return null;
            }
        }
    }

}

