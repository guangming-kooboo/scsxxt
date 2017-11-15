using ServicePlatform.App_Start;
using ServicePlatform.Areas.Permission.Models;
using ServicePlatform.Config;
using ServicePlatform.Lib;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using WebGrease.Css.Extensions;

namespace ServicePlatform.Areas.Permission.Controllers
{
    public class HomeController : BasePermission
    {
        //
        // GET: /Permission/Home/

     

        // GET: /Permission/Home/
        #region 框架
        [BaseActionFilter]public ActionResult Index()
        {
            return View();
        }
        [BaseActionFilter]public ActionResult Top()
        {
            return View();
        }
        [BaseActionFilter]public ActionResult Left()
        {
            return View();
        }
        [BaseActionFilter]public ActionResult Home()
        {
            return View();
        }
        #endregion


        #region 用户管理
        //代码转化
         [BaseActionFilter]
        private List<T_User_Note> Get_User_NoteList(List<T_User> dataList)
        {
            return (from a in dataList
                    select new T_User_Note()
                    {
                        UserID = a.UserID,

                        NickName = a.NickName,

                        UserPwd = a.UserPwd,

                        UserType = a.UserType.ToString(),

                        Note = a.Note
                    }).ToList();
        }
        //添加用户
      
        [BaseActionFilter]
        public ActionResult UserAdd()
        {
            ViewBag.actionLink = "/Permission/Home/UserAdd";
            return View();
        }

        /*
RoleID	RoleName	RoleType
3	企业管理员	2
31	人力资源	3
32	企业导师	3
33	企业普通员工	3
5	高校管理员	3
51	高校新闻管理员	5
52	高校导师	5
53	实习管理老师	5
61	当前批次学生	6
62	过往批次学生	6
7	平台管理员	7
71	平台普通员工	7
72	平台新闻管理员	7
C1	普通用户	默认
TTTT	测试角色	默认
         */
        


        [BaseActionFilter]
        public ActionResult UnitAdd()
        {
            ViewBag.actionLink = "/Permission/Home/UnitCreat";
            return View();
        }

        [HttpPost]
        [BaseActionFilter]
        public ActionResult UnitCreat()
        {


            if (Request.Form["UserPWD"] == Request.Form["UserPWD2"])
            {
                bool flag = false;
                
                string UserPWD = Request.Form["UserPWD"];
                string UnitCode = Request.Form["UnitCode"];
                string UserCode = Request.Form["UserCode"];
                string UserID = UnitCode + "-" + UserCode;
                string UserName = Request.Form["NickName"];

                int UserType = int.Parse(Request.Form["UnitType"]);

                string UnitName = Request.Form["UnitName"];
               

                //用户表
                if (db.T_User.Find(UserID) == null)
                {
                    UserPWD = ServicePlatform.Lib.EncryptString.NoneEncrypt(UserPWD);
                    //添加用户
                    db.T_User.Add(new T_User { UserID = UserID, NickName = UserName, UserPwd = UserPWD, UserType = UserType });                        
                    db.SaveChanges();
                    //这里要增加事务控制
                    switch (UserType)
                    {
                        case UserTypeConst.EnterpriseUser:
                            {
                                //企业表
                                db.T_Enterprise.Add(new T_Enterprise() { Ent_No = UnitCode, Ent_Name = UnitName, RegisterTime = DateTimeFormat.GetTime(), CountyID = DefaultValueConst.CountyID, EntState = UnitStatus.Approved, EntRank = DefaultValueConst.EntRank, EntCategoryID = DefaultValueConst.EntCategoryID, InfoLocked = EditStatus.UnLock }); //直接设置为审核通过，因为是管理添加的。当采用申请制度后，应设置为UnitStatus.UnComitted
                                db.SaveChanges();
                                //企业目录
                                new ServicePlatform.Areas.Platform.Controllers.HomeController().CreateEntDirectorys(UnitCode);
                                //员工表
                                db.T_Employee.Add(new T_Employee() { UserID = UserID, Ent_No = UnitCode, EmployeeID = UserCode, EmployeeName = UserName });
                                db.SaveChanges();
                                //角色表
                                db.UserRole.Add(new T_UserRole() { UserID = UserID, RoleID = UserRoleConst.EnterpriseAdmin });
                                db.SaveChanges();    
                                
                            } break;

                        case UserTypeConst.SchoolUser:
                            {
                                //高校表
                                db.T_School.Add(new T_School() { SchoolName = UnitName, SchoolID = UnitCode, Status = UnitStatus.Approved });//直接设置为审核通过，因为是管理添加的。当采用申请制度后，应设置为UnitStatus.UnComitted
                                db.SaveChanges();
                                //学校目录
                                new ServicePlatform.Areas.School.Controllers.HomeController().CreateSchoolDirectorys(UnitCode);
                                //教师表                                
                                db.T_Faculty.Add(new T_Faculty() { UserID = UserID, FacNo = UserCode, FacName = UserName, SchoolID = UnitCode, FacStatus = DefaultValueConst.FacStatus, FacProPos = DefaultValueConst.FacProPos });
                                db.SaveChanges();//最后保存！
                                //角色表
                                db.UserRole.Add(new T_UserRole() { UserID = UserID, RoleID = UserRoleConst.SchoolAdmin });
                                db.SaveChanges(); 
                                
                            }break;
                    }
                    flag = true;
                    
                }
                else 
                {
                    flag = false;
                    //return Content(PageHelper.Tip("添加失败：已存在该用户", -1));
                }
                if (flag)
                { return Content(PageHelper.Tip("用户添加成功", "/Platform/Home/T_Enterprise_ListShow")); }
                else
                {
                    return Content(PageHelper.Tip("添加失败：已存在该用户", -1));
                }
            }

            else
            {
                return Content(PageHelper.Tip("2次密码不一致，请检查密码", -1));
            }

            //------------------
            //return View();
        }


