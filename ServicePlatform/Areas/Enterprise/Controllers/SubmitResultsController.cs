using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.Enterprise.Controllers
{
    public class SubmitResultsController : Controller
    {
        public ActionResult Results(string results)
        {
            ViewBag.Results = results;
            return View();
        }

    }
}
