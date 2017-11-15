using Core.Services;
using Core.Services.Entity;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services.Utility;

namespace ServicePlatform.Areas.Platform.Controllers
{
    public class SAdminController : BaseController
    {
        //
        // GET: /Platform/SAdmin/



      
        public ActionResult Index(string Ent_No)
        {
            DataContext.SetFiled("Ent_No", Ent_No);
            return View(S<T_Employee>().All(DataContext, "该企业所有员工"));
        }

        public ActionResult Login(string Ent_No)
        {
            var user = new AccountServices().GetEntAdmin(Ent_No);
            return RedirectToAction("LoginExtent", "Home",new { UserID = user.UserID.Replace(Ent_No + "-", ""), UserPwd = user.UserPwd, Ent_No = Ent_No } );
        }

      
    }
}
