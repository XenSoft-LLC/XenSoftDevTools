using System;
using System.Collections.Generic;
using System.Text;

namespace XenRipper.src {
    interface IRipper {

        public void Import(string targetDirectory);

        public void Load(string tilesetName);
    }
}
