using System;
using System.Collections.Generic;
using System.Text;

namespace XenDB.XenDriver.Repository {
    public interface IRepository {

        public bool checkIfExistsByID(string objectId);
        public string GetByID(string objectId);
        public bool checkIfExistsByColumnName(string objectId, string columnName);
        public List<string> GetByColumnName(string objectId, string columnName);
    }
}
