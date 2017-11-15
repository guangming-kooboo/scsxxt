using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services.Permission;
using Qx.Tools;
using ServicePlatform.Config;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.ServiceApi.Controllers
{
    public class MobileResult
    {
        public string State { get; set; }
        public object Data { get; set; }
    }
    public class MobileController : BaseController
    {
        // /ServiceApi/Mobile/Login?UserID=14093&UserPwd=14093
        public ActionResult Login(string UserID, string UserPwd, string UnitID="10385")
        {
            var result=new MobileResult();
            if (string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(UserPwd))
            {
                result.State = "401";
                result.Data = "用户名或密码不能为空";
            }

            #region 给用户自动添加ID前缀
            if (!string.IsNullOrEmpty(UnitID))
            {
                UserID = UnitID + "-" + UserID;//添加前缀
            }
            #endregion

            DataContext.SetFiled("UserID", UserID); DataContext.SetFiled("UserPwd", UserPwd);
            var sucess = new UserServices().All(DataContext, "登陆");
            if (sucess.Any())
            {
                var accountContext=new AccountContext()
                {
                    UserID = sucess.FirstOrDefault().UserID,
                    UserName = sucess.FirstOrDefault().NickName
                };
                Session["AccountContext"] = accountContext;
                result.State = "200";
                result.Data = accountContext;
            }
            else
            {
                result.State = "401";
                result.Data = "用户名或密码错误";  
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
