using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using System.Data.Entity;
using Qx.Scsxxt.Core.Services.Permission;
using Qx.Tools;
using ServicePlatform.App_Start;

using ServicePlatform.Areas.Platform.Models.Home;
using ServicePlatform.Config;
using ServicePlatform.Controllers.Base;
using ServicePlatform.Lib;


namespace ServicePlatform.Areas.Platform.Controllers
{
    public class HomeController : BaseController
    {
        EnterpriseContext db = new EnterpriseContext();
        UserContext uc = new UserContext();
        SchoolContext sc = new SchoolContext();
        ContentContext cc = new ContentContext();
        //
        // GET: /Platform/Home/

       
        public ActionResult Index()
        {

            return RedirectToAction("Login");
            var model=new Index()
            {
                SchoolList = S<Core.Services.Entity.T_School>().All(DataContext),
                EnterpriseList = S<Core.Services.Entity.T_Enterprise>().All(DataContext),
                PlateInfoList801= S<Core.Services.Entity.T_HomePageContent>().All(DataContext,"平台栏目801"),
                PlateInfoList802 = S<Core.Services.Entity.T_HomePageContent>().All(DataContext, "平台栏目802"),
                PlateInfoList803 = S<Core.Services.Entity.T_HomePageContent>().All(DataContext, "平台栏目803"),
                PlateInfoList804 = S<Core.Services.Entity.T_HomePageContent>().All(DataContext, "平台栏目804"),
                PlateInfoList805 = S<Core.Services.Entity.T_HomePageContent>().All(DataContext, "平台栏目805")
            };
            foreach (var item in model.SchoolList)
            {
               // DataContext.SetFiled("UnitID", item.SchoolID);
               // var logo = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的Logo").FirstOrDefault();
              //  item.SchoolLogo = logo==null?"#":logo.DLFileUrl;
            }
            foreach (var item in model.EnterpriseList)
            {
                DataContext.SetFiled("UnitID", item.Ent_No);
                var logo = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的Logo").FirstOrDefault();
                item.EntLogo = logo == null ? "#" : logo.DLFileUrl;
            }

            return View(model);
        }

        #region 登录
        public ActionResult Login(string  UnitID="")
        {
            if (!string.IsNullOrEmpty(DataContext.UserUnit))
            {
                return Redirect("/Platform/Admin/Index");
            }
            else
            {
             var model=new Login()
             {
                 SchoolItems = S<Core.Services.Entity.T_School>().ToSelectItems(UnitID),
                 EntItems = S<Core.Services.Entity.T_Enterprise>().ToSelectItems(UnitID),
             };
             return View(model);
            }
           
        }
        public ActionResult LoginOut()
        {
            //退出登录前准备工作
            Session.Clear();
            if (Session["Vars"] != null)
            {
                Session.Remove("Vars");
            }
            return Redirect("/Platform/Admin/Index");   
        }
        public ActionResult LoginExtent(string UserID, string UserPwd, string Ent_No, string SchoolID)
        {
            return Login(new Login() { UserID = UserID, UserPwd = UserPwd, Ent_No = Ent_No, SchoolID = SchoolID });
        }
        [HttpPost]
        public ActionResult Login(Login form )
        {
             if (string.IsNullOrEmpty(form.UserID)|| string.IsNullOrEmpty(form.UserPwd))
            {
                return Alert("用户名或密码不能为空！");
            }

            #region 给用户自动添加ID前缀
            var UnitID = form.getUnitID();//所属组织
            if (!string.IsNullOrEmpty(UnitID))
            {
                form.UserID = UnitID+"-"+form.UserID;//添加前缀
            }
            #endregion

            DataContext.SetFiled("UserID",form.UserID); DataContext.SetFiled("UserPwd", form.UserPwd);
            var sucess = new UserServices().All(DataContext, "登录");
            if (sucess.Any())
            {
                #region 初始化用户信息
                var Vars = new ServicePlatform.Config.Vars(form.UserID);
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
                 accountContext.UserID= IsLogin.UserID;
                 DataContext.UserName =
                 accountContext.UserName=
                 sucess.FirstOrDefault().NickName;
                 DataContext.UserUnit = IsLogin.UserUnit;
                 DataContext.PracBatchID = info == null ? "查找当前批次失败" : info.PracBatchID;

                 var employee = S<Core.Services.Entity.T_Employee>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                 //var enterprise = S<Core.Services.Entity.T_Enterprise>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                 var teacher = S<Core.Services.Entity.T_Faculty>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                 var student = S<Core.Services.Entity.T_Student>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                 //var school = S<Core.Services.Entity.T_School>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                 var platformer = S<Core.Services.Entity.T_Platformer>().All().Where(a => a.UserID == DataContext.UserID).ToList();
                 if (employee.Count > 0)
                 {

                     DataContext.IsEmployee = true;
                    DataContext.IsEnterprise = true;
                }
                 //if (enterprise.Count > 0)
                 //{
                 //    DataContext.IsEnterprise = true;
                 //}
                 if (teacher.Count > 0)
                 {
                     DataContext.IsTeacher = true;
                    DataContext.IsSchool = true;
                }
                 if (student.Count > 0)
                 {
                     DataContext.IsStudent = true;
                    DataContext.IsSchool = true;
                }
                 //if (school.Count > 0)
                 //{
                 //    DataContext.IsSchool = true;
                 //}
                 if (platformer.Count > 0)
                 {
                     DataContext.IsPlatform = true;
                 }
                #endregion

                DataContext.IsLogin = true;
                Session["AccountContext"] = accountContext;
                if(ReturnUrl=="/"||ReturnUrl.Contains("Login"))
                {
                    return Redirect("/Platform/Admin/Index");
                }
                return Redirect(ReturnUrl);

            }
            else
            {
                return Alert("登录失败：用户名或密码错误！",-1);
            }
        }

