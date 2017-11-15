using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Permission.Entity;
using Qx.Permission.Interfaces;
using Qx.Permission.Models;
using ServicePlatform.Areas.Permision2.ViewModels.CRUD;

namespace ServicePlatform.Areas.Permision2.Controllers
{
    public class HomeController : BasePermissionController
    {
        //
        // GET: /Permision2/Home/
        private IPermission _permission;
        public HomeController(IPermission permission)
        {
            _permission = permission;
        }
        //获取菜单[排序后]
       
        //public ActionResult Index()
        //{
        //    return View();
        //   //(new Index_M()
        //   //{
        //   //   // MenuItems = (_permission.GetMenuByUserId("10385-14093"))
        //   //}
        //   );
        //}

    }
}
