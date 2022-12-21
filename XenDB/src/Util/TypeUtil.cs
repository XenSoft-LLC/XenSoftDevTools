using System;
using System.Collections.Generic;
using System.Text;
using XenDriver.Model;

namespace XenDB.XenDriver.Util {
    public static class TypeUtil {
        public static bool IsAbstractModel(Type type) {
            return type.IsSubclassOf(typeof(AbstractModel));
        }
    }
}
