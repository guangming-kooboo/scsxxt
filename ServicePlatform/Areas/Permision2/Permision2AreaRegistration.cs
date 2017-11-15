using System.Web.Mvc;

namespace ServicePlatform.Areas.Permision2
{
    public class Permision2AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Permision2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Permision2_default",
                "Permision2/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
