using System.Collections.Generic;
using XenDB.XenDriver.DB.Default.CoreModel;
using XenDriver.Model;

namespace XenDB.XenDriver.DB {
    public static class MasterTableList {

        private static List<AbstractModel> _tables = new List<AbstractModel>() {

        };

        public static List<AbstractModel> Tables {
            get { return _tables; }
            set { _tables = value;  }
        }

        public static void loadCoreModels() {
            _tables.Add(new AuthUserModel());
            // _tables.Add(new ActorModel()); // Are all actors combattants?
        }

        //TODO: Maybe delete
        public static void addModel(AbstractModel objectModel) {
            _tables.Add(objectModel);
        }
    }
}
