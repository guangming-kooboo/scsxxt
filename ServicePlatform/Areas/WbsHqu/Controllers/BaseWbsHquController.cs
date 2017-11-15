using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.WbsHqu.Controllers
{
    public class BaseWbsHquController : BaseAccountController
    {
        protected void InitReport(string Title, string AddLink, string ExtraParam, int pageIndex=1, int perCount=10, bool showDeafultButton = true)
        {
            base.InitReport(Title, AddLink, ExtraParam, showDeafultButton, "Qx.Wbs.Hqu");
        }

    }
}