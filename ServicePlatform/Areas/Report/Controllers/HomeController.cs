using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Report.Interfaces;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.Report.Controllers
{
    public class HomeController : BaseAccountController
    {
        private readonly IReportServices reportServices;

        public HomeController(IReportServices reportServices)
        {
            this.reportServices = reportServices;
        }

       


        //Report/Home/Index?ReportID=System&Params = ";"
        public ActionResult Index(string ReportID = "System", string Params = ";", int pageIndex=1, int perCount=10)
        {
            InitReport("报表引擎", "/Report/Home/Add", pageIndex, perCount);
             return View();
        }
        public ActionResult Add()
        {
            InitAdminLayout("添加报表", CurrentUrl());
            return View(new Qx.Report.Models.Report());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Qx.Report.Models.Report model)
        {
            if (reportServices.Add(model))
                return Redirect(BackToReport);
            else
            {
                return Alert("添加失败",-1);
            }
           
        }
        public ActionResult Edit(string id)
        {
            InitAdminLayout("编辑报表", CurrentUrl());
            return View(reportServices.GetReprot(id));
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Qx.Report.Models.Report model)
        {
            if (reportServices.Update(model))
                return Redirect(BackToReport);
            else
            {
                return Alert("保存失败", -1);
            }
        }
        public ActionResult Delete(string id,string key="")
        {
            if (string.IsNullOrEmpty(key))
            {
                return   Alert("请输入删除确认码！", -1);
            }
            if (reportServices.Delete(id))
            {
                return Alert("删除成功",BackToReport);
            }
            else
            {
                return Alert("删除失败", -1);
            }
        }

        //  字符                   转义后
        //   ;                    $semicolon

        public ActionResult Report(string ReportID, string Params,string ExtraParam, string AddLink, string Title, int pageIndex , int perCount, string dbConnStringKey)
        {
            ViewData["ReportID"] = ReportID; ViewData["Params"] = Params;
            ViewData["AddLink"] = AddLink; ViewData["ExtraParam"] = ExtraParam;
            ViewBag.Title = Title;
            var table = reportServices.ToHtml(ReportID, Params, dbConnStringKey);
            var header = new List<string>(table[0]);
            table.Remove(table[0]);
            table = InitCutPage(table, pageIndex, perCount);
            table.Insert(0,header);
            return PartialView(table);
        }
        public ActionResult ReportToExcel(string ReportID, string Params, string dbConnStringKey)
        {
            return File((reportServices.ToExcel(
                ReportID, 
                Params,
                GetProjectDir("Include\\Template.xlsx"), 
                GetProjectDir("Include\\报表.xlsx"),
                dbConnStringKey)),
                "application/zip-x-compressed", "报表.xlsx"
                ) ;
        }
    }
}
