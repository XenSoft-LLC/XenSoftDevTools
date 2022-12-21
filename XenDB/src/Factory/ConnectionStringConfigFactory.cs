using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.ConnectionConfig;
using XenUtils.JsonIO;

namespace XenDB.XenDriver.Factory{
    public class ConnectionStringConfigFactory {


        public ConnectionStringConfig generateSavedConnectionConfig()
        {
            return new JsonIO<ConnectionStringConfig>().loadObject("F:/Repository/XenSoftDevTools/XenDB/src/ConnectionThreadManager/ConnectionStringConfig/ConnectionStringConfig.json");
        }

    }
}
