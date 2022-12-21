using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.Repository;
using XenDriver.Model;

namespace XenDB.XenDriver.Service {
    public class BaseService<T> where T : AbstractModel, new() {

        public int Insert(T model){
            return BaseRepository.Insert(model).ID;
        }

        public T GetByID(string objectId)
        {
            return BaseRepository.SelectByID<T>(objectId);
        }

        public List<T> GetAll()
        {
            return BaseRepository.SelectAll<T>();
        }

        public List<T> GetByColumnName(string objectId, string columnName)
        {
            return BaseRepository.SelectByColumnName<T>(objectId, columnName);
        }

        public void Update()
        {
            
        }
    }
}
