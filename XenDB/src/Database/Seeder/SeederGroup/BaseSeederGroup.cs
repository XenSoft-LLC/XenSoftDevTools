using System.Collections.Generic;
using System.Threading.Tasks;

namespace XenDB.Database {
    public class BaseSeederGroup {
        private List<ISeeder> _seeders = new List<ISeeder>();

        public BaseSeederGroup() { }

        public void Add(List<ISeeder> seeders) { _seeders.AddRange(seeders); }

        public void Run() {
            _seeders.ForEach(seeder => { seeder.Seed(); });
        }
    }
}
