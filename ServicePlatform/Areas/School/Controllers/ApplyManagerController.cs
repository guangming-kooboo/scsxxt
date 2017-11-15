using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;

namespace ServicePlatform.Areas.School.Controllers
{
    public class ApplyManagerController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();


        [BaseActionFilter]
        public ActionResult PlatFormApply()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string nowbatch = SchoolHelper.GetCurrentBatch(schoolid);

            var q1 = (from f in sc.SchoolPubToUnit where f.PracBatchID == nowbatch select f).ToList();

            string[] OpenStatus = new string[q1.Count];
            string[] ApplyStatus = new string[q1.Count];

            for (int i = 0; i < q1.Count;i++ )
            {
                if(q1[i].ApplyStatusName!="审核通过")
                {
                    OpenStatus[i] = "未开放";
                    ApplyStatus[i] = q1[i].ApplyStatusName;
                }
                else
                {
                    OpenStatus[i] = "开放";
                    ApplyStatus[i] = q1[i].ApplyStatusName;
                }
            }

            ViewBag.List = q1;
            ViewBag.OpenStatus = OpenStatus;
            ViewBag.ApplyStatus = ApplyStatus;
            ViewBag.FenGe = "|";
            return View();
        }

        [BaseActionFilter]
        public ActionResult PassTheApply(string praid,string unitid)
        {
            T_SchoolPubToUnit ts = sc.SchoolPubToUnit.Find(praid, unitid);
            ts.ApplyStatusID = 2;
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        [BaseActionFilter]
        public ActionResult RejectTheApply(string praid, string unitid)
        {
            T_SchoolPubToUnit ts = sc.SchoolPubToUnit.Find(praid, unitid);
            ts.ApplyStatusID = 3;
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        [BaseActionFilter]
        public ActionResult RollBackTheApply(string praid, string unitid)
        {
            T_SchoolPubToUnit ts = sc.SchoolPubToUnit.Find(praid, unitid);
            ts.ApplyStatusID = 1;
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

    }
}