        [HttpPost]
        [BaseActionFilter]
        public ActionResult UserAdd(T_User f)
        {
             if(Request.Form["UserPWD"]==Request.Form["UserPWD2"])
             {
                 bool flag = false;
                 string UserType = Request.Form["UserType"];
                 string UnitCode = Request.Form["UnitCode"];
                 string _UserID = UnitCode + "-" + f.UserID;
                 string _UseName = f.NickName;
                 f.UserID = _UserID;
                 //用户表
                 //用户角色表
                 if (db.T_User.Find(_UserID) == null)
                 {
                     f.UserPwd = ServicePlatform.Lib.EncryptString.NoneEncrypt(f.UserPwd);
                     f.UserType = int.Parse(UserType);//设置用户类型，加速查找
                     db.T_User.Add(f); db.SaveChanges();
                     db.UserRole.Add(new T_UserRole() { UserID = _UserID, RoleID = UserType });// db.SaveChanges();
                     switch (UserType)
                     {
                         case "3":
                             {
                                 //企业表
                                 //员工表
                                 db.T_Enterprise.Add(new T_Enterprise() { Ent_No = UnitCode, Ent_Name = _UseName, RegisterTime = DateTimeFormat.GetTime(), CountyID = "-1", EntState = 1, EntRank = "-1", EntCategoryID = "-1", InfoLocked = -1 }); db.SaveChanges();
                                 db.T_Employee.Add(new T_Employee() { UserID = _UserID, Ent_No = UnitCode, EmployeeID = UnitCode, EmployeeName = _UseName });
                                 db.SaveChanges();//最后保存！
                                 //企业目录
                                 new ServicePlatform.Areas.Platform.Controllers.HomeController().CreateEntDirectorys(UnitCode);
                             } break;
                         case "5":
                             {
                                 //学校表   
                                 //教师表
                                 db.T_School.Add(new T_School() { SchoolName = _UseName, SchoolID = UnitCode, Status = 1 }); ; db.SaveChanges();
                                 db.T_Faculty.Add(new T_Faculty() { UserID = _UserID, FacNo = UnitCode, FacName = _UseName, SchoolID = UnitCode, FacStatus = 0, FacProPos = 0 });
                                 db.SaveChanges();//最后保存！
                                 //学校目录
                                 new ServicePlatform.Areas.School.Controllers.HomeController().CreateSchoolDirectorys(UnitCode);
                             } break;
                     }
                     flag = true;
                 }
                 else
                 {
                     flag = false;
                 }
                 if (flag)
                 { return Content(PageHelper.Tip("用户添加成功", "/Platform/Home/T_Enterprise_ListShow")); }
                 else
                 {
                     return Content(PageHelper.Tip("添加失败：已存在该用户", -1));
                 }
             }
            else
             {
                 return Content(PageHelper.Tip("2次密码不一致，请检查密码", -1));
             }
           
        }


        [BaseActionFilter]
        public bool AddOneUser(string UserID, string UserNickName, int UserType, string UserPWD)
        {
            T_User newUser = new T_User();
            newUser.UserID = UserID;
            newUser.UserPwd = UserPWD;
            newUser.UserType = UserType;
            newUser.NickName = UserNickName;

            int result = -1;
            try {
                result = db.SaveChanges();
            }
            catch(Exception e)
            {

            }
            if (result == 1)
            {
                return true;
            }
            else 
            {
                return false;
            }

        }//ending of AddOneUser

