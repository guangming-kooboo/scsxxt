using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services.Entity;
using Qx.Community.Entity;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Controllers.Base;
using ServicePlatform.Models.Template;

namespace ServicePlatform.Controllers
{
    public class RController : BaseAccountController 
    {
        //
        // GET: /R/Wbs

        public ActionResult Wbs()
        {
            return Alert("欢迎进入工作量子系统,请稍后...", "/Wbs/Admin/NodeList?ReportID=Wbs_工作量列表");
        }
        //public ActionResult Report()
        //{
        //    return Alert("欢迎进入报表引擎,请稍后...", "/Report/Home/Index?ReportID=System&Params =;");
        //}
        //测试 /Report/Index


        public ActionResult Index(string ReportID = "4", string Params = ";;;;;;;;;;")
        {
            if (!ReportID.HasValue())
            {
                return new RedirectResult("/R/Index?ReportID=4&Params=;;;;;;;;;;");
            }
            var paramArray = Params.Split(';');

            var items = new List<ItemList>()
            {
                new ItemList("批次号", S<T_PracBatch>().ToSelectItems(paramArray[0])),
                 new ItemList("岗位名字", S<C_Position>().ToSelectItems(paramArray[1])),
                    new ItemList("年级", S<C_Specialty>().GetSelectItems<C_Specialty>()),
                      new ItemList("专业", S<C_Specialty>().ToSelectItems(paramArray[3])),
                        new ItemList("班级", S<T_Class>().ToSelectItems(paramArray[4])),
                          new ItemList("志愿顺序",(new List<SelectListItem>() {new SelectListItem() {Text = "1",Value = "1"},new SelectListItem() {Text = "2",Value = "2"} }).Format(paramArray[5])),
                             new ItemList("岗位顺序",(new List<SelectListItem>() {new SelectListItem() {Text = "1",Value = "1"},new SelectListItem() {Text = "2",Value = "2"} }).Format(paramArray[6])),
                                new ItemList("企业", S<T_Enterprise>().ToSelectItems(paramArray[7])),
            };
            ViewData["ReportID"] = ReportID; ViewData["Params"] = Params;
            return View(items);
        }
    }
}
