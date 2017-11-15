using ServicePlatform.App_Start;
using ServicePlatform.Areas.DownLoadFile.Func;
using ServicePlatform.Areas.News.Lib;
using ServicePlatform.Areas.News.ToolHelper;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.DownLoadFile.Controllers
{
    public class DLListController : Controller
    {
        ServicePlatform.Models.ContentContext sb = new ServicePlatform.Models.ContentContext();
        EnterpriseContext aa = new EnterpriseContext();
        PlateformContext ab = new PlateformContext();
        ServicePlatform.Models.SchoolContext acc = new ServicePlatform.Models.SchoolContext();

        [BaseActionFilter]
        public ActionResult DLTabListByEasyUi()
        {
            string JudgeSub = "";
            string UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            string Sub = (Session["Vars"] as ServicePlatform.Config.Vars).getSubSystem();
            string UnitName = "";
            string TargetSubSystem=SessionHandler.SubSysHandle(Sub);
            if (TargetSubSystem == "Platform")
            {
                UnitName = "平台";
                JudgeSub = "平台端";
            }
            else if (TargetSubSystem == "School")
            {
                UnitName = acc.School.Find(UNITID).SchoolName;
                JudgeSub = "学校端";
            }
            else if (TargetSubSystem == "Enterprise")
            {
                UnitName = aa.T_Enterprise.Find(UNITID).Ent_Name;
                JudgeSub = "企业端";
            }
            //接受点击的Tab内容
            //string GetTabName = Request["DefaultTabTitle"];
            string GetTabName="";
            var TabCol1 = (from a in sb.C_ContentColumn where a.ContTypeID == ServicePlatform.Lib.ContentType.DownLoadFile && a.SybSystem ==  JudgeSub select a).First();
            GetTabName = TabCol1.ContColumnName;

            //资源下载的Tab显示页面
            ViewBag.UnitID = UNITID;
            ViewBag.JudgeSub = JudgeSub;

            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageCount = 2;
            if (GetTabName!=null)
            {
                var FileCount = (from a in sb.C_ContentColumn from b in sb.T_DownLoadFiles where a.ContColumnName == GetTabName && a.ContColumnID == b.DLFileColumnID select b).Count();
                pageCount = FileCount/10+1;
            }

            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = pageCount;

            ViewBag.UnitName = UnitName;
            return View();
            //return Redirect("/DownLoadFile/DLList/DLTabListByEasyUi");
            //return RedirectToAction("DLTabListByEasyUi");
        }

        [BaseActionFilter]
        public ActionResult HandleTagName() {
            string UNITID = (Session["Vars"] as ServicePlatform.Config.Vars).getUserUnit();
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int PageCount = 3;
            string GetTabName = Request["TabTitle"];
            if (GetTabName != null)
            {
                var FileCount = (from a in sb.C_ContentColumn from b in sb.T_DownLoadFiles from c in sb.T_Content where a.ContColumnName == GetTabName && a.ContColumnID == b.DLFileColumnID && c.ContentID == b.DLFileID && c.UnitID == UNITID select b).Count();
                PageCount = FileCount / 10+1;
            }
            string ContentTransfer = ServicePlatform.Areas.DownLoadFile.Func.PageBar.GetPageBar(pageIndex, PageCount);
            return Content(ContentTransfer);
        }

    }
}
