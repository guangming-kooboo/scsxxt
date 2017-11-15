using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;
using Qx.Tools;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.App.Controllers
{
    public class BaseAppController : BaseController
    {
       
        protected AccountContext AccountContext
        {
            get
            {
                return Session["AccountContext"] as AccountContext;
            }
            set
            {
                Session["AccountContext"] = value;
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AccountContext == null)//已登录
            {
                Session["ReturnUrl"] = Request.RawUrl;
                filterContext.Result = new RedirectResult("/App/Visitor/Login");
            }
        }
    }
}
