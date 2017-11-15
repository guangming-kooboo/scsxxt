using System.Web.Mvc;

namespace ServicePlatform.Areas.ServiceApi
{
    public class ServiceApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ServiceApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ServiceApi_default",
                "ServiceApi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
