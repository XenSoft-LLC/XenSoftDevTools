using System;
using System.Collections.Generic;
using System.Text;
using XenDriver.Model;

namespace XenDB.XenDriver.Model.ModelForm {
    public abstract class BaseModelForm<T> where T : AbstractModel {

        abstract public string FormName { get; }

        public Type GetModelType
        {
            get { return typeof(T); }
        }

    }
}
