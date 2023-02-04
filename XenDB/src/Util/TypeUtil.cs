using System;
using XenDB.Model;

namespace XenDB.Util {
    public static class TypeUtil {
        public static bool IsAbstractModel<T>(this T type) where T : Type {
            return type.IsSubclassOf(typeof(AbstractModel));
        }
    }
}
