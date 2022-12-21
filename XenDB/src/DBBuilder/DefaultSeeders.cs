using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.DBBuilder.Default.Model;
using XenDB.XenDriver.Default.Model;
using XenDB.XenDriver.Model;
using XenDB.XenDriver.Seeder;
using XenDriver.Model;

namespace XenDB.XenDriver.DBBuilder {
    public static class DefaultSeeders {

        public static BaseSeeder<UserRoleModel> userRoleSeeder = new BaseSeeder<UserRoleModel>();
        public static BaseSeeder<AuthUserModel> authUserSeeder = new BaseSeeder<AuthUserModel>();

        private static void _loadDefaultData() {
            userRoleSeeder.LoadModels(new List<UserRoleModel>() { new UserRoleModel("User"), new UserRoleModel("Admin") });
            authUserSeeder.LoadModels(new List<AuthUserModel>() { new AuthUserModel("admin", "password", 2, false) , new AuthUserModel("user", "password", 1, false) });
        }

        private static void _seed()
        {
            userRoleSeeder.Seed();
            authUserSeeder.Seed();
        }

        public static void RunDefaultSeeders(){
            _loadDefaultData();
            _seed();
        }

    }
}