        #endregion

        public ActionResult EasyLogin(string  uid)
        {

            if (string.IsNullOrEmpty(uid))
            {
                return Alert("用户名或密码不能为空！");
            }


            DataContext.SetFiled("UserID", uid);
            var sucess = new UserServices().All(DataContext, "开发者登录");
            if (sucess.Any())
            {
                #region 初始化用户信息
                var Vars = new ServicePlatform.Config.Vars(uid);
                Vars.Init();
                Vars.msgCount = sc.T_MsgRec.Count(a => a.Receiver == uid && a.ReadTime == -1);

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
                if (employee.Count > 0)
                {

                    DataContext.IsEmployee = true;
                    DataContext.IsEnterprise = true;
                }
                //if (enterprise.Count > 0)
                //{
                //    DataContext.IsEnterprise = true;
                //}
                if (teacher.Count > 0)
                {
                    DataContext.IsTeacher = true;
                    DataContext.IsSchool = true;
                }
                if (student.Count > 0)
                {
                    DataContext.IsStudent = true;
                    DataContext.IsSchool = true;
                }
                //if (school.Count > 0)
                //{
                //    DataContext.IsSchool = true;
                //}
                if (platformer.Count > 0)
                {
                    DataContext.IsPlatform = true;
                }
                #endregion

                DataContext.IsLogin = true;
                Session["AccountContext"] = accountContext;
                if (ReturnUrl == "/" || ReturnUrl.Contains("Login"))
                {
                    return Redirect("/Platform/Admin/Index");
                }
                return Redirect(ReturnUrl);

            }
            else
            {
                return Alert("登录失败：用户名不存在！", -1);
            }
        }
        public ActionResult Forget()
        {
            return Alert("请联系管理员重置密码");
        }

        #region 注册
        public ActionResult Regist()
        {
            return Alert("感谢您的支出，请联系管理员商议入驻事宜！");
           // return View();
        }
        [HttpPost]
        public ActionResult Regist(T_User form)
        {
           

            if (Request.Form["UserPwd2"] == null || Request.Form["UserID"] == null)
            {
                return Content("注册失败:请认真填写用户名和密码");
            }
            else if (uc.T_User.Find(form.UserID) != null)
            {
                return Content("注册失败:用户ID已存在");
            }

            else if (form.UserPwd != Request.Form["UserPwd2"])
            {
                return Content("注册失败:未填写密码或2次密码不一致");
            }
            else
            {
              
                form.UserPwd = ServicePlatform.Lib.EncryptString.NoneEncrypt(form.UserPwd);//密文存储密码         
                //构造日志
                form.Note += DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day + "日" + DateTime.Now.ToShortTimeString() + "注册了账号 (" + "操作者:" + form.UserID + ");";

                uc.T_User.Add(form);
                return uc.SaveChanges() > 0 ? Content("注册成功") : Content("注册失败");                            

            }

        }
        #endregion

        #region 企业管理 
        [BaseActionFilter]
        public ActionResult T_Enterprise_ListShow(string keyWord="",int perCount = 20, int pageIndex = 1)
        {
            var data=db.T_Enterprise.Where(m => m.Ent_Name.Contains(keyWord)).ToList();
            //分页相关开始
            ViewBag.link = "/Platform/Home/T_Enterprise_ListShow";
            ViewBag.addLink = "/Permission/Home/UnitAdd";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_Enterprise>(data, perCount, pageIndex);
            //分页相关结束

            ViewBag.Title = "企业列表";
            ViewBag.Limit = 2;
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_Enterprise_Note())[1];
            //ViewBag.Action = new string[][]
            // {
                