        //删除用户
        [BaseActionFilter]
        public ActionResult UserDelete(string id)
        {
            var old = uc.T_User.Find(id);
            if (old != null)
            {
                uc.Entry(old).State = EntityState.Deleted;
                uc.SaveChanges();
                return Content(PageHelper.Tip("删除成功", "/Permission/Home/UserShow"));
            }
            else
            {
                return Content(PageHelper.Tip("删除失败", -1));
            }
           
        }
        //所有用户
        [BaseActionFilter]
        public ActionResult UserShow(string keyword="",int perCount = 50, int pageIndex = 1)
        {
           ViewBag.forbidenButton= ((Vars) Session["Vars"]).getButtons();
            //关键词User
            Vars vars = (Session["Vars"] as Vars);

            List<T_User> data = new List<T_User>();
            //只有平台才能添加用户
            string addLink = "";
            //过滤用户列表
            if(vars.IsEntAdmin)
            {
                data = (from a in db.T_User
                        join b in db.T_Employee on a.UserID equals b.UserID
                        where b.Ent_No == vars.UserUnit &&
                           a.UserID.Contains(keyword)
                        select a).ToList();
            }
            else if(vars.IsSchoolAdmin)
            {
                data = (from a in db.T_User
                        join b in db.T_Faculty on a.UserID equals b.UserID
                        where b.SchoolID == vars.UserUnit &&
                           a.UserID.Contains(keyword)
                        select a).ToList();
            }
            else
            {
                addLink = "/Permission/Home/UserShow";
                data = db.T_User.Where(m => m.UserID.Contains(keyword)).ToList();
            }
           
            //分页相关开始
           

            var dataNote = Get_User_NoteList(data);                                                                     //处理显示数据【add】
            ViewBag.link = addLink;
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_User>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_User_Note>(dataNote, perCount, pageIndex);           //显示数据分页【add】
            //分页相关结束


            ViewBag.Title = "所有用户";
            ViewBag.addLink = "/Permission/Home/UserAdd";
            ViewBag.Limit = new int[] { 0, 1};                                                                 //配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_User_Note())[1];
            ViewBag.Action = new string[][]
             {
              new string[] {"/Permission/Home/UserRoleShow","分配角色"},
                new string[] {"/Permission/Home/UserDelete","删除"}
             };

            return View();
        }
        #endregion

        #region 角色管理
        //代码转换
        public List<T_Role_Note> Get_RoleNoteList(List<T_Role> dataList)
        {
            return (from a in dataList
                    select new T_Role_Note()
                    {
                        RoleID = a.RoleID,
                        RoleName = a.RoleName,
                        RoleType = a.RoleType
                    }
                        ).ToList();
        }
        //添加角色
        [BaseActionFilter]public ActionResult RoleAdd()
        {
            ViewBag.actionLink = "/Permission/Home/RoleAdd";
            return View();
        }
         [HttpPost]
        [BaseActionFilter]public ActionResult RoleAdd(T_Role f)
        {
            f.RoleType = "默认";
            if (db.Role.Find(f.RoleID) == null)
            {
                db.Role.Add(f);
                db.SaveChanges();
                return Content(PageHelper.Tip("角色添加成功", "/Permission/Home/RoleShow"));
            }
            else
            {
                return Content(PageHelper.Tip("添加失败：已存在该角色", -1));
            }
        }
         //删除
         [BaseActionFilter]
         public ActionResult RoleDelete(string RoleID)
         {
             var old = db.Role.Find(RoleID);
             if (old != null)
             {
                 db.Entry(old).State = EntityState.Deleted;
                 db.SaveChanges();
                 return Content(PageHelper.Tip("删除成功", "/Permission/Home/RoleShow"));
             }
             else
             {
                 return Content(PageHelper.Tip("删除失败", -1));
             }

         }
         //所有角色
         [BaseActionFilter]
         public ActionResult RoleShow(int perCount = 8, int pageIndex = 1)
         {
             //关键词Role
             Vars vars = (Session["Vars"] as Vars);
             //分页相关开始
             var data = db.Role.ToList();//原始数据
             var dataNote = Get_RoleNoteList(data);                                                                     //处理显示数据【add】
             ViewBag.link = "/Permission/Home/RoleShow";
             ViewBag.total = data.Count;
             ViewBag.currentPage = pageIndex;
             ViewBag.perCount = perCount;

             ViewBag.dataList = LibHelper.PageHelper.GetPage<T_Role>(data, perCount, pageIndex);//分页操作
             ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_Role_Note>(dataNote, perCount, pageIndex);           //显示数据分页【add】
             //分页相关结束


             ViewBag.Title = "所有角色";
             ViewBag.addLink = "/Permission/Home/RoleAdd";
             ViewBag.Limit = new int[] { 0, 1 };                                                                 //配置列【modified】
             ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_Role_Note())[1];
             ViewBag.Action = new string[][]
             {
              new string[] {"/Permission/Home/RoleModuleShow","分配菜单"},
              new string[] {"/Permission/Home/RoleFuncObjForbidShow","禁用按钮"},
                new string[] {"/Permission/Home/RoleDelete","删除"}
             };

             return View("ListShow");
         }
        #endregion

