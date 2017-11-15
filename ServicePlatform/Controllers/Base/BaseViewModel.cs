using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Controllers.Base
{
    public class BaseViewModel
    {
        //public  BaseViewModel(string condition, string action)
        //{
        //    _condition = condition;
        //    _action = action;
        //}
        public string _condition
        {
            get { return HttpContext.Current.Request["_condition"]; }
        }

    }
}