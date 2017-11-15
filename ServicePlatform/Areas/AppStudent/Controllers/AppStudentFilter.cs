using System.Collections.Generic;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Controllers.Base;
using System.Web.Mvc;
using Qx.Tools;

namespace ServicePlatform.Areas.AppStudent.Controllers
{
    public class AppStudentFilter : BaseAppStudentController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AccountContext == null)//no登录
            {
                Session["ReturnUrl"] = Request.RawUrl;
                filterContext.Result = new RedirectResult("/AppStudent/Visitor/Login");
            }
        }
    }
}