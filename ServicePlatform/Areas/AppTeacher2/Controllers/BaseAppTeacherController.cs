using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Tools;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppTeacher2.Controllers
{
    public class BaseAppTeacherController : BaseController
    {
        //
        // GET: /AppTeacher2/BaseAppTeacher/

        public ActionResult Index()
        {
            return View();
        }
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
                filterContext.Result = new RedirectResult("/AppStudent/Visitor/Login");
            }
        }

    }
}
