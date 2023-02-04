using System;
using XenDB.Connection;
using XenUtils.JsonIO;

namespace XenDB.Factory{
    public static class ConnectionStringConfigFactory {
        public static ConnectionStringConfig GenerateSavedConnectionConfig()
        {
            return JsonIO<ConnectionStringConfig>.loadObject(Environment.GetEnvironmentVariable("XenDBConfig"));
        }
    }
}
