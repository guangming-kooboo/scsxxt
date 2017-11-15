using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;
using Qx.Permission.Interfaces;
using ServicePlatform.Areas.Permision2.ViewModels.Admin;
using Qx.Permission.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace ServicePlatform.Areas.Permision2.Controllers
{
    public class AdminController : BasePermissionController
    {
        private IPermission _permission;
        private IRepository<Role> _role;
        private IRepository<User> _user;
        public AdminController(IPermission permission, IRepository<Role> role,IRepository<User> user)
        {
            _permission = permission;
            _role = role;
            _user = user;
        }
        //
        // GET: /Permision2/Admin/

        public ActionResult Index()
        {
            InitMenu(new Dictionary<string, string>() { 
                {"角色列表","/Permision2/Admin/RoleList?ReportID=Permision_角色列表&Params=;"},
                {"用户列表","/Permision2/Admin/UserList?ReportID=Permision_用户列表&Params=;"},
                {"按钮列表","/Permision2/Admin/ButtonList?ReportID=Permision_按钮列表&Params=;"},
                {"菜单列表","/Permision2/Admin/MenuList?ReportID=Permision_菜单列表&Params=MRoot"}
            });
            return View();
        }
        ///Permision2/Admin/RoleList?ReportID=Permision_角色列表&Params=;
        public ActionResult RoleList(string ReportID, string Params, int pageIndex = 1, int perCount = 10)
        {
            InitReport("角色列表", "/Permision2/CRUD/RoleAdd", pageIndex, perCount);
            return View(RoleList_M.ToViewModel(Params));
        }
        public ActionResult UserList(string ReportID, string Params, int pageIndex = 1, int perCount = 10)
        {

            InitReport("用户列表", "/Permision2/CRUD/UserAdd", pageIndex, perCount);
            return View(UserList_M.ToViewModel(Params));
        }
        public ActionResult ButtonList(string ReportID, string Params, int pageIndex = 1, int perCount = 10)
        {
            if(Params==";")
                InitReport("按钮列表", "/Permision2/CRUD/ButtonAdd", pageIndex, perCount);
            else
                InitReport("按钮列表", "/Permision2/CRUD/ButtonAdd?menuid="+Params, pageIndex, perCount);
            return View(ButtonList_M.ToViewModel(Params));
        }
        public ActionResult MenuList(string ReportID, string Params="MRoot", int pageIndex = 1, int perCount = 10)
        {
           var fathers= _permission.FindFather(Params);
            InitReport("菜单列表", "/Permision2/CRUD/MenuAdd?fartherid=" + Params, pageIndex, perCount);
            return View(MenuList_M.ToViewModel(fathers));
        }
        public ActionResult UserRoleList(string ReportID, string Params, int pageIndex = 1, int perCount = 10)
        {
            var user = _user.Find(Params);
            InitReport("分配角色", "/Permision2/CRUD/UserRoleAdd?userid="+Params, pageIndex, perCount);
            return View(UserRoleList_M.ToViewModel(user));
        }
        public ActionResult RoleMenuList(string ReportID, string Params, int pageIndex = 1, int perCount = 10)
        {
            var role = _role.Find(Params);
            InitReport("分配菜单", "/Permision2/CRUD/RoleMenuAdd?roleid="+Params, pageIndex, perCount);
            return View(RoleMenuList_M.ToViewModel(role));
        }
        public ActionResult BanButtonList(string ReportID, string Params, int pageIndex = 1, int perCount = 10)
        {
            var role = _role.Find(Params);
            InitReport("按钮禁用", "/Permision2/CRUD/BanButtonAdd?roleid="+Params, pageIndex, perCount);
            return View(BanButtonList_M.ToViewModel(role));
        }
        public ActionResult ChooseBanButtonList(string ReportID,string roleid, string Params, int pageIndex = 1, int perCount = 10)
        {
            InitReport("选择禁用按钮", "#", pageIndex, perCount, "roleid=" + roleid);
            return View();
        }
        public ActionResult ChooseRoleList(string ReportID, string userid, string Params, int pageIndex = 1, int perCount = 10)
        {
            InitReport("选择分配角色", "#", pageIndex, perCount, "userid=" + userid);
            return View();
        }
        public ActionResult ChooseMenuList(string ReportID, string roleid,string buttonid, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (buttonid.HasValue())
            {
                InitReport("选择菜单", "#", pageIndex, perCount, "buttonid=" + buttonid);
            }
            else
            {
                InitReport("选择菜单", "#", pageIndex, perCount, "roleid=" + roleid);
            }
            return View();
        }
    }
}