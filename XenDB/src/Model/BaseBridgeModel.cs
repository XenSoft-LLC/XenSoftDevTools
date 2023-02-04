using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml.Linq;
using XenDB.Database;
using XenDB.Model;
using XenDB.Util;

namespace XenDB.Model {
    public class BaseBridgeModel<X, Y> : AbstractModel where X : AbstractModel where Y : AbstractModel {
        private static string _firstTypeName = typeof(X).Name.Substring(typeof(X).Name.Length - 5) == "Model" ? typeof(X).Name.Substring(0, typeof(X).Name.Length - 5) : 
            typeof(X).Name.Substring(0, typeof(X).Name.Length - 5);
        private static string _secondTypeName = typeof(Y).Name.Substring(typeof(X).Name.Length - 5) == "Model" ? typeof(Y).Name.Substring(0, typeof(X).Name.Length - 5) : 
            typeof(Y).Name.Substring(0, typeof(Y).Name.Length - 5);
        private static string _newTableName = $"{_firstTypeName}{_secondTypeName}";

        public override string TableName { get { return _newTableName.ToLower();  } }
    }
}
