using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.V2.Controllers
{
    public class HomeController : BaseV2Controller
    {
        //
        // GET: /V2/Home/

        public ActionResult Index()
        {
            InitAdminLayout("测试");
            return View();
        }

    }
}
