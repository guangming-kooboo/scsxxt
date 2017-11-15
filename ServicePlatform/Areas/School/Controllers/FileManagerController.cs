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
    public class FileManagerController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();
        public ActionResult FileUpDownLoad(string focus)
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取报名须知
            var q = (from f in sc.DownLoadFiles where f.UnitID == schoolid &&f.DLFileColumnID==10 select f).ToList();
            ViewBag.EnrollMustKnow = q;

            //获取实习指导
            var q1 = (from f in sc.DownLoadFiles where f.UnitID == schoolid && f.DLFileColumnID == 11 select f).ToList();
            ViewBag.PraGuide = q1;

            if (focus == null)
            {
                ViewBag.Focus = "tab1";
            }
            else
            {
                ViewBag.Focus = focus;
            }

            return View();
        }
        //上传报名须知
        public ActionResult UpLoadEnrollMustKnow(HttpPostedFileBase EnrollMustKnow)
        {
            if (EnrollMustKnow != null)//如果重新提交
            {
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                T_DownLoadFiles td = new T_DownLoadFiles();
                td.DLFileColumnID = 10;
                td.DLFileID = DateTimeFormat.ConvertDateTimeInt(DateTime.Now).ToString();
                td.DLFilePubUser = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
                td.UnitID = schoolid;
                td.PubTime = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
                td.DLFileTitle = EnrollMustKnow.FileName;

                string url = "/UserUploadFile/School/" + schoolid + "/OtherFiles/SchoolSourceFile/";
                string result = Lib.FileOperate.UploadFile_OriName(EnrollMustKnow, url);
                td.DLFileUrl = result;

                sc.DownLoadFiles.Add(td);
                sc.SaveChanges();
            }
            else
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "文件不能为空" });
            }
            string u = "/School/FileManager/FileUpDownLoad?focus=tab1";
            return Redirect(u);
        }

        //查看报名须知
        public ActionResult ViewEnrollMustKnow(string id)
        {
            T_DownLoadFiles tu = sc.DownLoadFiles.Find(id);
            string old = tu.DLFileUrl;
            return Redirect(old);
        }

        //删除报名须知
        public ActionResult DeleteEnrollMustKnow(string id)
        {
            T_DownLoadFiles tu = sc.DownLoadFiles.Find(id);
            sc.DownLoadFiles.Remove(tu);
            string u = "/School/FileManager/FileUpDownLoad?focus=tab1";
            return Redirect(u);
        }

        //上传实习指导
        public ActionResult UpLoadPraGuide(HttpPostedFileBase PraGuide)
        {
            if (PraGuide != null)//如果重新提交
            {
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                T_DownLoadFiles td = new T_DownLoadFiles();
                td.DLFileColumnID = 11;
                td.DLFileID = DateTimeFormat.ConvertDateTimeInt(DateTime.Now).ToString();
                td.DLFilePubUser = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
                td.UnitID = schoolid;
                td.PubTime = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
                td.DLFileTitle = PraGuide.FileName;

                string url = "/UserUploadFile/School/" + schoolid + "/OtherFiles/SchoolSourceFile/";
                string result = Lib.FileOperate.UploadFile_OriName(PraGuide, url);
                td.DLFileUrl = result;

                sc.DownLoadFiles.Add(td);
                sc.SaveChanges();
            }
            else
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "文件不能为空" });
            }
            string u = "/School/FileManager/FileUpDownLoad?focus=tab2";
            return Redirect(u);
        }

        //查看实习指导
        public ActionResult ViewPraGuide(string id)
        {
            T_DownLoadFiles tu = sc.DownLoadFiles.Find(id);
            string old = tu.DLFileUrl;
            return Redirect(old);
        }

        //删除实习报告
        public ActionResult DeletePraGuide(string id)
        {
            T_DownLoadFiles tu = sc.DownLoadFiles.Find(id);
            sc.DownLoadFiles.Remove(tu);
            string u = "/School/FileManager/FileUpDownLoad?focus=tab2";
            return Redirect(u);
        }

    }
}