            //    //new string[] {"/Platform/Home/T_Enterprise_Check_List","Ent_No","审核实习申请"},           
            //    new string[] {"/Enterprise/Home/T_Enterprise_Show","Ent_No","查看资料"},                
            //     new string[] {"/Platform/Home/T_Enterprise_UnLock","Ent_No","解锁"},
            //      new string[] {"/Platform/Home/T_Enterprise_Lock","Ent_No","锁定"},
            //       new string[] {"/Platform/Home/T_Enterprise_Suspend","Ent_No","挂起"},
            //      new string[] {"/Platform/Home/T_Enterprise_UnSuspend","Ent_No","解挂"},
            //      new string[] {"/Platform/Home/T_Enterprise_Respring","Ent_No","注销"},
            //      new string[] {"/Platform/Home/T_Enterprise_UnRespring","Ent_No","解注"},
            //      //new string[] {"/Enterprise/Home/T_Enterprise_Edict","Ent_No","修改"},
            //    new string[] {"/Platform/Home/T_Enterprise_Delete","Ent_No","删除企业"}
            // };
           
            return View();
        }

        #region 审核企业实习批次
        //审核企业实习批次申请[企业批次表]
        public ActionResult T_Enterprise_Check_List(string Ent_No, int perCount = 8, int pageIndex = 1)
        {
            //关键词User
            Vars vars = (Session["Vars"] as Vars);
            //分页相关开始
            var data = db.T_EntBatchReg.Where(m => m.Ent_No == Ent_No).ToList();//原始数据
            ViewBag.link = "/Platform/Home/T_Enterprise_ListShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_EntBatchReg>(data, perCount, pageIndex);//分页操作
            //分页相关结束

          
            ViewBag.Title = "企业列表";
            ViewBag.Limit = 2;
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_Enterprise_Note())[1];
            ViewBag.Action = new string[][]
             {
                
                new string[] {"/Platform/Home/T_Enterprise_Check_OK","EntPracNo","通过"},
                new string[] {"/Platform/Home/T_Enterprise_Check_NotOK","EntPracNo","拒绝"},
                new string[] {"/Platform/Home/T_Enterprise_Check_Delete","EntPracNo","删除"}
              
             };

