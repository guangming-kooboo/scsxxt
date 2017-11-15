using System.Web.Mvc;

namespace ServicePlatform.Areas.AppStudent
{
    public class AppStudentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AppStudent";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AppStudent_default",
                "AppStudent/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
