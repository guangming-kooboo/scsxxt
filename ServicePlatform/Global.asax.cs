using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Ioc.Unity;

namespace ServicePlatform
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityIoc.Register(typeof (Controller).GetSubClasses());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            //Response.Redirect("/content/404/");
            //Exception ex = Server.GetLastError();
            //if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 404)
            //{
               
            //}
        }
       
    }
}