using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.Repository;
using XenDriver.Model;

namespace XenDB.XenDriver.Service {
    public abstract class AbstractService {

        public abstract IRepository Repository { get; }


        public bool checkIfExistsByID(string objectId)
        {

            return Repository.checkIfExistsByID(objectId);

        }

        public string selectByID(string objectId)
        {

            return Repository.GetByID(objectId);

        }

        public List<string> selectByColumnName(string objectId, string columnName)
        {

            return Repository.GetByColumnName(objectId, columnName);

        }

        public bool checkIfExistsByColumnName(string objectId, string columnName)
        {
            return checkIfExistsByColumnName(objectId, columnName);


        }
    }
}
