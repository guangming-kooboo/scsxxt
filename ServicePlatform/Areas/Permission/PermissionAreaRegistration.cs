using System.Web.Mvc;

namespace ServicePlatform.Areas.Permission
{
    public class PermissionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Permission";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Permission_default",
                "Permission/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
