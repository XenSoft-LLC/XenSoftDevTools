using System;
using XenDB.Model;

namespace XenDB.Factory {
    class FormModelFactory<T> where T : AbstractModel, new() {

        public static T generateObjectFromForm(BaseModelForm<T> modelForm) {
            return (T)Activator.CreateInstance(
               typeof(T)
            , new object[] { modelForm }
            );

        }

    }
}
