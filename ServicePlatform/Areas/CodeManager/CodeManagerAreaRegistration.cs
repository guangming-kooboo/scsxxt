using System.Web.Mvc;

namespace ServicePlatform.Areas.CodeManager
{
    public class CodeManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CodeManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CodeManager_default",
                "CodeManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
