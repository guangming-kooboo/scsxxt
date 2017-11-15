using Core.Services;
using Core.Services.Entity;
using ServicePlatform.Areas.App.ViewModel.Visitor;
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

namespace ServicePlatform.Areas.App.Controllers
{
    public class VisitorController : BaseController
    {
        //
        // GET: /App/Visitor/Login     
        private IAppService _app;
        public VisitorController(IAppService app)
        {
            _app = app;         
        }
        public ActionResult ForgetPsw()
        {
            return View(ForgetPsw_M.ToViewModel(_app.GetSchoolIItems()));
        }
        [HttpPost]
        public ActionResult ForgetPsw(ForgetPsw_M form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Alert();
                }
                else
                {
                    var uid = F("UnitID") + "-" + F("UserID");
                    var pwd = F("UserPwd");
                    var SecondPwd= F("SecondPwd");
                    var StuName = F("StuName");
                    if (StuName == _app.GetStuName(uid))
                    {
                        if (pwd == SecondPwd)
                        {
                            if (_app.ForgetPsw(uid, pwd))
                            {
                                return Redirect("/App/Visitor/Login");
                            }
                            else
                            {
                                return Alert("操作错误");
                            }
                        }
                        else
                        {
                            return Alert("请检查密码", -1);
                        }
                    }
                    else
                    {
                        return Alert("名字错误，请检查", -1);
                    }
                }
            }
            catch (Exception ex)
            {
                return Alert(ex.Message);
            }
        }

        public ActionResult Login()
        {         
           return View(Login_M.ToViewModel(_app.GetSchoolIItems()));
        }
        [HttpPost]
        public ActionResult Login(Login_M form) 
        {          
            var uid=F("UnitID") + "-" +F("UserID");
            var pwd = F("UserPwd");

            DataContext.SetFiled("UserID", uid); DataContext.SetFiled("UserPwd", pwd);
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

                var employee = S<Core.Services.Entity.T_Employee>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                var enterprise = S<Core.Services.Entity.T_Enterprise>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                var teacher = S<Core.Services.Entity.T_Faculty>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                var student = S<Core.Services.Entity.T_Student>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                var school = S<Core.Services.Entity.T_School>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                var platformer = S<Core.Services.Entity.T_Platformer>().All().Where(a => a.UserID == DataContext.UserID).ToList();
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
                return Redirect("/App/Home/UserInfo?uid=" + uid);

            }
            else
            {
                return Alert("登录失败：用户名或密码错误！",-1);
            }
           
        }

    }
}
