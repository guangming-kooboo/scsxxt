using System.Web.Mvc;
using Qx.Report.Interfaces;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Controllers
{
    public class HomeController : BaseController
    {


        private IReportServices reportServices;

        public HomeController(IReportServices reportServices)
        {
            this.reportServices = reportServices;
        }


        //
        // GET: /Home/Report
        public ActionResult Report()
        {
            return Alert("欢迎进入报表引擎", "/Report/Home/Index?ReportID=System&Params =;");
        }
        public ActionResult Index(int id=-1)
       {
            return Redirect("/Platform/Admin/Index");
            if (id == -1)
                return Redirect("/Platform/Home/Index");
            else
                return View();
        }
        //富文本编辑器页面
        public ActionResult Editor()
        {
            return View();
        }

        //测试 /Home/Report

        //public ActionResult Report(string ReportID, string Params)
        //{
        //    ViewData["ReportID"] = ReportID; ViewData["Params"] = Params;
        //    return PartialView(reportServices.ToHtml(ReportID, Params));
        //}
        //public ActionResult ReportToExcel(string ReportID, string Params)
        //{
        //    return File((reportServices.ToExcel(
        //        ReportID, 
        //        Params,
        //        GetProjectDir("Include\\Template.xlsx"), 
        //        GetProjectDir("Include\\报表.xlsx"))),
        //        "application/zip-x-compressed", "报表.xlsx") ;
        //}
        //  /Home/Test2
        public ActionResult Test2()
        {
              return View();
        }
        //  /Home/Test3
        public void Test3()
        {
            string UnitCode = "12345";
            int a;
            a = new ServicePlatform.Areas.Platform.Controllers.HomeController().CreateEntDirectorys(UnitCode);
            a=new ServicePlatform.Areas.Platform.Controllers.HomeController().DeleteEntDirectorys(UnitCode);
        }
        //  return Redirect("/Home/ErrorPage?ErrorCode="+"");
        public ActionResult ErrorPage(string ErrorCode )
        { 
            switch (ErrorCode){
                case "EntNoExist": {  ViewBag.ErrorMessage = "企业不存在！"; } break;
                case "404": {  ViewBag.ErrorMessage = "访问的页面不存在！"; } break;
                case "InserFailer": { ViewBag.ErrorMessage = "插入数据失败！"; } break;

            }
            return View();
        }
     
    }
}