        #region 菜单管理
        //代码转化
         [BaseActionFilter]
         private List<T_Module_Note> Get_ModuleNoteList(List<T_Module> dataList)
         {
             return (from a in dataList
                     join b in db.Module on a.FartherModuleID equals b.ModuleID
                     select new T_Module_Note()
                     {
                         ModuleID = a.ModuleID,
                         ModuleName = a.ModuleName,
                         FartherModuleID = b.ModuleName,
                         PageName = a.PageName,
                         ModuleLevel = a.ModuleLevel,
                         AvailableStatus = a.AvailableStatus == 1 ? "是" : "否",
                         ModuleOrder =a.ModuleOrder.ToString()
                     }).ToList();
         }
         //添加菜单
         [BaseActionFilter]
         public ActionResult ModuleAdd(string FartherModuleID = "-1")
        {
            ViewBag.Title = "添加菜单";
            ViewBag.FartherModuleID = FartherModuleID;
            ViewBag.actionLink = "/Permission/Home/ModuleAdd";
            return View();
          
        }
          [HttpPost]
        [BaseActionFilter]
        public ActionResult ModuleAdd(T_Module f)
        {
            if (db.Module.Find(f.ModuleID) == null)
            {
                db.Module.Add(f);
                db.SaveChanges();
                return Content(PageHelper.Tip("菜单添加成功", "/Permission/Home/ModuleShow?ModuleID=" + f.FartherModuleID));
            }
            else
            {
                return Content(PageHelper.Tip("添加失败：已存在该菜单", -1));
            }
        }
        //编辑
          [BaseActionFilter]
          public ActionResult ModuleEdict(string ModuleID)
          {
              ViewBag.Title = "编辑菜单";
              ViewBag.actionLink = "/Permission/Home/ModuleEdict";
              ViewBag.data = db.Module.Find(ModuleID);
              return View();
          }
          [HttpPost]
          [BaseActionFilter]
          public ActionResult ModuleEdict(T_Module f)
          {
              db.Entry(f).State = EntityState.Modified;
              db.SaveChanges();
              return Content(PageHelper.Tip("保存成功", "/Permission/Home/ModuleShow?ModuleID="+f.FartherModuleID));
          }
          //删除
          [BaseActionFilter]
          public ActionResult ModuleDelete(string ModuleID)
          {
              var old = db.Module.Find(ModuleID);
              if (old != null)
              {
                  db.Entry(old).State = EntityState.Deleted;
                  db.SaveChanges();
                  return Content(PageHelper.Tip("删除成功", "/Permission/Home/ModuleShow"));
              }
              else
              {
                  return Content(PageHelper.Tip("删除失败", -1));
              }

          }
          //所有菜单
          [BaseActionFilter]
          public ActionResult ModuleShow(string ModuleID = "MRoot", int perCount = 20, int pageIndex = 1)
          {//List版本日期2015/12/25   
              //关键词Module
              Vars vars = (Session["Vars"] as Vars);


              //分页相关开始
              var data = db.Module.Where(a => a.FartherModuleID == ModuleID).OrderBy(b=>b.ModuleOrder).ToList();//原始数据
             // data = data.OrderBy(m => m.ModuleLevel).OrderBy(m => m.ModuleOrder).ToList();
              var dataNote = Get_ModuleNoteList(data);                                                                     //处理显示数据【add】
             
              #region 错误检测 
              //for (int i = 0; i < data.Count; i++)
              //{
              //    for (int j = 0; j < dataNote.Count; j++)
              //    {
              //        if (data[i].ModuleID == dataNote[j].ModuleID)
              //        {
              //            data.Remove(data[i]);
              //        }
              //    }

              //}
              #endregion

              ViewBag.link = "/Permission/Home/ModuleShow";
              ViewBag.total = data.Count;
              ViewBag.currentPage = pageIndex;
              ViewBag.perCount = perCount;

              ViewBag.dataList = LibHelper.PageHelper.GetPage<T_Module>(data, perCount, pageIndex);//分页操作
              ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_Module_Note>(dataNote, perCount, pageIndex);           //显示数据分页【add】
              //分页相关结束



              ViewBag.Title = "父亲为 [" + db.Module.Find(ModuleID).ModuleName + "] 的所有菜单";
              ViewBag.addLink = "/Permission/Home/ModuleAdd?FartherModuleID=" + ModuleID;
              ViewBag.Limit = new int[] { 0,1,2,3,4,5,6 };                                                                 //配置列【modified】
              ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_Module_Note())[1];
              ViewBag.Action = new string[][]
             {
                new string[] {"/Permission/Home/RoleFuncObjForbidShow","分配按钮"},
                new string[] {"/Permission/Home/ModuleShow","查看孩子"},
                new string[] {"/Permission/Home/ModuleEdict","编辑"},
                new string[] {"/Permission/TimeSwitch/Index","定时控制"},
                new string[] {"/Permission/Home/ModuleDelete","删除"}
             };

