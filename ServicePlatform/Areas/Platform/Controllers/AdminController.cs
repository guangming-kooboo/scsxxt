using ServicePlatform.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Platform.Controllers
{
    public class AdminController : BaseAccountController
    {
        //
        // GET: /Platform/Admin/Index
     
        public ActionResult Index(string right= "Home")
        {
            if (DataContext.IsSchool)
            {
                ViewData["right"] = "IsSchool";
            }
            else if(DataContext.IsEnterprise)
            {
                ViewData["right"] = "IsEnterprise";
            }
            else if (DataContext.IsPlatform)
            {
                ViewData["right"] = "IsPlatform";
            }
            else if (DataContext.IsTeacher)
            {
                ViewData["right"] = "IsTeacher";
            }
            else if (DataContext.IsStudent)
            {
                ViewData["right"] = "IsStudent";
            }
            else if (DataContext.IsEmployee)
            {
                ViewData["right"] = "IsEmployee";
            }
            else
            {
                ViewData["right"] = "Home";
            }
            return View();
        }
       
        public ActionResult Top()
        {
            
            return View();
        }
       
        public ActionResult Left()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult IsSchool()
        {
            return View();
        }
        public ActionResult IsEnterprise()
        {
            return View();
        }
        public ActionResult IsPlatform()
        {
            return View();
        }
        public ActionResult IsTeacher()
        {
            return View();
        }
        public ActionResult IsStudent()
        {
            return View();
        }
        public ActionResult IsEmployee()
        {
            return View();
        }
    }
}
