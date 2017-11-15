using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.V2.Controllers
{
    public class BaseV2Controller : BaseController
    {
        protected void InitReport(string Title, string AddLink,  string ExtraParam = "", bool showDeafultButton=false)
        {
            base.InitReport(Title, AddLink, ExtraParam, showDeafultButton, "Qx.Scsxxt.Extentsion");
        }

    }
}
