using System.Collections.Generic;
using XenDB.Service;
using XenDB.Model;
using XenDB.Driver;

namespace XenDB.Database {
    public class BaseSeeder<T> : ISeeder where T : AbstractModel, new() {
        public List<T> Models = new List<T>();

        public BaseSeeder(List<T> models) {
            _addModels(models);
        }

        private void _addModels(List<T> models) {
            Models.AddRange(models);
        }

        public void Seed() {
            Models.ForEach(m => { m.Insert(); });
        }
    }
}
