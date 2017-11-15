using System.Web.Mvc;

namespace ServicePlatform.Areas.Enterprise
{
    public class EnterpriseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Enterprise";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Enterprise_default",
                "Enterprise/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
