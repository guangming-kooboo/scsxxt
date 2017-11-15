using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Core.Services;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services.Utility;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Platform.Controllers
{
    public class UnitsController : BaseController
    {
        public UnitServices Services
        {
          get { return new UnitServices(); } 
        }

        public ActionResult Index(string id)
        {
           return View(Services.GetUnitList());
        }

       
    }
}
