using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.Repository;
using XenDriver.Model;

namespace XenDB.XenDriver.Service {
    public class AuthUserService : AbstractService {

        private AuthUserRepository _authUserRepository = new AuthUserRepository();

        public override IRepository Repository { get { return _authUserRepository; } }

        public void Signup(string userName, string password) {
            _authUserRepository.Signup(userName, password);
        }
    }
}
