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
    public class S_EmployeManagerController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();

        //加入人才库
        [BaseActionFilter]
        public ActionResult EnterTalentPool()
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            var q_check=(from f in sc.StuBatchReg where f.UserID==userid select f).ToList();
            if(q_check.Count==0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "您还未被注册到任何一个批次，暂不能进行该页的相关操作！" });
            }
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);

            string temp = SchoolHelper.GetStuPracticeNo(userid);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "未注册批次，数据出错!" });
            }
            else
            {
                praticeno = temp;
            }
            string PracticeNo = praticeno;

            var q1 = (from f in ctc.C_StuInfoType select f).ToList();

            string[] OpenStatus = new string[q1.Count];

            for (int i = 0; i < q1.Count; i++)
            {
                int id = q1[i].InfoTypeID;
                var q = (from f in sc.StuInfoPub where f.PracticeNo == PracticeNo && f.InfoTypeID == id select f).ToList();

                
                if (q.Count==0)
                {
                    OpenStatus[i] = "已开放";
                }
                else
                {
                    OpenStatus[i] = "已隐藏";
                }
            }

            ViewBag.List = q1;
            ViewBag.OpenStatus = OpenStatus;
            ViewBag.FenGe = "|";
            return View();
        }

        //信息发布相关操作
        [BaseActionFilter]
        public ActionResult PubOperation(int infotypeid, string to, string type)
        {

            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);

            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;

            string temp = SchoolHelper.GetStuPracticeNo(userid);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "未注册批次，数据出错!" });
            }
            else
            {
                praticeno = temp;
            }
            string PracticeNo = praticeno;

            if (type == "hide")
            {
                T_StuInfoPub temp1 = new T_StuInfoPub();
                temp1.PracticeNo = PracticeNo;
                temp1.InfoTypeID = infotypeid;
                temp1.PubLevel = "学校级";
                sc.StuInfoPub.Add(temp1);
                sc.SaveChanges();
            }
            else
            {
                T_StuInfoPub ts = sc.StuInfoPub.Find(PracticeNo, infotypeid);
                if (ts != null)
                {
                    sc.StuInfoPub.Remove(ts);
                    sc.SaveChanges();
                }                
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        public ActionResult MyResume(string focus, string StuPracNo,string Entid)
        {
            string praticeno = string.Empty;
            string temp=string.Empty;
            if (StuPracNo == null)
            {
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
                string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
                //string praticeno = userid + prcbatchid;
                temp = SchoolHelper.GetStuPracticeNo(userid);

                if (temp == "未注册批次，数据出错!")
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "未注册批次，数据出错!" });
                }
                else
                {
                    praticeno = temp;
                }
                ViewBag.PraticeNo = praticeno;
            }      
            else
            {
                praticeno = StuPracNo;          
            }
            ViewBag.PraticeNo = praticeno;
            //获取我的简历列表
            var q_resume = (from f in sc.Resume where f.PracticeNo == praticeno select f).ToList();
            ViewBag.Resume = q_resume;


            List<T_RecruitPosition> My_Resume_Sended=new List<T_RecruitPosition>();

            List<T_RecruitPosition> Resume_Get = new List<T_RecruitPosition>();

            //投递简历
            #region 投递简历
            //获取学校当前批次
            //string praticeno = userid + prcbatchid;


            ViewBag.PracticeNo = praticeno;

            //var q = (from f in ec.T_EntBatchReg where f.PracBatchID == prcbatchid select f).ToList();
            var q = (from f in ec.T_Enterprise select f).ToList();

            List<T_RecruitPosition> tr = new List<T_RecruitPosition>();
            if (Request.Form["Enterprise"] == null && q.Count != 0 && Entid==null)
            {
                string temp1 = q[0].Ent_No;

                var q_pos = (from f in ec.RecruitPosition where f.Ent_No == temp1 select f).ToList();

                ViewBag.PositionList = q_pos;
            }
            else
            {
                //string entno = Request.Form["Enterprise"];
                var q_pos = (from f in ec.RecruitPosition where f.Ent_No == Entid select f).ToList();
                ViewBag.PositionList = q_pos;
            }
            //获取公司列表
            List<SelectListItem> Enterprise = new List<SelectListItem>();
            for (int i = 0; i < q.Count; i++)
            {
                //Enterprise.Add(new SelectListItem() { Text = q[i].Ent_Name, Value = q[i].Ent_No });
                if (q[i].Ent_No == Entid)
                {
                    Enterprise.Add(new SelectListItem() { Text = q[i].Ent_Name, Value = q[i].Ent_No, Selected = true });
                    //SelectedEntpracticeNo = q1[i].EntPracNo;
                    //同时获得选中企业的“企业实习号”
                    //SelectedEntName = q1[i].Ent_Name;
                }
                else
                {
                    Enterprise.Add(new SelectListItem() { Text = q[i].Ent_Name, Value = q[i].Ent_No, Selected = false });
                }
            }
            ViewData["Enterprise"] = Enterprise;


            //获取简历列表
            List<SelectListItem> Resume_L = new List<SelectListItem>();
            if (q_resume.Count == 0)
            {
                Resume_L.Add(new SelectListItem() { Text = "空", Value = "空" });

                ViewBag.CanSend = "false";
            }
            else
            {
                ViewBag.CanSend = "true";
                for (int i = 0; i < q_resume.Count; i++)
                {
                    Resume_L.Add(new SelectListItem() { Text = q_resume[i].ResumeName, Value = q_resume[i].ResumeURL });
                }
            }
            ViewData["Resume_L"] = Resume_L;
            #endregion

            //获取我的简历投递历史
            var q_history = (from f in sc.JobWanted where f.PracticeNo == praticeno&&f.Flag=="自投" select f).ToList();

            var q_history_o = (from f in sc.JobWanted where f.PracticeNo == praticeno && f.Flag == "企业邀请" select f).ToList();

            for (int i = 0; i < q_history.Count;i++ )
            {
                string entno = q_history[i].Ent_No;
                string posid = q_history[i].PositionID;
                T_RecruitPosition temp1 = ec.RecruitPosition.Find(entno, posid);
                if (temp != null)
                {
                    My_Resume_Sended.Add(temp1);
                }
            }

            for (int i = 0; i < q_history_o.Count; i++)
            {
                string entno = q_history_o[i].Ent_No;
                string posid = q_history_o[i].PositionID;
                T_RecruitPosition temp1 = ec.RecruitPosition.Find(entno, posid);
                if (temp1 != null)
                {
                    Resume_Get.Add(temp1);
                }
            }
            ViewBag.History = q_history;//自投简历历史信息（面试时间等）
            ViewBag.My_Resume_Sended = My_Resume_Sended;//自投简历岗位信息
            ViewBag.History2 = q_history_o;//企业邀请历史信息（面试时间等）     
            ViewBag.Resume_Get = Resume_Get;//企业邀请岗位信息   

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

        //添加简历--文件
        [BaseActionFilter]
        public ActionResult AddNewResume_ByFile(HttpPostedFileBase Resume)
        {
            //string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            string practiceno = Request.Form["practiceno"];
            T_Resume tu = new T_Resume(); ;
            if (Resume != null)//如果重新提交
            {
                T_Resume tu_check = sc.Resume.Find(practiceno, Resume.FileName);
                if(tu_check!=null)
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "简历名不能重复" });
                }
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                string url = "/UserFiles/School/" + schoolid + "/OtherFiles/StuResume/";
                string result = Lib.FileOperate.UploadFile_OriName(Resume, url);
                tu.PubTime = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
                tu.PracticeNo = practiceno;
                tu.ResumeURL = result;
                tu.ResumeName = Resume.FileName;

                var q_check = sc.Resume.Where(a => a.PracticeNo == practiceno).ToList();
                if(q_check.Count==0)
                {
                    tu.IsDefault = 1;
                }
                else
                {
                    tu.IsDefault = 0;
                }
                sc.Resume.Add(tu);
                sc.SaveChanges();
            }
            else
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "文件不能为空" });
            }
            string u = "/School/S_EmployeManager/MyResume?focus=tab1";
            return Redirect(u);
            //return Content("<script>alert('文件上传成功!');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //删除实习报告
        [BaseActionFilter]
        public ActionResult DeleteResume(string practiceno,string name)
        {
            T_Resume tu = sc.Resume.Find(practiceno,name);
            if(tu!=null)
            {
                Lib.FileOperate.DeleteFlie(tu.ResumeURL);
                sc.Resume.Remove(tu);
                sc.SaveChanges();
            }
            string u = "/School/S_EmployeManager/MyResume?focus=tab1";
            return Redirect(u);
            //return Content("<script>alert('删除成功!');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //预览实习报告
        [BaseActionFilter]
        public ActionResult ViewResume(string practiceno, string name)
        {
            T_Resume tu = sc.Resume.Find(practiceno, name);
            string old = tu.ResumeURL;
            //string path = AppDomain.CurrentDomain.BaseDirectory + "Areas/School/Content/Home/file/";
            //string fileName = "学生信息导入模版.xls";
            return Redirect(old);
        }

        //设简历为默认
        [BaseActionFilter]
        public ActionResult SetResumeDefault(string practiceno, string name)
        {
            T_Resume tu = sc.Resume.Find(practiceno, name);
            tu.IsDefault = 1;
            sc.SaveChanges();
            var q = sc.Resume.Where(a => a.PracticeNo == practiceno & a.ResumeName != name).ToList();
            for(int i=0;i<q.Count;i++)
            {
                T_Resume temp = q[i];
                temp.IsDefault = 0;
                sc.SaveChanges();
            }
            string u = "/School/S_EmployeManager/MyResume?focus=tab1";
            return Redirect(u);
        }

        //投递简历
        [BaseActionFilter]
        public ActionResult SendMyResume(string Entid)
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //string praticeno = userid + prcbatchid;
            string temp = SchoolHelper.GetStuPracticeNo(userid);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "未注册批次，数据出错!" });
            }
            else
            {
                praticeno = temp;
            }

            ViewBag.PracticeNo = praticeno;

            //var q = (from f in ec.T_EntBatchReg where f.PracBatchID == prcbatchid select f).ToList();
            var q = (from f in ec.T_Enterprise select f).ToList();
            var q_resume = (from f in sc.Resume where f.PracticeNo == praticeno select f).ToList();

            List<T_RecruitPosition> tr = new List<T_RecruitPosition>();
            if (Request.Form["Enterprise"] == null && q.Count != 0)
            {
                string temp1 = q[0].Ent_No;

                var q_pos = (from f in ec.RecruitPosition where f.Ent_No == temp1 select f).ToList();

                ViewBag.PositionList = q_pos;
            }
            else
            {
                string entno = Request.Form["Enterprise"];
                var q_pos = (from f in ec.RecruitPosition where f.Ent_No == entno select f).ToList();
                ViewBag.PositionList = q_pos;
            }
            //获取公司列表
            List<SelectListItem> Enterprise = new List<SelectListItem>();
            for(int i=0;i<q.Count;i++)
            {
                //Enterprise.Add(new SelectListItem() { Text = q[i].Ent_Name, Value = q[i].Ent_No });
                if (q[i].Ent_No == Entid)
                {
                    Enterprise.Add(new SelectListItem() { Text = q[i].Ent_Name, Value = q[i].Ent_No, Selected = true });
                    //SelectedEntpracticeNo = q1[i].EntPracNo;
                    //同时获得选中企业的“企业实习号”
                    //SelectedEntName = q1[i].Ent_Name;
                }
                else
                {
                    Enterprise.Add(new SelectListItem() { Text = q[i].Ent_Name, Value = q[i].Ent_No, Selected = false });
                }
            }
            ViewData["Enterprise"] = Enterprise;


            //获取简历列表
            List<SelectListItem> Resume = new List<SelectListItem>();
            if (q_resume.Count == 0)
            {
                Resume.Add(new SelectListItem() { Text = "空", Value = "空" });

                ViewBag.CanSend = "false";
            }
            else
            {
                ViewBag.CanSend = "true";
                for (int i = 0; i < q_resume.Count; i++)
                {
                    Resume.Add(new SelectListItem() { Text = q_resume[i].ResumeName, Value = q_resume[i].ResumeURL });
                }
            }
            ViewData["Resume"] = Resume;

            return View();
        }

        //投递简历操作
        [BaseActionFilter]
        public ActionResult JobWanted(string practiceno,string entno,string posid,string resume)
        {
            T_JobWanted check = sc.JobWanted.Find(practiceno, entno, posid);
            if (check != null)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "已经向该职位投递过简历，请勿重复投递！" });
            }
            else
            {
                T_JobWanted tj = new T_JobWanted();
                tj.PracticeNo = practiceno;
                tj.Ent_No = entno;
                tj.PositionID = posid;
                tj.ResumeURL = resume;
                tj.Flag = "自投";
                tj.JobStatus = 0;
                sc.JobWanted.Add(tj);
                sc.SaveChanges();
                //return Content("<script>alert('投递成功');window.location='/School/S_EmployeManager/SendMyResume';</script>");

                return Content("<script>alert('投递成功');window.location='/School/S_EmployeManager/MyResume?focus=tab2';</script>");
                //return Redirect("/School/S_EmployeManager/SendMyResume");
            }
        }

        //已经投递的简历撤回操作
        [BaseActionFilter]
        public ActionResult JobWanted_RollBack(string practiceno, string entno, string posid)
        {
            T_JobWanted check = sc.JobWanted.Find(practiceno, entno, posid);
            if (check != null)
            {
                sc.JobWanted.Remove(check);
                sc.SaveChanges();
            }
            //string u = "/School/S_EmployeManager/MyResume?focus=tab2";
            //return Redirect(u);

            return Content("<script>alert('撤回成功');window.location='/School/S_EmployeManager/MyResume?focus=tab3';</script>");
        }

        //终录情况
        [BaseActionFilter]
        public ActionResult Offer_AgreeOrNot(string practiceno, string entno, string posid,string flag)
        {
            var q = (from f in sc.JobWanted where f.PracticeNo == practiceno && f.Ent_No == entno && f.JobStatus == 5 select f).ToList();
            if(q.Count!=0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "您已经有匹配的工作，若想更换公司，请在截止日期之前回退选择！" });
            }
            T_JobWanted check = sc.JobWanted.Find(practiceno, entno, posid);
            if (check != null)
            {
                if (flag == "agree")
                {
                    check.JobStatus = 5;
                }
                else
                {
                    check.JobStatus = 6;
                }
                sc.SaveChanges();
            }
            //string u = "/School/S_EmployeManager/MyResume?focus=tab2";
            //return Redirect(u);

            return Content("<script>alert('操作成功');window.location='/School/S_EmployeManager/MyResume?focus=tab3';</script>");
        }

        //回退终录情况
        [BaseActionFilter]
        public ActionResult RollBack_AgreeOrNot(string practiceno, string entno, string posid)
        {
            T_JobWanted check = sc.JobWanted.Find(practiceno, entno, posid);
            if (check != null)
            {
                check.JobStatus = 4;
                sc.SaveChanges();
            }
            //string u = "/School/S_EmployeManager/MyResume?focus=tab2";
            //return Redirect(u);

            return Content("<script>alert('操作成功');window.location='/School/S_EmployeManager/MyResume?focus=tab3';</script>");
        }

        //终录公司登记
        [BaseActionFilter]
        public ActionResult StuFinalEntRecord()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //string praticeno = userid + prcbatchid;
            string temp = SchoolHelper.GetStuPracticeNo(userid);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "未注册批次，数据出错!" });
            }
            else
            {
                praticeno = temp;
            }

            var q = (from f in sc.StuFinalEntRecord where f.PracticeNo == praticeno select f).ToList();
            if(q.Count!=0)
            {
                ViewBag.Detail = q[0];
            }
            
            return View();  
        }

        [BaseActionFilter]
        public ActionResult StuFinalEntRecordOP()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //string praticeno = userid + prcbatchid;
            string temp = SchoolHelper.GetStuPracticeNo(userid);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "未注册批次，数据出错!" });
            }
            else
            {
                praticeno = temp;
            }
            string entname = Request.Form["entname"];
            string entcategory = Request.Form["entcategory"];
            string entrank = Request.Form["entrank"];
            string entaddress = Request.Form["entaddress"];
            string entresume = Request.Form["entresume"];
            string entemail = Request.Form["entemail"];
            string entconnect = Request.Form["entconnect"];
            if (entname == "" || entaddress == "" || entconnect == "")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "公司名称/地址/联系地址 不能为空" });
            }
            else
            {
                var q = (from f in sc.StuFinalEntRecord where f.PracticeNo == praticeno select f).ToList();
                if (q.Count == 0)
                {
                    T_StuFinalEntRecord ts = new T_StuFinalEntRecord();
                    ts.PracticeNo = praticeno;
                    ts.Ent_Name = entname;
                    ts.EntCategory = entcategory;
                    ts.EntRank = entrank;
                    ts.EntAddress = entaddress;
                    ts.EntResume = entresume;
                    ts.Email = entemail;
                    ts.Contectinfo = entconnect;
                    sc.StuFinalEntRecord.Add(ts);
                    sc.SaveChanges();
                    return Content("<script>alert('保存成功');window.location='/School/S_EmployeManager/StuFinalEntRecord';</script>");
                }
                else
                {
                    sc.StuFinalEntRecord.Remove(q[0]);
                    sc.SaveChanges();
                    T_StuFinalEntRecord ts = new T_StuFinalEntRecord();
                    ts.PracticeNo = praticeno;
                    ts.Ent_Name = entname;
                    ts.EntCategory = entcategory;
                    ts.EntRank = entrank;
                    ts.EntAddress = entaddress;
                    ts.EntResume = entresume;
                    ts.Email = entemail;
                    ts.Contectinfo = entconnect;
                    sc.StuFinalEntRecord.Add(ts);
                    sc.SaveChanges();
                    return Content("<script>alert('修改成功');window.location='/School/S_EmployeManager/StuFinalEntRecord';</script>");
                }
                
            }    
        }
    }
}
