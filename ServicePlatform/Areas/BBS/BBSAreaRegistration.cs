using System.Web.Mvc;

namespace ServicePlatform.Areas.BBS
{
    public class BBSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BBS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BBS_default",
                "BBS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
