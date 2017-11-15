using System.Web.Mvc;

namespace ServicePlatform.Areas.AppTeacher2
{
    public class AppTeacher2AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AppTeacher2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AppTeacher2_default",
                "AppTeacher2/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
