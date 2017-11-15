using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.BBS.Controllers
{
    public class TestController : BaseController
    {
        //BBS/Test/Index
        public ActionResult Index()
        {
            return View();
        }
        //BBS/Test/FileTest
        public ActionResult FileTest()
        {
            return View();
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult FileTest(HttpPostedFileBase photo)
        {
            //var ttt = UploadFile(photo, "/UserFiles/BBS/Files/");
           
            return Alert(DataContext.UserID);
        }
    }
}
