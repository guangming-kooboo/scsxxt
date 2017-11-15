using System.Collections.Generic;
using System.Web.Mvc;
using Core.Services.Entity;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Controllers.Base;
using ServicePlatform.Models.Template;

namespace ServicePlatform.Areas.Wbs.Controllers
{
    public class TestController : BaseController
    {
        //测试 Wbs/Test/Index


        public ActionResult Index(string ReportID = "4", string Params = ";;;;;;;;;;")
        {
            var paramArray= Params.UnPackString(';');
           
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
