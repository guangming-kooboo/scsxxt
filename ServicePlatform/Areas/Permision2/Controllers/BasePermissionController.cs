using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Permision2.Controllers
{
    public class BasePermissionController : BaseAccountController
    {
        protected void InitReport(string Title, string AddLink, int pageIndex, int perCount, string ExtraParam = "")
        {
            base.InitReport(Title, AddLink, pageIndex, perCount, ExtraParam, "Qx.Permission");
        }
    }
}
