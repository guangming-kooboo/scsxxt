using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Lib;
using ServicePlatform.Config;
using System.Data;
using ServicePlatform.App_Start;
using ServicePlatform.Areas.Permission.Models;

namespace ServicePlatform.Areas.Platform.Controllers
{
    public class PFEmployeeController : Controller
    {
        //
        // GET: /Platform/PFEmployee/
        EnterpriseContext ec = new EnterpriseContext();
        CodeTableContext ct = new CodeTableContext();
        SchoolContext sc = new SchoolContext();
        UserContext uc = new UserContext();
        PermissionContext pc = new PermissionContext();
        PlateformContext pfc = new PlateformContext();


        public ActionResult Index()
        {
            return View();
        }

        #region 平台员工管理
        [BaseActionFilter]
        public ActionResult PFEmployeeAdd()
        {
            ViewBag.actionLink = "/Platform/PFEmployee/PFEmployeeAdd";
            return View();
        }

        [HttpPost]
        [BaseActionFilter]
        public ActionResult PFEmployeeAdd(T_User NewUser)
        {
            Vars vars = (Session["Vars"] as Vars);
            string UserType = "C1";//默认设置为普通用户，仅能登陆维护密码和个人信息
            string PFNo = Request.Form["PFNo"];
            string PFName = Request.Form["PFName"];
            string Phone = Request.Form["Phone"];
            string Address = Request.Form["Address"];
            string Email = Request.Form["Email"];
            string Ent_No = vars.getUserUnit();

            

            string UserID = Ent_No + "-" + PFNo;//出现2次
            //string NickName = Request.Form["NickName"];
            string UserPwd = ServicePlatform.Lib.EncryptString.NoneEncrypt(Request.Form["UserPWD"]);//密文存储密码         




            NewUser.UserID = UserID;
            NewUser.NickName = PFName;//默认相同
            NewUser.UserPwd = UserPwd;
            NewUser.Note = DateTimeFormat.GetTime() + "平台管理员为用户注册了账号";

            T_Platformer NewPlatformer = new T_Platformer();
            //NewPlatformer.Ent_No = Ent_No;
            NewPlatformer.PFNo = PFNo;
            NewPlatformer.PFName = PFName;
            NewPlatformer.UserID = UserID;
            NewPlatformer.Phone = Phone;
            NewPlatformer.Address = Address;
            NewPlatformer.Email = Email;

            



            T_UserRole NewUserRole = new T_UserRole() { UserID = NewUser.UserID, RoleID = UserType };
            if (uc.T_User.Find(NewUser.UserID) == null)
            {
                uc.T_User.Add(NewUser);
                pfc.T_Platformer.Add(NewPlatformer);                
                pc.UserRole.Add(NewUserRole);
                uc.SaveChanges();
                pfc.SaveChanges();
                pc.SaveChanges();
                return Content(PageHelper.Tip("平台员工注册成功", "/Enterprise/Home/UserListShow"));
            }
            else
            {
                return Content(PageHelper.Tip("平台员工注册失败,已存在该员工号", -1));
            }

        }

         [BaseActionFilter]
        public ActionResult PFEmployeeListShow(int perCount = 8, int pageIndex = 1)
        {
            //关键词User
            Vars vars = (Session["Vars"] as Vars);
            //string Ent_No = vars.getUserUnit();
            //分页相关开始
            var data = (from a in pfc.T_Platformer
                        select a
                           ).ToList();//原始数据
            ViewBag.link = "/Platform/PFEmployee/PFEmployeeListShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_Platformer>(data, perCount, pageIndex);//分页操作
            //分页相关结束


            ViewBag.Title = "所有平台员工";
            ViewBag.addLink = "/Platform/PFEmployee/PFEmployeeAdd";
            ViewBag.Limit = 3;
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_Platformer_Note())[1];//
            ViewBag.Action = new string[][]
             {
              
                //new string[] {"/Enterprise/Home/User_Delete","删除"}
             };

            return View();
        }
        #endregion
    }
}
