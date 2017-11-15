using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Community.Entity;
using Qx.Report.Scsxxt.Models;
using Qx.Tools.Scsxxt.CommonExtendMethods;
using ServicePlatform.Controllers.Base;
using ServicePlatform.ViewModels.Demo;

namespace ServicePlatform.Controllers
{
    public class DemoController : BaseController
    {
        //
        // GET: /Demo/

        public ActionResult Index()
        {
            InitAdminLayout("表单控件模版", "#");
            return View();
        }
        // /Demo/CutPage?pageIndex=1&perCount=10
        public ActionResult CutPage(int pageIndex = 1, int perCount = 10)
        {
            var data = new CommunityDbContext().T_User.Select(a => a.UserID).ToList();
            var model = InitCutPage(data, pageIndex, perCount);
            return View(model);
        }
        // /Demo/Report?ReportID=Test_用户查询&Params=;
        public ActionResult Report(string ReportID = "1", string Params = ";", int pageIndex = 1, int perCount = 10)
        {
            InitReport("ReportDemo", "#","",true,"Qx.System");
            return View();
        }
  
        public ActionResult Form()
        {

            InitForm("表单控件模版",true);
            return View(Form_M.ToViewModel(1, "我是string", DateTime.Now, 1.23f, 2.42343243432434d, 'a'));
        }

       
        // /Demo/Report_Scsxxt?ReportID=Demo&Params=;
        public ActionResult Report_Scsxxt(string ReportID, string Params, int pageIndex = 1, int perCount = 10)
        {
            if (!ReportID.HasValue())
            {
                return RedirectToAction("Report_Scsxxt", new { ReportID = "Demo", Params = ";", pageIndex = 1, perCount = 10 });
            }
            InitReport("Report_Scsxxt", "#", "", true, "Qx.System");
            return View("Report");
        }
        // /Demo/CrossServiceReport
        public ActionResult CrossServiceReport(string ReportID, string Paramsint, int pageIndex = 1, int perCount = 10)
        {

            if (!ReportID.HasValue())
            {
                return RedirectToAction("CrossServiceReport", new { ReportID = "Demo", Params = ";", pageIndex = 1, perCount = 10 });
            }

            var paramList = new List<CrossDbParam>();
            paramList.Add(new CrossDbParam()
            {
                ParamIndex = 5,
                OutIndex = 6,//应当与多配置的标题所在列索引相同
                Func = (a) =>
                {
                    return a == "10" ? "每页10条" : "每页不是10条";
                }
            });
            InitReport(paramList, "CrossServiceReport", "#", "", true, "Qx.System");
            return View("Report");
        }
        // /Demo/Report2?ReportID=System&Params=;
        public ActionResult Report2(string ReportID, string Params)
        {
            InitReport("ReportDemo(前端分页)", "#", "", true, "Qx.System");
            return View();
        }
    }
}