            return View("ListShow");
        }

        //通过企业实习批次申请[企业批次表]
        public bool T_Enterprise_Check_OK(string EntPracNo)
        {
            //改变状态
            var f = (from t in db.T_EntBatchReg where t.EntPracNo == EntPracNo select t).FirstOrDefault();

            f.ApplyStatus = 2;
            db.Entry(f).State = EntityState.Modified;
            return db.SaveChanges() > 0 ? true : false;
        }
        //拒绝企业实习批次申请[企业批次表]
        public bool T_Enterprise_Check_NotOK(string EntPracNo)
        {
            //改变状态
            var f = (from t in db.T_EntBatchReg where t.EntPracNo == EntPracNo select t).FirstOrDefault();

            f.ApplyStatus = 3;
            db.Entry(f).State = EntityState.Modified;
            return db.SaveChanges() > 0 ? true : false;
        }
        //删除企业实习批次申请[企业批次表]
        public bool T_Enterprise_Check_Delete(string EntPracNo)
        {
            //改变状态
            var f = (from t in db.T_EntBatchReg where t.EntPracNo == EntPracNo select t).FirstOrDefault();
            f.ApplyStatus = 4;
            db.Entry(f).State = EntityState.Modified;
            return db.SaveChanges() > 0 ? true : false;
        }
       
        #endregion


        //企业资料解锁[企业表]
        public ActionResult T_Enterprise_UnLock(string Ent_No)
        {
            var f = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();
         
           f.InfoLocked = EditStatus.UnLock;//运用全局常量
           db.Entry(f).State = EntityState.Modified;
           db.SaveChanges();
           return RedirectToAction("T_Enterprise_ListShow");
        }
        //企业资料锁定[企业表]
        public ActionResult T_Enterprise_Lock(string Ent_No)
        {
            var f = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();

            f.InfoLocked = EditStatus.Locked;
            db.Entry(f).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("T_Enterprise_ListShow");
        }
        //企业挂起[企业表]
        public ActionResult T_Enterprise_Suspend(string Ent_No)
        {
            var f = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();

            f.EntState = UnitStatus.Suspend;
            db.Entry(f).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("T_Enterprise_ListShow");
        }
        //企业解挂[企业表]
        public ActionResult T_Enterprise_UnSuspend(string Ent_No)
        {
            var f = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();

            f.EntState = UnitStatus.Approved;
            db.Entry(f).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("T_Enterprise_ListShow");
        }
        //企业解除注销[企业表]
        public ActionResult T_Enterprise_UnRespring(string Ent_No)
        {
            var f = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();

            f.EntState = UnitStatus.Approved;
            db.Entry(f).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("T_Enterprise_ListShow");
        }
        //企业注销[企业表]
        public ActionResult T_Enterprise_Respring(string Ent_No)
        {
            var f = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();

            f.EntState = UnitStatus.Respring;
            db.Entry(f).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("T_Enterprise_ListShow");
        }
        //删除企业
        public ActionResult T_Enterprise_Delete(string Ent_No)
        {
            //var f = (from t in db.T_Enterprise where t.Ent_No == Ent_No select t).FirstOrDefault();

            //f.EntState = UnitStatus.Respring;
            //db.Entry(f).State = EntityState.Modified;
            //return db.SaveChanges() > 0 ? true : false;
            string sqlStr;
            sqlStr = "Delete From T_Enterprise Where Ent_No='" + Ent_No + "'";
            try
            {
                db.Database.ExecuteSqlCommand(sqlStr);
            }
            catch (Exception e)
            {
               // return false;
            }
            if (DeleteEntDirectorys(Ent_No) == -1)
            {
               // return false;
            }

            db.SaveChanges();
            return RedirectToAction("T_Enterprise_ListShow");

        }
        //企业重置密码
        public ActionResult T_Enterprise_RevisePSW(string EntNo)
        {
            //var ent = (from t in db.T_Enterprise where t.Ent_No == EntNo select t).FirstOrDefault().UserID;
            //var uid = db.T_Employee.FirstOrDefault(o => o.Ent_No == EntNo).UserID;//获取该企业的管理员ID
            ViewBag.entno = EntNo;
            return View();
        }
        [HttpPost]
        public ActionResult T_Enterprise_RevisePSW(string EntNo, string newpsw, string newpsw2)
        {
            try
            {

                if (!string.IsNullOrEmpty(newpsw) && !string.IsNullOrEmpty(newpsw2))
                {
                    if (newpsw == newpsw2)
                    {
                        var employee = db.T_Employee.FirstOrDefault(o => o.Ent_No == EntNo);
                        if (employee != null)
                        {
                            var uid = employee.UserID; //获取该企业的管理员ID
                            var user = db.T_User.FirstOrDefault(a => a.UserID == uid);
                            if (user != null)
                            {
                                user.UserPwd = Lib.EncryptString.NoneEncrypt(newpsw);
                                db.Entry(user).State = EntityState.Modified;
                                db.SaveChanges();
                                return Alert("重置密码成功", -1);
                            }
                            else
                            {
                                return Alert("不存在该用户", -1);
                            }
                        }
                        else
                        {
                            return Alert("不存在该企业的管理员", -1);
                        }
                    }
                    else
                    {
                        return Alert("两次密码不一致，请重新填写", -1);
                    }
                }
                else
                {
                    return Alert("请将信息填写完整", -1);
                }
            }
            catch (Exception ex)
            {
                return Alert(ex.Message);
            }
        }
        //生成 企业目录
        public int CreateEntDirectorys(string EntNo)
        {
            if (string.IsNullOrEmpty(EntNo))
            {
                return -1;
            }
            else
            {
                var EntDirectorysHome = "/UserFiles/Enterprise/" + EntNo;
                return FileOperate.CreateDirectorys(new List<string>()
            {
                EntDirectorysHome+"/EntAdPics",
                EntDirectorysHome+"/EntFiles",
                EntDirectorysHome+"/EntLogo",
                EntDirectorysHome+"/EntPhotos",
                EntDirectorysHome+"/EntPPTs",
                EntDirectorysHome+"/EntVideos",
                EntDirectorysHome+"/Contents/Ads",
                EntDirectorysHome+"/Contents/DownLoadFiles",
                EntDirectorysHome+"/Contents/News",
                EntDirectorysHome+"/Contents/Signatures",
                EntDirectorysHome+"/Contents/Stamps"
            });
            }

        }
        //删除企业目录
        public int DeleteEntDirectorys(string EntNo)
        {
            if (string.IsNullOrEmpty(EntNo))
            {
                return -1;
            }
            else
            {
                var EntDirectorysHome = "/UserFiles/Enterprise/" + EntNo;
                return FileOperate.DeleteDirectorys(new List<string>()
            {
                EntDirectorysHome
            }
            );
           
            }
        }


        #endregion



    }
}