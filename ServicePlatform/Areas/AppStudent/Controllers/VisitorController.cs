using Core.Services;
using Core.Services.Entity;
using ServicePlatform.Areas.AppStudent.ViewModel.Visitor;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services.Permission;
using Qx.Scsxxt.Core.Services;
using Qx.Tools;
using ServicePlatform.Config;
using ServicePlatform.Models;

namespace ServicePlatform.Areas.AppStudent.Controllers
{
    public class VisitorController : BaseAppStudentController
    {
        //
        // GET: /AppStudent/Visitor/Login     
        SchoolContext sc = new SchoolContext();
        private IAppService _app;
        public VisitorController(IAppService app)
        {
            _app = app;         
        }

        public ActionResult Test()
        {
            return Json(new {requestId = "123456"},JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgetPsw()
        {
            InitAppStudentLayout("忘记密码");
            return View(ForgetPsw_M.ToViewModel(_app.GetSchoolIItems()));
        }
        //[HttpPost]
        //public ActionResult ForgetPsw(ForgetPsw_M form)
        //{
        //    if (form._condition == "")
        //    {
        //        return Redirect("/AppStudent/Visitor/ForgetPsw");
        //    }
        //    else
        //    {
        //        return RedirectToNext(form._condition);
        //    }

        //}

        public ActionResult Login()
        {
            
            InitAppStudentLayout("登录");
            return View(Login_M.ToViewModel(_app.GetSchoolIItems()));
        }
        [HttpPost]
        public ActionResult Login(Login_M form) 
        {
            //清除session
            Session.Clear();
            var uid=F("UnitID") + "-" +F("UserID");
            var pwd = F("UserPwd");
            if (uid == null || pwd == "")
            {
                return WxAlert("请将信息填写完整");
            }
            else
            {
                DataContext.SetFiled("UserID", uid); DataContext.SetFiled("UserPwd", pwd);
                var sucess = new UserServices().All(DataContext, "登录");
                if (sucess.Any())
                {
                    #region 初始化用户信息
                    var Vars = new ServicePlatform.Config.Vars(uid);
                    Vars.Init();
                    Vars.msgCount = sc.T_MsgRec.Count(a => a.Receiver == form.UserID && a.ReadTime == -1);

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
                    //var enterprise = S<Core.Services.Entity.T_Enterprise>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    var teacher = S<Core.Services.Entity.T_Faculty>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    var student = S<Core.Services.Entity.T_Student>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    //var school = S<Core.Services.Entity.T_School>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    var platformer = S<Core.Services.Entity.T_Platformer>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                    if (employee.Any())
                    {

                        DataContext.IsEmployee = true;
                        DataContext.IsEnterprise = true;
                    }
                    //if (enterprise.Count > 0)
                    //{
                    //    DataContext.IsEnterprise = true;
                    //}
                    if (teacher.Any())
                    {
                        DataContext.IsTeacher = true;
                        DataContext.IsSchool = true;
                    }
                    if (student.Any())
                    {
                        DataContext.IsStudent = true;
                        DataContext.IsSchool = true;
                    }
                    //if (school.Count > 0)
                    //{
                    //    DataContext.IsSchool = true;
                    //}
                    if (platformer.Any())
                    {
                        DataContext.IsPlatform = true;
                    }
                    #endregion
                    DataContext.IsLogin = true;                 
                    Session["AccountContext"] = accountContext;
                    if (DataContext.IsStudent) //学生端
                    {
                        if (!string.IsNullOrEmpty(_app.FindStu(DataContext.UserID).T_Student.StuCellphone))
                        {
                            return RedirectToNext(form._condition);
                        }
                        else
                        {
                            return Redirect("/AppStudent/Home/CompleteInfo");
                        }
                    }
                    if (DataContext.IsTeacher)//教师端
                    {
                        return Redirect("/AppTeacher2/THome/HomePage");
                    }
                    else
                    {
                        return WxAlert("抱歉，没有权限进入");
                    }
                }
                else
                {
                    return WxAlert("登录失败：用户名或密码错误！");
                }
            }
        }

        public ActionResult RevisePassword()
        {
            InitAppStudentLayout("重置密码");
            return View(RevisePassword_M.ToViewModel(_app.GetSchoolIItems()));
        }

        [HttpPost]
        public ActionResult RevisePassword(RevisePassword_M form)
        {
            var uid = form.unitid + "-" + form.userid;
            if (_app.FindStu(uid) != null)
            {
                if (form.NewPsw1 != null && form.NewPsw1 != null && form.NewPsw2 != null)
                {
                    if (form.NewPsw1 != form.NewPsw2)
                    {
                        return WxAlert("密码不匹配", "提示");
                    }
                    else
                    {
                        if (_app.ForgetPsw(uid, form.NewPsw1))
                        {
                            Session.Clear();
                            return WxAlert("重置密码成功", "恭喜", "/AppStudent/Visitor/Login");
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
                return WxAlert("请注意信息填写正确");
            }
        }
        public ActionResult Login2()
        {
            InitAppStudentLayout("登录");
            return View(Login2_M.ToViewModel(_app.GetSchoolIItems()));
        }

        [HttpPost]
        public ActionResult Login2(Login_M form)
        {
            switch (form._condition)
            {
                case "a":
                    return RedirectToNext(form._condition);
                    break;
                default:
                {
                    //return RedirectToNext(form._condition);
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
                                S<Core.Services.Entity.T_Employee>()
                                    .All()
                                    .Where(a => a.UserID == DataContext.UserID)
                                    .ToList();
                            var enterprise =
                                S<Core.Services.Entity.T_Enterprise>()
                                    .All()
                                    .Where(a => a.UserID == DataContext.UserID)
                                    .ToList();
                            var teacher =
                                S<Core.Services.Entity.T_Faculty>()
                                    .All()
                                    .Where(a => a.UserID == DataContext.UserID)
                                    .ToList();
                            var student =
                                S<Core.Services.Entity.T_Student>()
                                    .All()
                                    .Where(a => a.UserID == DataContext.UserID)
                                    .ToList();
                            var school =
                                S<Core.Services.Entity.T_School>()
                                    .All()
                                    .Where(a => a.UserID == DataContext.UserID)
                                    .ToList();
                            var platformer =
                                S<Core.Services.Entity.T_Platformer>()
                                    .All()
                                    .Where(a => a.UserID == DataContext.UserID)
                                    .ToList();
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
                            return RedirectToNext(form._condition);
                        }
                        else
                        {
                            return WxAlert("登录失败：用户名或密码错误！");
                        }
                    }
                }
            }
        }
    }
}
