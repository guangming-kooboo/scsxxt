using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;
namespace ServicePlatform.Areas.Platform.Controllers
{
    public class PlatformApplyController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();


        //平台申请学校页面
        public ActionResult ApplySchool(string school)
        {
            string schoolid = string.Empty;
            //获取年级
            List<SelectListItem> School = SchoolHelper.GetSchoolList();
            ViewData["School"] = School;

            if (school == null)
            {
                if (Request.Form["School"] == null)
                {
                    return View();
                }
                else
                {
                    schoolid = Request.Form["School"];
                }
            }
            else
            {
                schoolid = school;
            }

            var q = (from f in sc.PracBatch where f.SchoolID == schoolid select f).ToList();
            string[] OpenStatus = new string[q.Count];
            string[] ApplyStatus = new string[q.Count];

            for (int i = 0; i < q.Count; i++)
            {
                string praid = q[i].PracBatchID;
                string unitid = "平台";
                var q1 = (from f in sc.SchoolPubToUnit where f.PracBatchID == praid && f.UnitID == unitid select f).ToList();
                if (q1.Count == 0)
                {
                    ApplyStatus[i] = "未申请";
                    OpenStatus[i] = "未开放";
                }
                else
                {
                    ApplyStatus[i] = q1[0].ApplyStatusName;
                    if (q1[0].ApplyStatusID == 2)
                    {
                        OpenStatus[i] = "开放";
                    }
                    else
                    {
                        OpenStatus[i] = "未开放";
                    }
                }
            }

            ViewBag.List = q;
            ViewBag.OpenStatus = OpenStatus;
            ViewBag.ApplyStatus = ApplyStatus;
            return View();

        }


        //平台申请开放操作
        public ActionResult SubmitApply(string schoolid, string praid)
        {
            string unitid = "平台";
            string url = string.Empty;
            T_SchoolPubToUnit ts = new T_SchoolPubToUnit();
            ts.PracBatchID = praid;
            ts.UnitID = unitid;
            ts.ApplyStatusID = 1;
            ts.StartTime = 0;
            ts.EndTime = 0;
            try
            {
                sc.SchoolPubToUnit.Add(ts);
                sc.SaveChanges();
            }
            catch
            {

            }
            finally
            {
                url = "/Platform/PlatformApply/ApplySchool?school=" + schoolid;

            }
            return Redirect(url);
        }

    }
}
