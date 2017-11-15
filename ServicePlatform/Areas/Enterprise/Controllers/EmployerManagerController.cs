using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;
using System.Data.Entity.Migrations;

namespace ServicePlatform.Areas.Enterprise.Controllers
{
    public class EmployerManagerController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();


        //申请人才库
        [BaseActionFilter]
        public ActionResult ApplyToTalentPool(string school)
        {
            string schoolid = string.Empty;
            //获取年级


            if (school == null)
            {
                if (Request.Form["School"] == null)
                {
                    List<SelectListItem> School = SchoolHelper.GetSchoolList();
                    ViewData["School"] = School;
                    if (School.Count!=0)
                    {
                        schoolid = School[0].Value;
                    }                
                    //return View();
                }
                else
                {
                    string tt=Request.Form["School"];
                    List<SelectListItem> School = SchoolHelper.GetSchoolList(tt);
                    ViewData["School"] = School;
                    schoolid = tt;
                }
            }
            else
            {
                List<SelectListItem> School = SchoolHelper.GetSchoolList(school);
                ViewData["School"] = School;
                schoolid = school;
            }
            string nowbatch = SchoolHelper.GetCurrentBatch(schoolid);
            if(nowbatch=="该学校未开放任何批次")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "该学校未开放任何批次!" });
            }
            var q = (from f in sc.PracBatch where f.SchoolID == schoolid && f.PracBatchID == nowbatch select f).ToList();
            string[] OpenStatus = new string[q.Count];
            string[] ApplyStatus = new string[q.Count];

            for (int i = 0; i < q.Count; i++)
            {
                string praid = q[i].PracBatchID;
                string unitid = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:","");
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

        //企业申请开放操作--申请学校
        [BaseActionFilter]
        public ActionResult EntSubmitApplyToSchool(string schoolid, string praid)
        {
            string unitid = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
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
                url = "/Enterprise/EmployerManager/ApplyToTalentPool?school=" + schoolid;
            }
            return Redirect(url);
        }

        //企业取消申请开放操作--申请学校
        [BaseActionFilter]
        public ActionResult EntCancelApplyToSchool(string schoolid, string praid)
        {
            string unitid = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
            string url = string.Empty;
            T_SchoolPubToUnit ts = sc.SchoolPubToUnit.Find(praid, unitid);
            if(ts!=null)
            {
                sc.SchoolPubToUnit.Remove(ts);
                sc.SaveChanges();
            }
            url = "/Enterprise/EmployerManager/ApplyToTalentPool?school=" + schoolid;
            return Redirect(url);
        }

        //企业申请开放操作-申请平台
        public ActionResult EntSubmitApplyToPlat(string schoolid, string praid)
        {
            string unitid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
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
                url = "/Enterprise/EmployerManager/ApplyToTalentPool?school=" + schoolid;
            }
            return Redirect(url);
        }

        //人才库浏览
        [BaseActionFilter]
        public ActionResult ScanTheTalentPool(string scid,string y,string speid)
        {
            string unitid = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
            var q = (from f in sc.SchoolPubToUnit where f.UnitID == unitid && f.ApplyStatusID==2 select f.PracBatchID).ToList();
            if(q.Count==0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "该企业尚未通过任何一家学校的人才库浏览审批！！" });
            }
            List<SelectListItem> School = new List<SelectListItem>();//获取学校i
            List<SelectListItem> EntryYear = new List<SelectListItem>();//获取学校i
            List<SelectListItem> Specility = new List<SelectListItem>();//获取学校i

            string notrep = string.Empty;
            string notrep1 = string.Empty;
            for(int i=0;i<q.Count;i++)
            {
                string temp=q[i];
                var q1 = (from f in sc.PracBatch where f.PracBatchID == temp select f).ToList();
                if(q1.Count==0)
                {
                    continue;
                }
                if (q1[0].SchoolName != notrep && q1[0].SchoolID != notrep1)
                {
                    notrep = q1[0].SchoolName;
                    notrep1 = q1[0].SchoolID;
                    School.Add(new SelectListItem() { Text = q1[0].SchoolName, Value = q1[0].SchoolID, Selected = false });
                }
            }
            if (Request.Form["School"]!=null)
            {
                EntryYear = SchoolHelper.GetEntryYearList(Request.Form["School"], Request.Form["EntryYear"]);
                Specility = SchoolHelper.GetSpecialtyList(Request.Form["School"], Request.Form["Specialty"]);
            }
            else
            {
                EntryYear.Add(new SelectListItem() { Text = "请选择学校", Value = "请选择学校" });
                Specility.Add(new SelectListItem() { Text = "请选择学校", Value = "请选择学校" });
            }
            ViewData["School"] = School.Distinct();
            ViewData["EntryYear"] = EntryYear;
            ViewData["Specialty"] = Specility;

            if (Request.Form["School"] != null && Request.Form["EntryYear"] != null && Request.Form["Specialty"] != null)
            {
                string schoolid = Request.Form["School"];
                ViewBag.NowOpSchool = schoolid;
                string year = Request.Form["EntryYear"];
                ViewBag.NowOpYear = year;
                string spe = Request.Form["Specialty"];
                ViewBag.NowOpSpe = spe;
                string temp=year+spe;
                var q_stu = (from f in sc.Student where f.StuClass.Contains(temp) && f.UserID.Contains(schoolid) select f).ToList();

                Dictionary<string, List<int>> StuInfoList = new Dictionary<string, List<int>>();
                List<T_Student> StuList = new List<T_Student>();

                int allindex = q_stu.Count;
                //处理信息开闭
                for (int i = 0; i < allindex; i++)
                {
                    string practiceno = SchoolHelper.GetStuPracticeNo(q_stu[i].UserID);
                    if(practiceno!="未注册批次，数据出错!")
                    {
                        var q_checkpub = (from f in sc.StuInfoPub where f.PracticeNo == practiceno select f).ToList();
                        var q_checkpub1=(from f in q_checkpub select f.InfoTypeID).ToList();
                        StuInfoList.Add(q_stu[i].StuNo, q_checkpub1);
                        StuList.Add(q_stu[i]);
                    }
                }
                ViewBag.StuList = StuList;
                ViewBag.StuInfoList = StuInfoList;
                if(StuInfoList.Count==0)
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "该班级的所有学生都未注册到当前批次！" });
                }
            }
            else if (scid!=null)
            {
                string temp = y + speid;
                var q_stu = (from f in sc.Student where f.StuClass.Contains(temp) && f.UserID.Contains(scid) select f).ToList();
                ViewBag.StuList = q_stu;
            }
            else
            {
                ViewBag.NowOpSchool = "null";
                ViewBag.StuList = null;
            }

            //List<SelectListItem> Job_List = new List<SelectListItem>();//获取本公司提供的职位

            var q_job = (from f in ec.RecruitPosition where f.Ent_No == unitid select f).ToList();
            //for (int i = 0; i < q_job.Count;i++ )
            //{
                //Job_List.Add(new SelectListItem() { Text = q_job[i].PositionName, Value = q_job[i].PositionID });
            //}

            //ViewData["Job_List"] = Job_List;

            string Htmls = "";
            Htmls += "<select "+"class="+"dfinput"+" id="+"joblist"+" name="+"joblist"+">";
            for (var i = 0; i < q_job.Count; i++)
            {
                Htmls += "<option value=" + q_job[i].PositionID +">";
                Htmls += q_job[i].PositionName;
                Htmls += "</option>";
            }
            Htmls += "</select>";

            HtmlString aa = new HtmlString(Htmls);
            ViewBag.Htmls = aa;

            if (Request.Form["School"]!=null)
            {
                ViewBag.SchoolFoucs = Request.Form["School"];
                ViewBag.EntryYearFoucs = Request.Form["EntryYear"];
                ViewBag.SpecialtyFoucs = Request.Form["Specialty"];
            }
            return View();
        }

        //查看学生实习资料
        [BaseActionFilter]
        public ActionResult ScanStuPraInfo(string userid,string schoolid)
        {
            if(schoolid==null)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "不存在该学生，数据出错!" });
            }
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            //string praticeno=userid+prcbatchid;
            string temp = SchoolHelper.GetStuPracticeNo(userid);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "该学生未注册所选学校的当前批次，数据出错!" });
            }
            else
            {
                praticeno = temp;
            }

            //string[] ForbidList = new string[3] { "#_form_weekrec", "#_form_practicecase", "#_form_newrep" };
            //string ForbidList = "#_form_weekrec" + "!" + "#_form_practicecase" + "!" + "#_form_newrep";
            string ForbidFlag = "true";
            string url = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab1&StuPracNo=" + praticeno + "&ForbidFlag=" + ForbidFlag;
            //string hLink = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab1&StuPracNo=" + PracticeNo;
            return Redirect(url);
        }

        //查看学生应聘简历
        [BaseActionFilter]
        public ActionResult ScanStuResumeInfo(string userid,string schoolid)
        {
            if (schoolid == null)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "不存在该学生，数据出错!" });
            }
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            //string praticeno=userid+prcbatchid;
            string temp = SchoolHelper.GetStuPracticeNo(userid);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "该学生未注册所选学校的当前批次，数据出错!" });
            }
            else
            {
                praticeno = temp;
            }
            var q_file = sc.Resume.Where(a => a.PracticeNo == praticeno).Select(a => a.ResumeURL).FirstOrDefault();
            if(q_file==null)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "该学生未上传或未设置默认简历！" });
            }
            else
            {
                if(System.IO.File.Exists(q_file))
                {
                    string filePath = Server.MapPath(q_file);//路径
                    return File(filePath, "application/ms-word", "简历预览.doc"); //welcome.txt是客户端保存的名字
                }
                else
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "文件已丢失！" });
                }
            }
            //string url = "/School/S_EmployeManager/MyResume?focus=tab1&StuPracNo=" + praticeno;
            //string hLink = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab1&StuPracNo=" + PracticeNo;
            //return Redirect(url);
        }

        [HttpPost]
        public JsonResult SendYQ()
        {
            string Nowopstu = Request["nowopstu"].ToString();
            string Posid = Request["posid"].ToString();
            string Nowopschool = Request["nowopschool"].ToString();
            string Nowopyear = Request["nowopyear"].ToString();
            string Nowopspe = Request["nowopspe"].ToString();
            string Ttime = Request["ttime"].ToString();

            string result = string.Empty;
            int Count = 0;

            string prcbatchid = SchoolHelper.GetCurrentBatch(Nowopschool);
            string entno = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
            //string praticeno = nowopstu + prcbatchid;
            string temp = SchoolHelper.GetStuPracticeNo(Nowopstu);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                result="未注册批次，数据出错!";
            }
            else
            {
                praticeno = temp;
            }
            T_JobWanted check = sc.JobWanted.Find(praticeno, entno, Posid);
            if (check != null)
            {
                result="已经向该学生发送邀请或该学生已申请这个岗位！";
            }
            else
            {
                var q_resume = (from f in sc.Resume where f.PracticeNo == praticeno select f.ResumeURL).FirstOrDefault();

                T_JobWanted tj = new T_JobWanted();
                tj.PracticeNo = praticeno;
                tj.Ent_No = entno;
                tj.PositionID = Posid;
                if (q_resume != null)
                {
                    tj.ResumeURL = q_resume;
                }
                tj.Flag = "企业邀请";
                tj.ReviewTime = DateTimeFormat.ConvertDateTimeInt(Convert.ToDateTime(Ttime));
                tj.JobStatus = 1;
                sc.JobWanted.Add(tj);
                Count=sc.SaveChanges();
            }
            return Json(new { count = Count, Pos = result });
        }

        [HttpPost]
        public JsonResult Sendoffer()
        {
            string Nowopstu = Request["nowopstu"].ToString();
            string Posid = Request["posid"].ToString();
            string Nowopschool = Request["nowopschool"].ToString();
            string Nowopyear = Request["nowopyear"].ToString();
            string Nowopspe = Request["nowopspe"].ToString();

            string result = string.Empty;
            int Count = 0;

            string prcbatchid = SchoolHelper.GetCurrentBatch(Nowopschool);
            string entno = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
            //string praticeno = nowopstu + prcbatchid;
            string temp = SchoolHelper.GetStuPracticeNo(Nowopstu);
            string praticeno = string.Empty;
            if (temp == "未注册批次，数据出错!")
            {
                result = temp;
            }
            else
            {
                praticeno = temp;
            }
            T_JobWanted check = sc.JobWanted.Find(praticeno, entno, Posid);
            if (check != null)
            {
                result="已经向该学生发送邀请或该学生已申请这个岗位！";
            }
            else
            {
                var q_resume = (from f in sc.Resume where f.PracticeNo == praticeno select f.ResumeURL).FirstOrDefault();

                T_JobWanted tj = new T_JobWanted();
                tj.PracticeNo = praticeno;
                tj.Ent_No = entno;
                tj.PositionID = Posid;
                if (q_resume != null)
                {
                    tj.ResumeURL = q_resume;
                }
                tj.Flag = "企业邀请";
                //tj.ReviewTime = DateTimeFormat.ConvertDateTimeInt(Convert.ToDateTime(ttime));
                tj.ReviewTime = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
                tj.ReviewRecord = "企业免试通过";
                tj.ReviewScore = 100;
                tj.JobStatus = 4;//直接通过面试
                sc.JobWanted.Add(tj);
                Count=sc.SaveChanges();
            }
            return Json(new { count = Count, Pos = result });
        }

        //面试与OFFER管理
        [BaseActionFilter]
        public ActionResult MeetingAndOffer()
        {
            string entno = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
            //var q_history = (from f in sc.JobWanted where f.Ent_No == entno && f.Flag == "企业邀请" select f).ToList();

            var q_position = (from f in ec.RecruitPosition where f.Ent_No == entno select f).ToList();

            //for (int i = 0; i < q_position.Count;i++ )
            //{
                //string posid = q_position[i].PositionID;
                //var q_count=(from )
            //}
            string[] selftou = new string[q_position.Count];//自投简历份数
            string[] meetnum=new string[q_position.Count];//面试人数
            string[] invitemeetnum=new string[q_position.Count];//面试邀请人数
            string[] offernum = new string[q_position.Count];//发送offer人数
            string[] hidenum=new string[q_position.Count];//录取人数
            

            for(int i=0;i<q_position.Count;i++)
            {
                string temp=q_position[i].PositionID;
                var q_for_all = (from f in sc.JobWanted where f.Ent_No == entno && f.PositionID == temp select f).ToList();
                var q_meetnum = (from f in q_for_all where f.JobStatus >= 2 select f).ToList();
                meetnum[i] = q_meetnum.Count.ToString();
                var q_invitetnum = (from f in q_for_all where f.Flag == "企业邀请" select f).ToList();
                invitemeetnum[i] = q_invitetnum.Count.ToString();
                var q_hidenum = (from f in q_for_all where f.JobStatus == 5 select f).ToList();
                hidenum[i] = q_hidenum.Count.ToString();
                var q_selftou = (from f in q_for_all where f.Flag == "自投" select f).ToList();
                selftou[i] = q_selftou.Count.ToString();
                var q_offernum=(from f in q_for_all where f.Flag == "企业邀请" && f.JobStatus>=4 select f).ToList();
                offernum[i] = q_offernum.Count.ToString();
                
            }
            

            ViewBag.PositionList = q_position;
            ViewBag.Meetnum = meetnum;
            ViewBag.Invitemeetnum = invitemeetnum;
            ViewBag.Hidenum = hidenum;
            ViewBag.Selftou = selftou;
            ViewBag.Offernum = offernum;
            return View();
        }

        //面试与OFFER管理详情
        [BaseActionFilter]
        public ActionResult MeetingAndOfferDetails(string posid)
        {
            string entno = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
            var q = (from f in sc.JobWanted where f.PositionID == posid && f.Ent_No == entno select f).ToList();
            List<T_Student> StudentList = new List<T_Student>();
            Dictionary<string, List<int>> StuInfoList = new Dictionary<string, List<int>>();
            //处理信息开闭
            for (int i = 0; i < q.Count; i++)
            {
                string practiceno = q[i].PracticeNo;

                var q_stu = (from f in sc.StuBatchReg where f.PracticeNo == practiceno select f).ToList();
                if(q_stu.Count!=0)
                {

                    string userid = q_stu[0].UserID;
                    var q_stu2 = (from f in sc.Student where f.UserID == userid select f).ToList();
                    if(q_stu2.Count!=0)
                    {
                        StudentList.Add(q_stu2[0]);
                        var q_checkpub = (from f in sc.StuInfoPub where f.PracticeNo == practiceno select f).ToList();
                        var q_checkpub1 = (from f in q_checkpub select f.InfoTypeID).ToList();
                        StuInfoList.Add(q_stu2[0].StuNo, q_checkpub1);
                        
                    }
                }
                else
                {
                    continue;
                }
            }

            ViewBag.JobList = q;
            ViewBag.StudentList = StudentList;
            ViewBag.StuInfoList = StuInfoList;
            if (StuInfoList.Count == 0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "该班级的所有学生都未注册到当前批次！" });
            }
            if (q.Count != StudentList.Count)
            {
                ViewBag.ErrorMsg = "部分学生数据异常，请联系高校管理员查看该批次的数据！";
            }

            if (StudentList.Count==0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "该批次的所有申请记录出现异常，请联系高校管理员查看该批次的数据！" });
            }
            return View();
        }

        //面试管理
        [BaseActionFilter]
        public ActionResult MeetingMaOp(string practiceno, string entno, string posid)
        {
            ViewBag.Practiceno = practiceno;
            ViewBag.Entno = entno;
            ViewBag.Posid = posid;
            T_JobWanted tj = sc.JobWanted.Find(practiceno, entno, posid);
            ViewBag.Detail = tj;

            string NowTime = DateTime.Now.ToString();
            ViewBag.NowTime = NowTime;
            return View();
        }

        //面试管理信息保存
        [BaseActionFilter]
        public ActionResult SaveMeetingMaOp()
        {
            string Practiceno = Request.Form["Practiceno"];
            string Entno = Request.Form["Entno"];
            string Posid = Request.Form["Posid"];
            T_JobWanted tj = sc.JobWanted.Find(Practiceno, Entno, Posid);
            string meettime = Request.Form["meettime"];
            string meetrec = Request.Form["meetrec"];
            string meetscore = Request.Form["meetscore"];
            string meetresult = Request.Form["meetresult"];
            if (meettime != "")
            {
                tj.ReviewTime = DateTimeFormat.ConvertDateTimeInt(Convert.ToDateTime(meettime));
                tj.JobStatus = 1;
            }
            tj.ReviewRecord = meetrec;
            if (meetscore != "")
            {
                tj.ReviewScore = Convert.ToInt32(meetscore);
            }
            if (meetresult == "get")
            {
                tj.JobStatus = 4;
            }
            else if (meetresult == "unget")
            {
                tj.JobStatus = 3;
            }
            else
            {

            }
            sc.SaveChanges();
            string url = "/Enterprise/EmployerManager/MeetingAndOfferDetails?posid=" + Posid;
            return Redirect(url);
        }

        //招聘岗位管理
        [BaseActionFilter]
        public ActionResult AddEmployePost()
        {
            string unitid = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
            var q = (from f in ec.RecruitPosition where f.Ent_No == unitid select f).ToList();
            ViewBag.Poistion = q;

            List<SelectListItem> PoistionList = new List<SelectListItem>();//获取岗位
            var q1 = (from f in ctc.C_Position select f).ToList();
            for (int i = 0; i < q1.Count; i++)
            {
                PoistionList.Add(new SelectListItem() { Text = q1[i].PositionName, Value = q1[i].PositionID });
            }

            ViewData["PoistionList"] = PoistionList;
            return View();
        }

        [BaseActionFilter]
        public ActionResult AddNewPost()
        {
            string unitid = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit.Replace("Ent_No:", "");
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            string posid = Request.Form["PoistionList"];
            
            string posdesc = Request.Form["posdesc"];
            string posreq = Request.Form["posreq"];
            string posnum = Request.Form["posnum"];
            string postday = Request.Form["postday"];
            string pay = Request.Form["pay"];
            string isac = Request.Form["isac"];
            T_RecruitPosition tp = new T_RecruitPosition();
            tp.Ent_No = unitid;
            tp.PositionID = posid;
            tp.PosDesc = posdesc;
            tp.PosRequirement = posdesc;
            tp.PosQuantity = Convert.ToInt32(posnum);
            tp.PubDate = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
            int temp = Convert.ToInt32(postday);
            tp.PosExpireDate = DateTimeFormat.ConvertDateTimeInt(DateTime.Now.AddDays(temp));
            tp.IsActive = Convert.ToInt32(isac);
            tp.PosPay = pay;
            tp.Publisher = userid;
            ec.RecruitPosition.AddOrUpdate(tp);
            ec.SaveChanges();
            string url = "/Enterprise/EmployerManager/AddEmployePost";
            return Redirect(url);
        }

        [BaseActionFilter]
        public ActionResult DeletePost(string entid,string posid)
        {
            T_RecruitPosition tp = ec.RecruitPosition.Find(entid, posid);
            if(tp!=null)
            {
                ec.RecruitPosition.Remove(tp);
                ec.SaveChanges();
            }
            string url = "/Enterprise/EmployerManager/AddEmployePost";
            return Redirect(url);
        }

        //关闭岗位
        [HttpPost]
        public JsonResult Close_RPos()
        {
            string Entno = Request["entno"].ToString();
            string Posid = Request["posid"].ToString();

            string result = string.Empty;
            int Count = 0;

            T_RecruitPosition tp = ec.RecruitPosition.Find(Entno, Posid);
            if (tp != null)
            {
                if(tp.IsActive==1)
                {
                    tp.IsActive=0;
                }
                else
                {
                    tp.IsActive=1;
                }
                Count = ec.SaveChanges();
            }


            return Json(new { count = Count, Pos = result });
        }



        //控制器里面：   年级专业班级联动

        [HttpPost]
        public JsonResult GetYearList()//年级列表
        {
            string schoolid = Request["SchoolCode"].ToString();
            

            List<C_Specialty> q_cate = new List<C_Specialty>();

            var q = (from f in sc.C_Specialty where f.SchoolID == schoolid select f).ToList();
            string nowyear = q[0].EntryYear.ToString();
            q_cate.Add(q[0]);
            for (int i = 1; i < q.Count;i++ )
            {
                if (q[i].EntryYear_S != nowyear)
                {
                    q_cate.Add(q[i]);
                    nowyear = q[i].EntryYear_S;
                }        
            }

            return Json(new { count = q_cate.Count, Pos = q_cate });
        }

        [HttpPost]
        public JsonResult GetSpeList()//专业列表
        {
            string EntryYear = Request["EntryYearCode"].ToString();
            string Schoolid = Request["SchoolCode"].ToString();
            //string schoolid = "10385";
            int Year = Convert.ToInt32(EntryYear);
            var Spe = (from p in sc.C_Specialty where p.EntryYear == Year && p.SchoolID == Schoolid select new { p.SpeNo, p.SpeName }).ToList();

            return Json(new { count = Spe.Count, Pos = Spe });
        }

    }
}
