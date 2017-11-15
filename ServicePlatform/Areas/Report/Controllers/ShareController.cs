using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Report.Controllers
{
    public class ShareController : BaseController
    {
        //Report/Share/UserList?ReportID=Share_用户查询&Params =;
        public ActionResult UserList(string ReportID = "Share_用户查询", string Params = ";", int pageIndex = 1, int perCount = 10)
        {
            InitReport("用户查询", "/Report/Home/Add", pageIndex, perCount);
            return View();
        }
    }
}
