using System;
using XenDB.Service;
using XenDB.System.Model;

namespace XenDB.System.Service {
    public static class AuthUserService {
        public static AuthUserModel Signup<T>(this T service, string userName, string password) where T : ModelService<AuthUserModel> {
            try {
                //TODO: Add input validation
                return service.Insert(new AuthUserModel(userName, password));
            } catch {
                throw new Exception("Invalid username/password combination.");
            }
        }

        public static AuthUserModel Login<T>(this T service, string userName, string password) where T : ModelService<AuthUserModel> {
            //TODO: Add input validation
            //TODO: Add separare stored query to do username/password check in one query, rather than pulling the model into memory by password.
            AuthUserModel result = service.GetFirstByColumnName("password", password);
            if (!result.Equals(null) && result.UserName == userName) {
                return result;
            } else {
                throw new Exception("Invalid username/password combination.");
            }
        }
    }

}

