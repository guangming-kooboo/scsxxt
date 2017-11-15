using System.Web.Mvc;

namespace ServicePlatform.Areas.DownLoadFile
{
    public class DownLoadFileAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DownLoadFile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DownLoadFile_default",
                "DownLoadFile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
