using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.Model;
using XenDriver.Model;

namespace XenDB.XenDriver.Seeder {
    interface ISeeder<T> {

        void Seed();

    }
}
