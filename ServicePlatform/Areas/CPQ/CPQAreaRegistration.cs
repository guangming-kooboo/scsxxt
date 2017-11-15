using System.Web.Mvc;

namespace ServicePlatform.Areas.CPQ
{
    public class CPQAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CPQ";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CPQ_default",
                "CPQ/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