              return View("ListShow");
          }
         #endregion

        #region 按钮管理
          //代码转化
          [BaseActionFilter]
          private List<T_FuncObject_Note> Get_NoteList(List<T_FuncObject> dataList)
          {
              return (from a in dataList
                      join b in db.Module on a.ModuleID equals b.ModuleID
                     
                      select new T_FuncObject_Note()
                      {
                          FuncObjID=a.FuncObjID,
                          ModuleID = b.ModuleName +"["+ b.ModuleID+"]"
                 
                      }).ToList();
          }
          //添加按钮
        [BaseActionFilter]public ActionResult FuncObjectAdd()
        {
            ViewBag.actionLink = "/Permission/Home/FuncObjectAdd";
            return View();
        }
          [HttpPost]
        [BaseActionFilter]public ActionResult FuncObjectAdd(T_FuncObject f)
        {
            if (db.FuncObject.Find(f.ModuleID,f.FuncObjID) == null)
            {
                db.FuncObject.Add(f);
                db.SaveChanges();
                return Content(PageHelper.Tip("按纽添加成功", "/Permission/Home/FuncObjectShow"));
            }
            else
            {
                return Content(PageHelper.Tip("添加失败：已存在该按纽", -1));
            }
        }
          //删除
          [BaseActionFilter]
          public ActionResult FuncObjectDelete(string FuncObjID, string ModuleID)
          {
              var old = db.FuncObject.Find(FuncObjID, ModuleID);
              if (old != null)
              {
                  db.Entry(old).State = EntityState.Deleted;
                  db.SaveChanges();
                  return Content(PageHelper.Tip("删除成功", "/Permission/Home/FuncObjectShow"));
              }
              else
              {
                  return Content(PageHelper.Tip("删除失败", -1));
              }

          }
          //所有按钮
          [BaseActionFilter]
          public ActionResult FuncObjectShow(int perCount = 8, int pageIndex = 1)
          {
              //关键词FuncObject
              Vars vars = (Session["Vars"] as Vars);
              //分页相关开始
              var data = db.FuncObject.ToList();//原始数据
              var dataNote = Get_NoteList(data);                                                                     //处理显示数据【add】
              ViewBag.link = "/Permission/Home/FuncObjectShow";
              ViewBag.total = data.Count;
              ViewBag.currentPage = pageIndex;
              ViewBag.perCount = perCount;

              ViewBag.dataList = LibHelper.PageHelper.GetPage<T_FuncObject>(data, perCount, pageIndex);//分页操作
              ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_FuncObject_Note>(dataNote, perCount, pageIndex);           //显示数据分页【add】
              //分页相关结束


              ViewBag.Title = "所有按钮";
              ViewBag.addLink = "/Permission/Home/FuncObjectAdd";
              ViewBag.Limit = new int[] { 0, 1 };                                                                 //配置列【modified】
              ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_FuncObject_Note())[1];
              ViewBag.Action = new string[][]
             {
               new string[] {"/Permission/FunobjTimeSwitch/Index","定时控制"},
                new string[] {"/Permission/Home/FuncObjectDelete","删除"}
             };

              return View("ListShow");
          }
          #endregion

        #region 用户角色管理
          //代码转化
          [BaseActionFilter]
          private List<T_UserRole_Note> Get_UserRole_NoteList(List<T_UserRole> dataList)
          {
              return (from a in dataList
                      join b in db.Role on a.RoleID equals b.RoleID
                      join c in db.T_User on a.UserID equals c.UserID
                      select new T_UserRole_Note()
                      {
                          UserID = c.UserID,
                          RoleID = b.RoleName,
                      }).ToList();
          }
          //添加角色组
        [BaseActionFilter]public ActionResult UserRoleAdd(string UserID)
        {
            ViewBag.actionLink = "/Permission/Home/UserRoleAdd";
            ViewBag.UserID = UserID;
            var UserData = (from u in db.T_User
                            where u.UserID == UserID
                            select u.NickName
                                ).ToList();
            ViewBag.UserName = UserData[0];
            string subSystem = (Session["Vars"] as Vars).getSubSystem();//这个以后要通过参数传递，当前是江湖救急。
            var RoleData = (from r in db.Role
                            where r.subSystem == subSystem
                            select r
                                ).ToList();

            ViewBag.RoleData = RoleData;


            var tip = "";
            var service = S<Core.Services.Entity.T_RoleModule>();

            foreach (var role in RoleData)
            {
                DataContext.SetFiled("RoleID", role.RoleID);
                tip += "<b>"+role.RoleName+":</b>";
                service.All(DataContext, "该角色具有的所有菜单").
                Select(a => a.T_Module).
                ForEach(a =>
                {
                    tip += a == null ? "": a.ModuleName+"、";
                });
                tip += "<br/>";
            }

            ViewBag.tip = tip;

            return View();
        }
          [HttpPost]
        [BaseActionFilter]public ActionResult UserRoleAdd(T_UserRole f)
        {
            if (db.UserRole.Find(f.UserID, f.RoleID) == null)
            {
                db.UserRole.Add(f);
                db.SaveChanges();
                return Content(PageHelper.Tip("添加成功", "/Permission/Home/UserRoleShow?UserID="+f.UserID));
            }
            else
            {
                return Content(PageHelper.Tip("添加失败：已存在该记录", -1));
            }
        }
          //删除
          [BaseActionFilter]
          public ActionResult UserRoleDelete(string UserID, string RoleID)
          {
              var old = db.UserRole.Find(UserID, RoleID);
              if (old != null)
              {
                  db.Entry(old).State = EntityState.Deleted;
                  db.SaveChanges();
                  return Content(PageHelper.Tip("删除成功", "/Permission/Home/UserRoleShow?UserID=" + old.UserID));
              }
              else
              {
                  return Content(PageHelper.Tip("删除失败", -1));
              }

          }
          //所有用户角色
        [BaseActionFilter]
          public ActionResult UserRoleShow(string UserID, int perCount = 8, int pageIndex = 1)
          {
              //关键词UserRole
              Vars vars = (Session["Vars"] as Vars);
              //分页相关开始
              var data = db.UserRole.Where(a => a.UserID == UserID).ToList();//原始数据
              var dataNote = Get_UserRole_NoteList(data);                                                                     //处理显示数据【add】
              ViewBag.link ="/Permission/Home/UserRoleShow?UserID=" + UserID;
              ViewBag.total = data.Count;
              ViewBag.currentPage = pageIndex;
              ViewBag.perCount = perCount;

              ViewBag.dataList = LibHelper.PageHelper.GetPage<T_UserRole>(data, perCount, pageIndex);//分页操作
              ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_UserRole_Note>(dataNote, perCount, pageIndex);           //显示数据分页【add】
            //分页相关结束


              ViewBag.Title = "所有用户角色";
              ViewBag.addLink = "/Permission/Home/UserRoleAdd?UserID=" + UserID;
              ViewBag.Limit = new int[] { 0, 1};                                                                 //配置列【modified】
              ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_UserRole_Note())[1];
              ViewBag.Action = new string[][]
             {
               
                new string[] {"/Permission/Home/UserRoleDelete","删除"}
             };

              return View("ListShow");
          }
          #endregion

        #region 角色菜单管理
        //代码转化
        [BaseActionFilter]
        private List<T_RoleModule_Note> Get_NoteList(List<T_RoleModule> dataList)
        {
            return (from a in dataList
                    join b in db.Module on a.ModuleID equals b.ModuleID
                    join c in db.Role on a.RoleID equals c.RoleID
                    select new T_RoleModule_Note()
                    {
                        RoleID = c.RoleName + "  [" + a.RoleID + "]",
                        ModuleID = b.ModuleName + "[" + b.ModuleID + "]",
                        IncludeAllSubMod = a.IncludeAllSubMod==1?"是":"否",
                        DataDomain = a.DataDomain,
                       
                    }).ToList();
        }
          //赋予菜单权限
        [BaseActionFilter]
        public ActionResult RoleModuleAdd(string RoleID)
        {
            ViewBag.RoleID = RoleID;
            ViewBag.actionLink = "/Permission/Home/RoleModuleAdd";
            return View();
        }
          [HttpPost]
        [BaseActionFilter]public ActionResult RoleModuleAdd(T_RoleModule f)
        {
            if (db.RoleModule.Find(f.RoleID, f.ModuleID) == null)
            {
                db.RoleModule.Add(f);
                db.SaveChanges();
                return Content(PageHelper.Tip("添加成功", "/Permission/Home/RoleModuleShow?RoleID="+f.RoleID));
            }
            else
            {
                return Content(PageHelper.Tip("添加失败：已存在该记录", -1));
            }
        }
          //删除
          [BaseActionFilter]
          public ActionResult RoleModuleDelete(string RoleID, string ModuleID)
          {
              var old = db.RoleModule.Find(RoleID, ModuleID);
              if (old != null)
              {
                  db.Entry(old).State = EntityState.Deleted;
                  db.SaveChanges();
                  return Content(PageHelper.Tip("删除成功", "/Permission/Home/RoleModuleShow?RoleID=" + RoleID));
              }
              else
              {
                  return Content(PageHelper.Tip("删除失败", -1));
              }

          }
          //所有菜单权限
          [BaseActionFilter]
          public ActionResult RoleModuleShow(string RoleID, int perCount = 8, int pageIndex = 1)
          {
              //关键词RoleModule
              Vars vars = (Session["Vars"] as Vars);
              //分页相关开始
              var data = db.RoleModule.Where(a => a.RoleID == RoleID).ToList();//原始数据
              var dataNote = Get_NoteList(data);                                                    //处理显示数据【add】
              ViewBag.link = "/Permission/Home/RoleModuleShow?RoleID=" + RoleID;
              ViewBag.total = data.Count;
              ViewBag.currentPage = pageIndex;
              ViewBag.perCount = perCount;

              ViewBag.dataList = LibHelper.PageHelper.GetPage<T_RoleModule>(data, perCount, pageIndex);//分页操作
              ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_RoleModule_Note>(dataNote, perCount, pageIndex);           //显示数据分页【add】
              //分页相关结束


              ViewBag.Title = "[" + db.Role.Find( RoleID).RoleName + "] 的权限列表";
              ViewBag.addLink = "/Permission/Home/RoleModuleAdd?RoleID=" + RoleID;
                ViewBag.Limit = new int[] { 1,2,3 };                                                                 //配置列【modified】
              ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_RoleModule_Note())[1];
              ViewBag.Action = new string[][]
             {
              
                new string[] {"/Permission/Home/RoleModuleDelete","删除"}
             };
              
              return View("ListShow");
          }
          #endregion

        #region 菜单按钮管理
          //代码转化
          [BaseActionFilter]
          private List<T_RoleFuncObjForbid_Note> Get_NoteList(List<T_RoleFuncObjForbid> dataList)
          {
              return (from a in dataList
                      join b in db.FuncObject on a.FuncObjID equals b.FuncObjID
                      join c in db.Role on a.RoleID equals c.RoleID
                      select new T_RoleFuncObjForbid_Note()
                      {
                          RoleID = c.RoleID,
                          FuncObjID = b.FuncObjID,
                      }).ToList();
          }
          //赋予按钮权限
        [BaseActionFilter]public ActionResult RoleFuncObjForbidAdd()
        {
            ViewBag.actionLink = "/Permission/Home/RoleFuncObjForbidAdd";
            return View();
        }
        [HttpPost]
        [BaseActionFilter]public ActionResult RoleFuncObjForbidAdd(T_RoleFuncObjForbid f)
        {
            if (db.RoleFuncObjForbid.Find(f.RoleID, f.FuncObjID) == null)
            {
                db.RoleFuncObjForbid.Add(f);
                db.SaveChanges();
                return Content(PageHelper.Tip("添加成功", "/Permission/Home/RoleFuncObjForbidShow"));
            }
            else
            {
                return Content(PageHelper.Tip("添加失败：已存在该记录", -1));
            }
        }
        //删除
        [BaseActionFilter]
        public ActionResult RoleFuncObjForbidDelete(string RoleID, string FuncObjID)
        {
            var old = db.RoleFuncObjForbid.Find(RoleID, FuncObjID);
            if (old != null)
            {
                db.Entry(old).State = EntityState.Deleted;
                db.SaveChanges();
                return Content(PageHelper.Tip("删除成功", "/Permission/Home/RoleFuncObjForbidShow"));
            }
            else
            {
                return Content(PageHelper.Tip("删除失败", -1));
            }

        }
        //所有按钮权限
        [BaseActionFilter]
        public ActionResult RoleFuncObjForbidShow(string RoleID,int perCount = 8, int pageIndex = 1)
        {
            //关键词RoleFuncObjForbid
            Vars vars = (Session["Vars"] as Vars);
            //分页相关开始
            var data = db.RoleFuncObjForbid.Where(a => a.RoleID == RoleID).ToList();//原始数据
            var dataNote = Get_NoteList(data);                                                                     //处理显示数据【add】
            ViewBag.link = "/Permission/Home/RoleFuncObjForbidShow";
            ViewBag.total = data.Count;
            ViewBag.currentPage = pageIndex;
            ViewBag.perCount = perCount;

            ViewBag.dataList = LibHelper.PageHelper.GetPage<T_RoleFuncObjForbid>(data, perCount, pageIndex);//分页操作
            ViewBag.dataNoteList = LibHelper.PageHelper.GetPage<T_RoleFuncObjForbid_Note>(dataNote, perCount, pageIndex);           //显示数据分页【add】
            //分页相关结束


            ViewBag.Title = "所有按钮权限";
            ViewBag.addLink = "/Permission/Home/RoleFuncObjForbidAdd";
            ViewBag.Limit = new int[] { 0, 1};                                                                 //配置列【modified】
            ViewBag.headerTitle = Permission.Lib.PermissionHelper.GetInfo(new T_RoleFuncObjForbid_Note())[1];
            ViewBag.Action = new string[][]
             {
              
                new string[] {"/Permission/Home/RoleFuncObjForbidDelete","删除"}
             };

            return View("ListShow");
        }
          #endregion

        #region 权限判断
        public List<T_Module>  GetModuleByUserID(string UserID)
        {
             var resultList=new List<T_Module>();
            //所有菜单
             var allModules = db.Module.ToList();


            //取出 拥有的本级菜单 关系
            var tempList = (from a in db.UserRole
                                         join b in db.Role on a.RoleID equals b.RoleID
                                         join c in db.RoleModule on b.RoleID equals c.RoleID
                                         join d in db.Module on c.ModuleID equals d.ModuleID
                                         where a.UserID == UserID
                                         select c
                 );
            //取出 隶属于本级菜单的子菜单 
            foreach (var t1 in tempList)
            {
                string tempModuleID = t1.ModuleID;
                //将本级菜单加入结果集
                resultList.Add((from t in allModules where t.ModuleID == tempModuleID select t).FirstOrDefault());
                //如果包含子菜单
                if(t1.IncludeAllSubMod==1)
                {
                    //将孩子加入结果集
                    resultList.AddRange(GetChildModuleByFatherID(tempModuleID, allModules, 0));
                }
            }
            //  return resultList.OrderBy(a=>a.ModuleOrder).ToList();
                return resultList.Distinct<T_Module>(
                    Qx.Tools.CommonExtendMethods.CommonExtendMethods.Equality<T_Module>
                    .CreateComparer(x=>x.ModuleID)
                    ).Where(a=>a.AvailableStatus==1).OrderBy(a=>a.ModuleOrder).ToList();
        }

        public List<T_FuncObject> GetForbidenButtonByUserID(string UserID)
        {
          
            //取出禁用按钮列表
            return (from a in db.UserRole
                                           join b in db.Role on a.RoleID equals b.RoleID
                                           join c in db.RoleFuncObjForbid on b.RoleID equals c.RoleID
                                           join d in db.FuncObject on c.FuncObjID equals d.FuncObjID
                                           where a.UserID == UserID
                                           select d
                 ).ToList();
         

        }
        //递归找孩子
        private List<T_Module> GetChildModuleByFatherID(string father, List<T_Module> Modules, int deep)
        {
            List<T_Module> ChildMenu = new List<T_Module>();

            List<T_Module> temp = (from t in Modules where t.FartherModuleID == father select t).ToList();
            if (temp.Count > 0)
            {


                foreach (var t in temp)
                {
                    Modules.Remove(t);//删除节点 提升效率
     
                    ChildMenu.Add(t);
                    int tempDeep = deep + 1;
                    ChildMenu.AddRange(GetChildMenu(t.ModuleID, Modules, tempDeep));
                }
            }
            deep--;
            return ChildMenu;
        }
        #endregion


        private List<T_Module> GetChildMenu(string father, List<T_Module> Modules, int deep)
        {
            List<T_Module> ChildMenu = new List<T_Module>();

            List<T_Module> temp = (from t in Modules where t.FartherModuleID == father select t).ToList();
            if (temp.Count > 0)
            {


                foreach (var t in temp)
                {
                    Modules.Remove(t);//删除节点 提升效率
                    
                    //优化显示
                    for (int j = 0; j < deep; j++)
                    {
                        t.ModuleName = t.ModuleName.Insert(0, "*");
                    }
                    t.ModuleName += "[" + t.ModuleOrder + "]";
                    if (t.AvailableStatus == 0)
                    {
                        t.ModuleName += "[不可用]";
                    }


                    ChildMenu.Add(t);
                    int tempDeep = deep + 1;
                    ChildMenu.AddRange(GetChildMenu(t.ModuleID, Modules, tempDeep));
                }
            }
            deep--;
            return ChildMenu;
        }
        #region 相关列表项
        public List<SelectListItem> GetMenu(string selectedModuleID = "-1", string rootFather = "MRoot")
        {

            List<T_Module> ChildMenu = GetChildMenu(rootFather, db.Module.ToList(), 1);
            List<SelectListItem> Menus = new List<SelectListItem>();
            //若没传入选中节点，设置默认选项
            if (selectedModuleID=="-1")
                Menus.Add(new SelectListItem() { Text = "顶级（默认）", Value = "MRoot", Selected = true });

            for (int i = 0; i < ChildMenu.Count; i++)
            {
                if (ChildMenu[i].ModuleID == selectedModuleID)
                {
                    Menus.Add(new SelectListItem() { Text = ChildMenu[i].ModuleName, Value = ChildMenu[i].ModuleID, Selected = true });
                }
                else
                {
                    Menus.Add(new SelectListItem() { Text = ChildMenu[i].ModuleName, Value = ChildMenu[i].ModuleID });
                }
                
            }
           
            return Menus;
        }
        public List<SelectListItem> GetRole()
        {
            List<T_Role> Role = db.Role.ToList();
            List<SelectListItem> Roles = new List<SelectListItem>();
            for (int i = 0; i < Role.Count; i++)
            {

                Roles.Add(new SelectListItem() { Text = Role[i].RoleName, Value = Role[i].RoleID });

            }
            return Roles;
        }
        public List<SelectListItem> GetUser()
        {
            List<T_User> User = uc.T_User.ToList();
            List<SelectListItem> Users = new List<SelectListItem>();
            for (int i = 0; i < User.Count; i++)
            {

                Users.Add(new SelectListItem() { Text = User[i].NickName, Value = User[i].UserID });

            }
            return Users;
        }
        public List<SelectListItem> GetButton()
        {
            List<T_FuncObject> FuncObject = db.FuncObject.ToList();
            List<SelectListItem> FuncObjects = new List<SelectListItem>();
            for (int i = 0; i < FuncObject.Count; i++)
            {

                FuncObjects.Add(new SelectListItem() { Text = FuncObject[i].FuncName, Value = FuncObject[i].FuncObjID });

            }
            return FuncObjects;
        }
        [BaseActionFilter]public ActionResult Tool(string objName = "T_Role", string nameSpace = "ServicePlatform.Areas.Permission.Models")
        {
            //ViewData["Properties"] = PermissionHelper.GetProperties(objName, nameSpace);
            //ViewBag.Title = "添加记录";
            //ViewBag.objName = (nameSpace + "." + objName); 
            //return View("Tool");
            
            string sql = "select * from " + objName;
            ViewData["list"] = db.Database.SqlQuery<object>(sql);
            
            ViewData["Properties"] =LibHelper.ReflectionHelper.GetProperties(objName, nameSpace);
           
            //ViewData["Properties"] =  LibHelper.ReflectionHelper.GetProperties(objName, nameSpace);
            ViewBag.Title = "查看记录";
            ViewBag.objName = (nameSpace + "." + objName);
            return View("ToolShow");
        }
        [HttpPost]
        [BaseActionFilter]public ActionResult Tool()
        {
            string objName = Request.Form["objName"].ToString();
          
            string[] colNames = LibHelper.ReflectionHelper.GetProperties(objName);
            object o = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(objName);
            for (int i = 0; i < colNames.Length; i++)
            {
                var tt = o.GetType().GetProperty(colNames[i]);
                if (tt.PropertyType.Name == "String")
                    tt.SetValue(o, Request.Form[colNames[i]]);
                else
                    tt.SetValue(o, int.Parse(Request.Form[colNames[i]]));

            }
            ViewBag.Values = LibHelper.ReflectionHelper.GetObjInfo(o);

            ViewData["Properties"] = LibHelper.ReflectionHelper.GetProperties(objName);
            ViewBag.Title = "查看记录";
            return View("Tool");
        }
#endregion
    }
}
