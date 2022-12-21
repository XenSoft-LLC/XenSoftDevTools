using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.Model;
using XenDB.XenDriver.Model.ModelForm;
using XenDriver.Model;

namespace XenDB.XenDriver.Factory {
    class FormModelFactory<T> where T : AbstractModel, new() {

        public static T generateObjectFromForm(BaseModelForm<T> modelForm) {
            return (T)Activator.CreateInstance(
               typeof(T)
            , new object[] { modelForm }
            );

        }

    }
}
