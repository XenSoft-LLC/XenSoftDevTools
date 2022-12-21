using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.Model;
using XenDB.XenDriver.Repository;
using XenDB.XenDriver.Service;
using XenDriver.Model;

namespace XenDB.XenDriver.Seeder {
    public class BaseSeeder<T>  where T : AbstractModel, new() {
        public List<T> Models = new List<T>();
        public BaseService<T> _service = new BaseService<T>();
        public void LoadModels(List<T> models)
        {
            Models.AddRange(models);
        }
        public void Seed()
        {
            Models.ForEach(m => { _service.Insert(m); });
        }
    }
}
