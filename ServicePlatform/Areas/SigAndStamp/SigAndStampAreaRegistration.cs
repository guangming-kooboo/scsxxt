using System.Web.Mvc;

namespace ServicePlatform.Areas.SigAndStamp
{
    public class SigAndStampAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SigAndStamp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SigAndStamp_default",
                "SigAndStamp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
