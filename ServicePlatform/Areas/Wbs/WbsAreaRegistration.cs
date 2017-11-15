using System.Web.Mvc;

namespace ServicePlatform.Areas.Wbs
{
    public class WbsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Wbs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Wbs_default",
                "Wbs/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
