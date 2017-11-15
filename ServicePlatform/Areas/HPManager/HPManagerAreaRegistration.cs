using System.Web.Mvc;

namespace ServicePlatform.Areas.HPManager
{
    public class HPManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HPManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HPManager_default",
                "HPManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
