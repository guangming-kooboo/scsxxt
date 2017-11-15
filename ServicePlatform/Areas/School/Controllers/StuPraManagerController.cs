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
    public class StuPraManagerController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();

        [BaseActionFilter]
        public ActionResult ChangeStuPraPos()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            ViewBag.SchoolID = schoolid;

            //获取年级
            List<SelectListItem> EntryYear = SchoolHelper.GetEntryYearList(schoolid);
            ViewData["EntryYear"] = EntryYear;

            //获取专业
            List<SelectListItem> Specialty = SchoolHelper.GetSpecialtyList(schoolid);
            ViewData["Specialty"] = Specialty;

            //获取班级
            List<SelectListItem> Class = SchoolHelper.GetClassList(schoolid);
            ViewData["Class"] = Class;

            //制作表格
            //List<T_PracticeVolunteer> tp = new List<T_PracticeVolunteer>();
            List<T_PracticeVolunteer> tpp = new List<T_PracticeVolunteer>();

            if (Request["Class"] == null)
            {
                return View();
            }
            else
            {
                string EntryYear1 = Request["EntryYear"].ToString();
                string SpeNo = Request["Specialty"].ToString();
                string ClassID = Request["Class"].ToString();

                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);

                var q_s = (from f in sc.Student where f.StuClass == ClassID select f).ToList();
                string[] StuNo = new string[q_s.Count];
                string[] UserID = new string[q_s.Count];
                string[] StuName = new string[q_s.Count];
                string[] EntName = new string[q_s.Count];
                string[] PosName = new string[q_s.Count];
                int index = 0;
                for (int i = 0; i < q_s.Count; i++)
                {
                    StuNo[index] = q_s[i].StuNo;
                    StuName[index] = q_s[i].StuName;
                    UserID[index] = q_s[i].UserID;
                    string userid = q_s[i].UserID;
                    var q_career = (from f in sc.StuBatchReg where f.UserID == userid select f.CareerStatusID).ToList();
                    if (q_career==null||q_career.Count==0)
                    {
                        EntName[index] = "该学生未注册批次";
                        PosName[index] = "该学生未注册批次";
                    }
                    else
                    {
                        if(q_career[0]<13)
                        {
                            EntName[index] = "该学生正在招聘";
                            PosName[index] = "该学生正在招聘";
                        }
                        else if (q_career[0] == 13)
                        {
                            string prcticeno = userid + prcbatchid;
                            var q = (from f in sc.PracticeVolunteer where f.PracticeNo == prcticeno && f.VolunteerStatus == 5 && f.PreVolStatus == "1" select f).ToList();
                            if(q.Count!=0)
                            {
                                EntName[index] = q[0].EntName;
                                PosName[index] = q[0].PosName;
                            }
                            else
                            {
                                EntName[index] = "该学生数据异常";
                                PosName[index] = "该学生数据异常";
                            }

                        }
                        else
                        {
                            EntName[index] = "该学生实习结束";
                            PosName[index] = "该学生实习结束";
                        }

                    }
                    index++;
                }
                ViewBag.StuNo = StuNo;
                ViewBag.StuName = StuName;
                ViewBag.EntName = EntName;
                ViewBag.PosName = PosName;
                ViewBag.UserID = UserID;
                ViewBag.LoopNum = q_s.Count;
                //ViewBag.StudentVol = tpp;
            }

            return View();
        }


        //更换岗位页面
        [BaseActionFilter]
        public ActionResult ChoicePos(string userid)
        {
            if (userid == null)
            {
                userid = Request.Form["UserID"];
            }
            var q_name = (from f in sc.Student where f.UserID == userid select f.StuName).ToList();
            ViewBag.StuName = q_name[0];
            ViewBag.UserID = userid;
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            //获取企业
            List<SelectListItem> Enterprise = SchoolHelper.GetEntList(prcbatchid);
            ViewData["Enterprise"] = Enterprise;

            //获取岗位
            string entno = Enterprise[0].Value;
            string entprano = entno + "-" + prcbatchid;
            List<SelectListItem> Position = SchoolHelper.GetPosList(entprano);
            ViewData["Position"] = Position;

            return View();

        }

        //更换岗位操作
        [BaseActionFilter]
        public ActionResult SaveFinalPosToStu()
        {
            
            string userid = Request["UserID"].ToString();
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string pricticeno=userid+prcbatchid;
            var q = (from f in sc.PracticeVolunteer where f.PracticeNo == pricticeno&& f.PreVolStatus=="1"&&f.VolunteerStatus==5 select f).ToList();
            if(q==null||q.Count==0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "数据出错！" });
            }
            else
            {
                string EntNo = Request["Enterprise"].ToString();
                string PosNo = Request["Position"].ToString();
                if(PosNo=="全部")
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "岗位选择有误！" });
                }
                T_PracticeVolunteer test = sc.PracticeVolunteer.Find(pricticeno, EntNo + "-" + prcbatchid, PosNo);
                if (test != null)
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "选择的岗位存在于学生的某个志愿当中，更换失败！" });
                }
                T_PracticeVolunteer tp = q[0];
                T_PracticeVolunteer newtp = new T_PracticeVolunteer();
                newtp.EntPracNo = EntNo + "-" + prcbatchid;
                newtp.PosID = PosNo;
                newtp.Interviewee = tp.Interviewee;
                newtp.InterviewRecord = tp.Interviewee;
                newtp.InterviewRecord = tp.InterviewRecord;
                newtp.InterviewScore = tp.InterviewScore;
                newtp.InterviewTime = tp.InterviewTime;
                newtp.PosSequence = tp.PosSequence;
                newtp.PracticeNo = tp.PracticeNo;
                newtp.PreVolStatus = tp.PreVolStatus;
                newtp.VolunteerSequence = tp.VolunteerSequence;
                newtp.VolunteerStatus = tp.VolunteerStatus;
                sc.PracticeVolunteer.Add(newtp);
                sc.PracticeVolunteer.Remove(tp);
                sc.SaveChanges();
            }
            string url = "/School/StuPraManager/ChangeStuPraPos?userid=" + userid;
            return Content("<script>alert('更换岗位成功！');window.location = '" + url + "';</script>");//返回上一页并刷新
        }

        //学生对企业评汇总
        [BaseActionFilter]
        public ActionResult ScanStuEvaEnt()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string practicebatchid = SchoolHelper.GetCurrentBatch(schoolid);

            if (Request["Enterprise"] == null)
            {
                //获取企业
                List<SelectListItem> Enterprise = SchoolHelper.GetEntList(practicebatchid);
                ViewData["Enterprise"] = Enterprise;

                string temp = Enterprise[0].Value;
                var q_temp = (from f in ec.T_EntBatchReg where f.Ent_No == temp && f.PracBatchID == practicebatchid select f.EntPracNo).ToList();
                string temp_entpracno = q_temp[0];

                //获取职位列表
                List<SelectListItem> Position = SchoolHelper.GetPosList(temp_entpracno);
                ViewData["Position"] = Position;

                return View();
            }
            else
            {
                string Entno = Request["Enterprise"].ToString();
                string Posno = Request["Position"].ToString();
                //获取企业
                List<SelectListItem> Enterprise = SchoolHelper.GetEntList(practicebatchid, Entno);
                ViewData["Enterprise"] = Enterprise;

                var q_temp = (from f in ec.T_EntBatchReg where f.Ent_No == Entno && f.PracBatchID == practicebatchid select f.EntPracNo).ToList();
                string temp_entpracno = q_temp[0];

                //获取职位列表
                List<SelectListItem> Position = SchoolHelper.GetPosList(temp_entpracno, Posno);
                ViewData["Position"] = Position;

                #region 平均分计算
                //企业评价平均分
                var q_avg_ent =
                    (from p in sc.StuBatchReg
                     from f in sc.PracticeVolunteer
                     where p.PracticeNo == f.PracticeNo && f.EntPracNo == temp_entpracno && f.PreVolStatus == "1" && f.VolunteerStatus == 5 &&(p.StuEvaEntScore!=0.0||p.StuEvaEntScore!=null)
                     group p by f.EntPracNo into g
                     select new
                     {
                         g.Key,
                         AveragePrice = g.Average(p => p.StuEvaEntScore)
                     }).ToList();
                if (q_avg_ent.Count != 0)
                {
                    ViewBag.Extra_ForE = "企业评价平均分" + Math.Round(q_avg_ent[0].AveragePrice, 2); ;
                }
                else
                {
                    ViewBag.Extra_ForE = "暂无数据";
                }


                //岗位评价平均分
                var q_avg_pos =
                    (from p in sc.StuBatchReg
                     from f in sc.PracticeVolunteer
                     where p.PracticeNo == f.PracticeNo && f.PosID == Posno && f.PreVolStatus=="1" && f.VolunteerStatus==5&&(p.StuEvaSchoolScore!=0.0||p.StuEvaSchoolScore!=null)
                     group p by f.PosID into g
                     select new
                     {
                         g.Key,
                         AveragePrice = g.Average(p => p.StuEvaEntScore)
                     }).ToList();
                if (q_avg_pos.Count != 0)
                {
                    ViewBag.Extra_ForP = "岗位评价平均分" + Math.Round(q_avg_pos[0].AveragePrice, 2);
                }
                else
                {
                    ViewBag.Extra_ForP = "暂无数据";
                }


                #endregion

                //获取学校当前批次
                //string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);

                //var q_s = (from Stu in sc.Student from PracStuInBatch in sc.StuBatchReg where Stu.StuClass == ClassID && Stu.UserID == PracStuInBatch.UserID && PracStuInBatch.PracBatchID == prcbatchid select new { Stu.StuNo, Stu.StuName, PracStuInBatch.PracticeNo, Stu.UserID }).ToList();
                var q_s = (from p in sc.PracticeVolunteer from q in sc.StuBatchReg from k in sc.Student where p.PracticeNo == q.PracticeNo && q.UserID == k.UserID && p.PreVolStatus == "1" && p.EntPracNo == temp_entpracno && p.PosID == Posno &&p.VolunteerStatus==5 select new { k.StuNo, k.StuName, p.PracticeNo, k.UserID,q.ReviewScore }).ToList();
                string[] StuNo = new string[q_s.Count];
                string[] StuName = new string[q_s.Count];
                string[] StuPracNo = new string[q_s.Count];
                string[] StuID = new string[q_s.Count];
                string[] StuEvaEntScore = new string[q_s.Count];
                string[] StuReviewScore = new string[q_s.Count];

                for (int i = 0; i < q_s.Count; i++)
                {
                    StuNo[i] = q_s[i].StuNo;
                    StuName[i] = q_s[i].StuName;
                    StuPracNo[i] = q_s[i].PracticeNo;
                    StuID[i] = q_s[i].UserID;
                    double temp = SchoolHelper.GetStuEvaEntSchoolScore(q_s[i].PracticeNo, "Ent");
                    if (temp == 0.0)
                    {
                        StuEvaEntScore[i] = "未评价";
                    }
                    else
                    {
                        StuEvaEntScore[i] = temp.ToString();
                    }
                    StuReviewScore[i] = q_s[i].ReviewScore.ToString() ;
                }
                ViewBag.StuNo = StuNo;
                ViewBag.StuName = StuName;
                ViewBag.StuPracNo = StuPracNo;
                ViewBag.StuID = StuID;
                ViewBag.StuEvaEntScore = StuEvaEntScore;
                ViewBag.StuReviewScore = StuReviewScore;
                ViewBag.DataCount = q_s.Count;              

                return View();
            }
        }

        //学生对学校评价汇总
        [BaseActionFilter]
        public ActionResult ScanStuEvaSchool()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            ViewBag.SchoolID = schoolid;

            if (Request["Class"] == null)
            {
                //获取年级
                List<SelectListItem> EntryYear = SchoolHelper.GetEntryYearList(schoolid);
                ViewData["EntryYear"] = EntryYear;

                //获取专业
                List<SelectListItem> Specialty = SchoolHelper.GetSpecialtyList(schoolid);
                ViewData["Specialty"] = Specialty;

                //获取班级
                List<SelectListItem> Class = SchoolHelper.GetClassList(schoolid);
                ViewData["Class"] = Class;
                return View();
            }
            else
            {
                string EntryYear1 = Request["EntryYear"].ToString();
                int year_temp = Convert.ToInt32(EntryYear1);
                string SpeNo = Request["Specialty"].ToString();
                string ClassID = Request["Class"].ToString();
                //获取年级
                List<SelectListItem> EntryYear = SchoolHelper.GetEntryYearList(schoolid, EntryYear1);
                ViewData["EntryYear"] = EntryYear;

                //获取专业
                List<SelectListItem> Specialty = SchoolHelper.GetSpecialtyList(schoolid,year_temp,SpeNo);
                ViewData["Specialty"] = Specialty;

                //获取班级
                List<SelectListItem> Class = SchoolHelper.GetClassList(schoolid,SpeNo, ClassID);
                ViewData["Class"] = Class;
                

                #region 平均分计算
                //班级评价平均分
                var q_avg_class =
                    (from p in sc.StuBatchReg
                     from f in sc.Student
                     from k in sc.T_Class
                     where p.UserID == f.UserID && k.ClassID == f.StuClass && f.StuClass == ClassID && (p.StuEvaSchoolScore != 0.0 || p.StuEvaSchoolScore != null)
                     group p by f.StuClass into g
                     select new
                     {
                         g.Key,
                         AveragePrice = g.Average(p => p.StuEvaSchoolScore)
                     }).ToList();
                if (q_avg_class.Count!=0)
                {
                    ViewBag.Extra_ForC = "班级评价平均分" + Math.Round(q_avg_class[0].AveragePrice, 2);
                }
                else
                {
                    ViewBag.Extra_ForC = "暂无数据";
                }


                //专业评价平均分
                var q_avg_spe =
                    (from p in sc.StuBatchReg
                    from f in sc.Student
                    from k in sc.T_Class
                     where p.UserID == f.UserID && f.StuClass == k.ClassID && k.SpeNo == SpeNo && k.SchoolID == schoolid && (p.StuEvaSchoolScore != 0.0 || p.StuEvaSchoolScore != null)
                    group p by k.SpeNo into g
                    select new
                    {
                        g.Key,
                        AveragePrice = g.Average(p => p.StuEvaSchoolScore)
                    }).ToList();
                if (q_avg_spe.Count != 0)
                {
                    ViewBag.Extra_ForS = "专业评价平均分" + Math.Round(q_avg_spe[0].AveragePrice, 2);
                }
                else
                {
                    ViewBag.Extra_ForS = "暂无数据";
                }
                

                //年级评价平均分
                var q_avg_year =
                    (from p in sc.StuBatchReg
                    from f in sc.Student
                    from k in sc.T_Class
                    where p.UserID == f.UserID && f.StuClass == k.ClassID && k.EntryYear == year_temp && k.SchoolID==schoolid && (p.StuEvaSchoolScore != 0.0 || p.StuEvaSchoolScore != null)
                    group p by k.EntryYear into g
                    select new
                    {
                        g.Key,
                        AveragePrice = g.Average(p => p.StuEvaSchoolScore)
                    }).ToList();         
                if (q_avg_year.Count != 0)
                {
                    ViewBag.Extra_ForE = "年级评价平均分" + Math.Round(q_avg_year[0].AveragePrice, 2);
                }
                else
                {
                    ViewBag.Extra_ForE = "数据出错";
                }
                #endregion

                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
                var q_s = (from Stu in sc.Student from PracStuInBatch in sc.StuBatchReg where Stu.StuClass == ClassID && Stu.UserID == PracStuInBatch.UserID && PracStuInBatch.PracBatchID == prcbatchid select new { Stu.StuNo, Stu.StuName, PracStuInBatch.PracticeNo, Stu.UserID, PracStuInBatch.ReviewScore}).ToList();
                string[] StuNo = new string[q_s.Count];
                string[] StuName = new string[q_s.Count];
                string[] StuPracNo = new string[q_s.Count];
                string[] StuID = new string[q_s.Count];
                string[] StuEvaSchoolScore = new string[q_s.Count];
                string[] ReviewScore = new string[q_s.Count];

                for (int i = 0; i < q_s.Count; i++)
                {
                    StuNo[i] = q_s[i].StuNo;
                    StuName[i] = q_s[i].StuName;
                    StuPracNo[i] = q_s[i].PracticeNo;
                    StuID[i] = q_s[i].UserID;
                    double temp1 = SchoolHelper.GetStuEvaEntSchoolScore(q_s[i].PracticeNo, "School");
                    if (temp1 == 0.0)
                    {
                        StuEvaSchoolScore[i] = "未评价";
                    }
                    else
                    {
                        StuEvaSchoolScore[i] = temp1.ToString();
                    }
                    ReviewScore[i] = q_s[i].ReviewScore.ToString();
                }
                ViewBag.StuNo = StuNo;
                ViewBag.StuName = StuName;
                ViewBag.StuPracNo = StuPracNo;
                ViewBag.StuID = StuID;
                ViewBag.StuEvaSchoolScore = StuEvaSchoolScore;
                ViewBag.ReviewScore = ReviewScore;
                ViewBag.DataCount = q_s.Count;
                return View();
            }
        }

        //企业查看学生对企业评汇总
        [BaseActionFilter]
        public ActionResult EntScanStuEvaEnt()
        {
            string entno = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit;
            var q_condition = (from f in ec.T_EntBatchReg where f.Ent_No == entno select f).ToList();
            List<string> Condition = (from f in q_condition select f.SchoolID).ToList();

            if (Request["School"] == null)
            {
                var q_onebacth = (from f in ec.T_EntBatchReg where f.Ent_No == entno select f.EntPracNo).ToList();
                if(q_onebacth.Count==0)
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "找不到当前企业的任何一个实习批次！" });
                }

                //获取学校
                List<SelectListItem> School = SchoolHelper.GetSchoolList(Condition,"");
                ViewData["School"] = School;
                //获取批次
                string temp_sid = School.FirstOrDefault().Value;
                List<SelectListItem> BatchList = SchoolHelper.GetEntBatchList(entno, temp_sid);
                ViewData["BatchList"] = BatchList;
                //获取职位列表
                List<SelectListItem> Position = SchoolHelper.GetPosList(q_onebacth[0]);
                ViewData["Position"] = Position;

                return View();
            }
            else
            {
                string sid = Request.Form["School"].ToString();
                string batchid = Request.Form["BatchList"].ToString();
                string posid = Request.Form["Position"].ToString();
                string practicebatchid = SchoolHelper.GetCurrentBatch(sid);
                var q_temp = (from f in ec.T_EntBatchReg where f.Ent_No == entno && f.PracBatchID == practicebatchid select f.EntPracNo).ToList();
                string entpracno = q_temp[0];

                //获取学校
                List<SelectListItem> School = SchoolHelper.GetSchoolList(Condition,sid);
                ViewData["School"] = School;
                //获取批次
                string temp_sid = School.FirstOrDefault().Value;
                List<SelectListItem> BatchList = SchoolHelper.GetEntBatchList(entno,temp_sid, batchid);
                ViewData["BatchList"] = BatchList;
                //获取职位列表
                List<SelectListItem> Position = SchoolHelper.GetPosList(entpracno, posid);
                ViewData["Position"] = Position;

                #region 平均分计算
                //学校评价平均分
                var q_avg_ent =
                    (from p in sc.StuBatchReg
                     from f in sc.PracticeVolunteer
                     from s in sc.Student
                     from c in sc.T_Class
                     where s.StuClass==c.ClassID && s.UserID==p.UserID && p.PracticeNo == f.PracticeNo && f.EntPracNo == entpracno && f.PreVolStatus == "1" && f.VolunteerStatus == 5 && (p.StuEvaEntScore != 0.0 || p.StuEvaEntScore != null) && c.SchoolID==sid
                     group p by f.EntPracNo into g
                     select new
                     {
                         g.Key,
                         AveragePrice = g.Average(p => p.StuEvaEntScore)
                     }).ToList();
                if (q_avg_ent.Count != 0)
                {
                    ViewBag.Extra_ForS = "学校评价平均分" + Math.Round(q_avg_ent[0].AveragePrice, 2);
                    ViewBag.Extra_ForB = "批次评价平均分" + Math.Round(q_avg_ent[0].AveragePrice, 2);
                }
                else
                {
                    ViewBag.Extra_ForS = "暂无数据";
                    ViewBag.Extra_ForB = "暂无数据";
                }


                //岗位评价平均分
                var q_avg_pos =
                    (from p in sc.StuBatchReg
                     from f in sc.PracticeVolunteer
                     where f.EntPracNo==entpracno &&p.PracticeNo == f.PracticeNo && f.PosID == posid && f.PreVolStatus == "1" && f.VolunteerStatus == 5 && (p.StuEvaSchoolScore != 0.0 || p.StuEvaSchoolScore != null) && p.PracBatchID == practicebatchid 
                     group p by f.PosID into g
                     select new
                     {
                         g.Key,
                         AveragePrice = g.Average(p => p.StuEvaEntScore)
                     }).ToList();
                if (q_avg_pos.Count != 0)
                {
                    ViewBag.Extra_ForP = "岗位评价平均分" + Math.Round(q_avg_pos[0].AveragePrice, 2);
                }
                else
                {
                    ViewBag.Extra_ForP = "暂无数据";
                }


                #endregion

                Dictionary<string, List<int>> StuInfoList = new Dictionary<string, List<int>>();

                var q_s = (from p in sc.PracticeVolunteer from q in sc.StuBatchReg from k in sc.Student where p.PracticeNo == q.PracticeNo && q.UserID == k.UserID && p.PreVolStatus == "1" && p.EntPracNo == entpracno && p.PosID == posid && p.VolunteerStatus == 5 select new { k.StuNo, k.StuName, p.PracticeNo, k.UserID, q.ReviewScore }).ToList();
                string[] StuNo = new string[q_s.Count];
                string[] StuName = new string[q_s.Count];
                string[] StuPracNo = new string[q_s.Count];
                string[] StuID = new string[q_s.Count];
                string[] StuEvaEntScore = new string[q_s.Count];
                string[] StuReviewScore = new string[q_s.Count];

                for (int i = 0; i < q_s.Count; i++)
                {
                    #region 处理信息开闭
                    string practiceno = SchoolHelper.GetStuPracticeNo(q_s[i].UserID);
                    if (practiceno != "未注册批次，数据出错!")
                    {
                        var q_checkpub = (from f in sc.StuInfoPub where f.PracticeNo == practiceno select f).ToList();
                        var q_checkpub1 = (from f in q_checkpub select f.InfoTypeID).ToList();
                        StuInfoList.Add(q_s[i].StuNo, q_checkpub1);
                    }
                    else
                    {
                        q_s.Remove(q_s[i]);
                    }
                    #endregion

                    StuNo[i] = q_s[i].StuNo;
                    StuName[i] = q_s[i].StuName;
                    StuPracNo[i] = q_s[i].PracticeNo;
                    StuID[i] = q_s[i].UserID;
                    double temp = SchoolHelper.GetStuEvaEntSchoolScore(q_s[i].PracticeNo, "Ent");
                    if (temp == 0.0)
                    {
                        StuEvaEntScore[i] = "未评价";
                    }
                    else
                    {
                        StuEvaEntScore[i] = temp.ToString();
                    }
                    StuReviewScore[i] = q_s[i].ReviewScore.ToString();
                }
                ViewBag.StuNo = StuNo;
                ViewBag.StuName = StuName;
                ViewBag.StuPracNo = StuPracNo;
                ViewBag.StuID = StuID;
                ViewBag.StuEvaEntScore = StuEvaEntScore;
                ViewBag.StuReviewScore = StuReviewScore;
                ViewBag.DataCount = q_s.Count;

                ViewBag.StuInfoList = StuInfoList;//信息开闭字典

                return View();
            }
        }
             

        //控制器里面：   年级专业班级联动
        [HttpPost]
        public JsonResult GetSpeList()//专业列表
        {
            string EntryYear = Request["EntryYearCode"].ToString();
            string schoolid = Request["SchoolID"].ToString();
            int Year = Convert.ToInt32(EntryYear);
            var Spe = (from p in sc.C_Specialty where p.EntryYear == Year && p.SchoolID == schoolid select new { p.SpeNo, p.SpeName }).ToList();

            return Json(new { count = Spe.Count, Pos = Spe });
        }

        [HttpPost]
        public JsonResult GetClassList()
        {
            string SpeNo = Request["SpeCode"].ToString();
            string schoolid = Request["SchoolID"].ToString();
            var Class = (from p in sc.T_Class where p.SpeNo == SpeNo && p.SchoolID == schoolid select p).ToList();
            return Json(new { count = Class.Count, Pos = Class });
        }

        [HttpPost]
        public JsonResult GetClassNum()
        {
            string ClassID = Request["ClassID"].ToString();
            var Classnum = (from f in sc.Student where f.StuClass == ClassID select f).ToList();
            return Json(new { count = Classnum.Count, Pos = Classnum.Count });
        }

        //获取企业提供的岗位
        [HttpPost]
        public JsonResult GetPosList()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            //获取企业
            string EntNo = Request["EntNo"].ToString();
            string entprano = EntNo + "-" + prcbatchid;

            var q_cate = (from f in ec.T_PracticePosition where f.EntPracNo == entprano select f).ToList();
            return Json(new { count = q_cate.Count, Pos = q_cate });
        }

        //获取参与高校的批次列表
        [HttpPost]
        public JsonResult GetBatchList()
        {
            string entno = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit;

            string schoolid = Request["SchoolID"].ToString();
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            //获取企业
            //string EntNo = Request["EntNo"].ToString();
            //string entprano = entno + "-" + prcbatchid;
            string entprano = SchoolHelper.GetEntPracticeNo(entno,prcbatchid);

            var q_cate = (from f in ec.T_EntBatchReg where f.EntPracNo == entprano select f).ToList();
            var q_cate1 = (from f in q_cate where f.SchoolID == schoolid select f).ToList();
            return Json(new { count = q_cate1.Count, Pos = q_cate1 });
        }

        //获取企业自身的岗位
        public JsonResult GetEntPosList()
        {
            string entno = (Session["Vars"] as ServicePlatform.Config.Vars).UserUnit;
            string schoolid = Request["SchoolID"].ToString();
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            //获取企业
            //string entprano = entno + "-" + prcbatchid;
            string entprano = SchoolHelper.GetEntPracticeNo(entno, prcbatchid);

            var q_cate = (from f in ec.T_PracticePosition where f.EntPracNo == entprano select f).ToList();
            return Json(new { count = q_cate.Count, Pos = q_cate });
        }

    }
}
