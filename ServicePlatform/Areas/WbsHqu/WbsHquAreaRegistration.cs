using System.Web.Mvc;

namespace ServicePlatform.Areas.WbsHqu
{
    public class WbsHquAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WbsHqu";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WbsHqu_default",
                "WbsHqu/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}