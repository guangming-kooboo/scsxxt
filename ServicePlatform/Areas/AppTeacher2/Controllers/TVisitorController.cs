using Core.Services;
using Core.Services.Entity;
using ServicePlatform.Areas.AppTeacher2.ViewModel.TVisitor;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services.Permission;
using Qx.Scsxxt.Core.Services;
using Qx.Tools;
using ServicePlatform.Config;
using Core.Interfaces;

namespace ServicePlatform.Areas.AppTeacher2.Controllers
{
    public class TVisitorController : BaseController
    {
        //
        // GET: /AppTeacher2/TVisitor/
        private bool states = true;
        private TAppInterface _TApp;

        public TVisitorController(TAppInterface TApp)
        {
            _TApp = TApp;

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(Login_M.ToViewModel(_TApp.GetSchoolIItems()));
        }

        [HttpPost]
        public ActionResult Login(Login_M model)
        {
            var uid = F("UnitID") + "-" + F("UserID");
            var pwd = F("UserPwd");
            if (uid == null || pwd == "")
            {
                return WxAlert("请将信息填写完整");
            }
            else
            {
                DataContext.SetFiled("UserID", uid);
                DataContext.SetFiled("UserPwd", pwd);
                var sucess = new UserServices().All(DataContext, "登录");
                if (sucess.Any())
                {
                    #region 初始化用户信息

                    var Vars = new ServicePlatform.Config.Vars(uid);
                    Vars.Init();
                    //Vars.msgCount = sc.T_MsgRec.Count(a => a.Receiver == form.UserID && a.ReadTime == -1);

                    #endregion

                    Session["Vars"] = Vars;

                    #region 初始化扩展

                    var IsLogin = (Session["Vars"] as Vars);
                    var info = S<Core.Services.Entity.T_PracBatch>()
                        .All(o => o.IsActive == 1 && o.IsCurrentBatch == "是" && o.SchoolID == IsLogin.UserUnit)
                        .FirstOrDefault();
                    var accountContext = new AccountContext();
                    DataContext.UserID =
                        accountContext.UserID = IsLogin.UserID;
                    DataContext.UserName =
                        accountContext.UserName =
                            sucess.FirstOrDefault().NickName;
                    DataContext.UserUnit = IsLogin.UserUnit;
                    DataContext.PracBatchID = info == null ? "查找当前批次失败" : info.PracBatchID;

                    var employee =
                        S<Core.Services.Entity.T_Employee>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    var enterprise =
                        S<Core.Services.Entity.T_Enterprise>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    var teacher =
                        S<Core.Services.Entity.T_Faculty>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    var student =
                        S<Core.Services.Entity.T_Student>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    var school =
                        S<Core.Services.Entity.T_School>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    var platformer =
                        S<Core.Services.Entity.T_Platformer>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    if (employee.Count > 0)
                    {
                        DataContext.IsEmployee = true;
                    }
                    if (enterprise.Count > 0)
                    {
                        DataContext.IsEnterprise = true;
                    }
                    if (teacher.Count > 0)
                    {
                        DataContext.IsTeacher = true;
                    }
                    if (student.Count > 0)
                    {
                        DataContext.IsStudent = true;
                    }
                    if (school.Count > 0)
                    {
                        DataContext.IsSchool = true;
                    }
                    if (platformer.Count > 0)
                    {
                        DataContext.IsPlatform = true;
                    }

                    #endregion

                    DataContext.IsLogin = true;
                    Session["AccountContext"] = accountContext;
                    return Redirect("/AppTeacher2/THome/HomePage");
                }
                else
                {
                    return WxAlert("登录失败：用户名或密码错误！");
                }
            }
        }

        public ActionResult ForgetPsw()
        {
            return View(ForgetPsw_M.ToViewModel(_TApp.GetSchoolIItems()));
        }

        public ActionResult RevisePsw()
        {
            return View(RevisePsw_M.ToViewModel(_TApp.GetSchoolIItems()));
        }

        [HttpPost]
        public ActionResult RevisePsw(RevisePsw_M form)
        {
            var uid = form.unitid + "-" + form.userid;
            if (_TApp.FindTeacher(uid) != null)
            {
                if (form.NewPsw1 != null&& form.NewPsw2 != null)
                {
                    if (form.NewPsw1 != form.NewPsw2)
                    {
                        return WxAlert("密码不匹配", "提示");
                    }
                    else
                    {
                        if (_TApp.ForgetPsw(uid, form.NewPsw1))
                        {
                            return WxAlert("重置密码成功", "恭喜", "/AppTeacher2/TVisitor/Login");
                        }
                        else
                        {
                            return WxAlert("重置密码失败");
                        }
                    }
                }
                else
                {
                    return WxAlert("请填写新密码");
                }
            }
            else
            {
                return WxAlert("请填写正确信息");
            }
        }
    }
}
