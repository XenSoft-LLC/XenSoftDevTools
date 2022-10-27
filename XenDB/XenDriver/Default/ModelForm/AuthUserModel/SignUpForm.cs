using System;
using System.Collections.Generic;
using System.Text;
using XenDB.XenDriver.DB.Default.CoreModel;
using XenDB.XenDriver.Model.ModelForm;

namespace XenDB.XenDriver.DBBuilder.Default.CoreModel.CoreModelForms.AuthUser {
    class SignUpForm : AbstractModelForm<AuthUserModel> {

        public string UserName { get; set; }
        public string Password { get; set; }
        private string _formName = "Sign Up Form";

        public override string FormName {
            get { return _formName; }
        }
    }
}
