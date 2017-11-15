using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Controllers.Base
{
    public class BaseAjaxResult
    {
        public BaseAjaxResult(int state , string msg)
        {
            _state = state;
            _msg = msg;
        }
        protected int _state ;
        protected string _msg ;
    }
}