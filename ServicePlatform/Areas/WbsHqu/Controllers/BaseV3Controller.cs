using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class BaseV3Controller : BaseAccountController
    {
        //
        // GET: /WbsHqu/BaseV3/

        protected void InitReport(string Title, string AddLink, int pageIndex, int perCount, bool showDeafultButton = true, string ExtraParam = "")
        {
            base.InitReport(Title, AddLink, pageIndex, perCount, ExtraParam, "SCSXXT_DEV");
        }

    }
}
