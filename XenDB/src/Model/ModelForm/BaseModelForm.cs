using System;

namespace XenDB.Model {
    public class BaseModelForm<T> where T : AbstractModel {
        public BaseModelForm() {
        }

        public Type GetModelType {
            get { return typeof(T); }
        }
    }
}
