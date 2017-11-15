using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;
using System.Data.OleDb;
using Core.Services.Entity;
using T_PracticePosition = ServicePlatform.Models.T_PracticePosition;
using T_PracticeVolunteer = ServicePlatform.Models.T_PracticeVolunteer;
using T_Student = ServicePlatform.Models.T_Student;

namespace ServicePlatform.Areas.School.Controllers
{
    public class StuPosManagerController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();

        //学生报名情况查询
        [BaseActionFilter]
        
        public ActionResult EnrollInformation()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //var q0 = (from f in sc.School where f.SchoolID == schoolid select f.SchoolName).ToList();
            //ViewBag.SchoolName = q0[0].ToString();
            //ViewBag.SchoolID = schoolid;

            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            ViewBag.prcbatchid = prcbatchid;

            //制作表格
            List<T_PracticeVolunteer> tp = new List<T_PracticeVolunteer>();

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
                string SpeNo = Request["Specialty"].ToString();
                string ClassID = Request["Class"].ToString();

                string StuNameFromUser = Request["StuName"].ToString();
                string StuNOFromUser = Request["StuNO"].ToString();

                string EntNameFromUser = Request["EntName"].ToString();
                string PosNameFromUser = Request["PosName"].ToString();

                string VolSqcFromUser = Request["VolSqc"].ToString();
                if (VolSqcFromUser == "No_Limit")
                {
                    VolSqcFromUser = "";
                }

                string PosSqcFromUser = Request["PosSqc"].ToString();
                if (PosSqcFromUser == "No_Limit")
                {
                    PosSqcFromUser = "";
                }

                //获取年级
                List<SelectListItem> EntryYear = SchoolHelper.GetEntryYearList(schoolid, EntryYear1);
                ViewData["EntryYear"] = EntryYear;

                //获取专业
                List<SelectListItem> Specialty = SchoolHelper.GetSpecialtyList(schoolid, SpeNo);
                ViewData["Specialty"] = Specialty;

                //获取班级
                List<SelectListItem> Class = SchoolHelper.GetClassList(schoolid, ClassID);
                ViewData["Class"] = Class;

             
                var q_s = (from f in sc.Student where f.StuClass == ClassID && f.StuNo.Contains(StuNOFromUser) && f.StuName.Contains(StuNameFromUser) select f).ToList();
                string[] StuNo = new string[q_s.Count];
                string[] StuName = new string[q_s.Count];
                string[] StuSex = new string[q_s.Count];
                int[] LoopNum = new int[q_s.Count];


                for (int i = 0; i < q_s.Count; i++)
                {
                    StuNo[i] = q_s[i].StuNo;
                    StuName[i] = q_s[i].StuName;
                    if (q_s[i].StuSex != null)
                    {
                        StuSex[i] = q_s[i].StuSex;
                    }
                    else {
                        StuSex[i] = "";
                    }                    
                    string practiceno = q_s[i].UserID + prcbatchid;
                    var q_v = (from f in sc.PracticeVolunteer from EntReg in sc.T_EntBatchReg from ent in sc.T_Enterprise from po in sc.C_Position where f.EntPracNo == EntReg.EntPracNo && EntReg.Ent_No == ent.Ent_No && ent.Ent_Name.Contains(EntNameFromUser) && f.PosID == po.PositionID && po.PositionName.Contains(PosNameFromUser) && f.PracticeNo == practiceno && f.PreVolStatus == "1" && f.PosSequence.ToString().Contains(PosSqcFromUser) && f.VolunteerSequence.ToString().Contains(VolSqcFromUser) select f).ToList();
                    LoopNum[i] = q_v.Count;
                    for (int j = 0; j < q_v.Count; j++)
                    {
                        tp.Add(q_v[j]);
                    }
                }
                ViewBag.StuNo = StuNo;
                ViewBag.StuName = StuName;
                ViewBag.StuSex = StuSex;
                ViewBag.LoopNum = LoopNum;
                ViewBag.Volunteer = tp;

                if (Request["ToExcelFlag"].ToString() == "导出Excel")
                {
                    string TemplateFileName = "StuPosInfo.xlsx";
                    string result = string.Empty;
                    string strConn;
                    string strTemplateFilePath = AppDomain.CurrentDomain.BaseDirectory + "UserFiles/ReportTemplate/" + TemplateFileName;
                    string DownLoadFileName = (Session["Vars"] as ServicePlatform.Config.Vars).UserID + "-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "-" + TemplateFileName;
                    //string DownLoadFileName = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "-" + TemplateFileName;
                    string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "UserFiles/ReportDownLoad/" + DownLoadFileName;

                    string strSQL;

                    //File.Copy(strTemplateFilePath, strFilePath, "true");
                    //File
                    System.IO.File.Copy(strTemplateFilePath, strFilePath, true);
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";" + "Extended Properties=Excel 12.0";
                    OleDbConnection conn = new OleDbConnection(strConn);
                    conn.Open();
                    OleDbCommand mySqlCommand = new OleDbCommand();
                    mySqlCommand.Connection = conn;
                    /*
                    for (int iLoop = 0; iLoop < q_s.Count; iLoop++)
                    {
                        strSQL = "Insert into [Sheet1$] values('" + StuNo[iLoop] + "','" + StuName[iLoop] + "','" + StuSex[iLoop] + "','4','5','6','7','8','9','10','11','12')";
                        //OleDbCommand mySqlCommand = new OleDbCommand(strSQL, conn);
                        mySqlCommand.CommandText = strSQL;
                        try
                        {
                            mySqlCommand.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            ViewBag.error = ex.Message;
                            return View();
                        }
                    }
                     */
                    //------
                    int index = 0;
                    int loop = 0;

                     if (tp != null)
                    {
                        for (int i = 0; i < tp.Count(); i += loop)
                        {
                            for (int j = 0; j < LoopNum[index]; j++)
                            {
                                strSQL = "Insert into [Sheet1$] values('" + StuNo[index] + "','" + StuName[index] + "','" + StuSex[index] + "','" + tp[i + j].VolunteerSequence + "','" + tp[i + j].PosSequence + "','" + tp[i + j].EntName + "','" + tp[i + j].PosName + "','" + DateTimeFormat.ConvertIntDateTime(tp[i + j].InterviewTime).ToString() + "','" + tp[i + j].InterviewRecord + "','" + tp[i + j].InterviewScore + "','" + tp[i + j].VolunteerStatusName + "','" + tp[i + j].VolunteerStatusName +"')";
                                //OleDbCommand mySqlCommand = new OleDbCommand(strSQL, conn);
                                mySqlCommand.CommandText = strSQL;
                                try
                                {
                                    mySqlCommand.ExecuteNonQuery();

                                }
                                catch (Exception ex)
                                {
                                    ViewBag.error = ex.Message;
                                    return View();
                                }

                            }
                            loop = LoopNum[index];
                            index++;
                        }
		            }
                    //------
                    conn.Close();
                    //path = Server.MapPath("~/frog.jpg.jpg");            
                    return File(strFilePath, "application/zip-x-compressed", DownLoadFileName);

                }

                return View();
            }
        }

        //企业报名情况查询
        [BaseActionFilter] 
        public ActionResult EntEnrollInformation()
        {
            try
            {
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;

                ViewData["schoolid"] = schoolid;
                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);


                if (Request["Position"] == null)
                {
                    //获取企业
                    List<SelectListItem> Enterprise = SchoolHelper.GetEntList(prcbatchid);
                    ViewData["Enterprise"] = Enterprise;

                    //获取岗位
                    string entno = Enterprise[0].Value;
                    string entprano = entno + "-" + prcbatchid;
                    List<SelectListItem> Position = SchoolHelper.GetPosList(entprano);
                    ViewData["Position"] = Position;
                    ViewBag.CountNum = 0;
                    return View();
                }
                else
                {
                    string EntNo = Request["Enterprise"].ToString();
                    ViewBag.EntNo = EntNo;
                    string PosNo = Request["Position"].ToString();
                    string Entprano = EntNo + "-" + prcbatchid;
                    //获取企业
                    List<SelectListItem> Enterprise = SchoolHelper.GetEntList(prcbatchid, EntNo);
                    ViewData["Enterprise"] = Enterprise;

                    //获取岗位
                    string entno = Enterprise[0].Value;
                    string entprano = entno + "-" + prcbatchid;
                    List<SelectListItem> Position = SchoolHelper.GetPosList(entprano, PosNo);
                    ViewData["Position"] = Position;
                    if (PosNo == "Sel_All")
                    {
                        //var q_count = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano group f by f.VolunteerStatus into Status select Status.Key).ToList();
                        //var q_count1 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano group f by f.PosID into PositionNum select PositionNum.Key).ToList();
                        var q_count1 = (from f in ec.T_PracticePosition where f.EntPracNo == Entprano select f).ToList();
                        string[] PosID = new string[q_count1.Count];
                        int[] EnrollNum = new int[q_count1.Count];
                        string[] PosName = new string[q_count1.Count];
                        int[] MeetingNum = new int[q_count1.Count];
                        int[] MeetedNum = new int[q_count1.Count];
                        int[] HiredNum = new int[q_count1.Count];
                        int[] ConfirmNum = new int[q_count1.Count];
                        //string[] EnrollNum = new string[q_count.Count];
                        for (int i = 0; i < q_count1.Count; i++)
                        {
                            PosID[i] = q_count1[i].PositionID;
                            string posid = q_count1[i].PositionID;
                            var q = (from f in ctc.C_Position where f.PositionID == posid select f.PositionName).ToList();
                            PosName[i] = q[0];//记录岗位名
                            var q0 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == posid && f.PreVolStatus == "1" select f).ToList();
                            EnrollNum[i] = q0.Count();//记录报名人数
                            var q1 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == posid && f.VolunteerStatus >= 1 select f).ToList();
                            MeetingNum[i] = q1.Count();//记录安排面试人数
                            var q2 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == posid && f.VolunteerStatus >= 2 select f).ToList();
                            MeetedNum[i] = q2.Count();//记录已面试人数
                            var q3 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == posid && f.VolunteerStatus >= 4 select f).ToList();
                            HiredNum[i] = q3.Count();//录取人数
                            var q4 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == posid && f.VolunteerStatus == 5 select f).ToList();
                            ConfirmNum[i] = q4.Count();//学生已确认人数
                        }
                        ViewBag.Entprano = Entprano;
                        ViewBag.PosID = PosID;
                        ViewBag.PosName = PosName;
                        ViewBag.EnrollNum = EnrollNum;
                        ViewBag.MeetingNum = MeetingNum;
                        ViewBag.MeetedNum = MeetedNum;
                        ViewBag.HiredNum = HiredNum;
                        ViewBag.ConfirmNum = ConfirmNum;
                        ViewBag.CountNum = q_count1.Count;//找到几条记录
                        return View();
                    }
                    else
                    {
                        //var q_count1 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo select f).ToList();
                        var q_count1 = (from f in ec.T_PracticePosition where f.EntPracNo == Entprano && f.PositionID == PosNo select f).ToList();
                        string[] PosID = new string[q_count1.Count];
                        string[] PosName = new string[1];
                        int[] EnrollNum = new int[1];
                        int[] MeetingNum = new int[1];
                        int[] MeetedNum = new int[1];
                        int[] HiredNum = new int[1];
                        int[] ConfirmNum = new int[1];
                        PosID[0] = q_count1[0].PositionID;
                        string posid = q_count1[0].PositionID;
                        var q = (from f in ctc.C_Position where f.PositionID == PosNo select f.PositionName).ToList();
                        PosName[0] = q[0];//记录岗位名
                        var q0 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == posid && f.PreVolStatus == "1" select f).ToList();
                        EnrollNum[0] = q0.Count();//记录报名人数
                        var q1 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo && f.VolunteerStatus >= 1 select f).ToList();
                        MeetingNum[0] = q1.Count();//记录安排面试人数
                        var q2 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo && f.VolunteerStatus >= 2 select f).ToList();
                        MeetedNum[0] = q2.Count();//记录已面试人数
                        var q3 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo && f.VolunteerStatus >= 4 select f).ToList();
                        HiredNum[0] = q3.Count();//录取人数
                        var q4 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo && f.VolunteerStatus == 5 select f).ToList();
                        ConfirmNum[0] = q4.Count();//学生已确认人数
                        ViewBag.Entprano = Entprano;
                        ViewBag.PosID = PosID;
                        ViewBag.PosName = PosName;
                        ViewBag.EnrollNum = EnrollNum;
                        ViewBag.MeetingNum = MeetingNum;
                        ViewBag.MeetedNum = MeetedNum;
                        ViewBag.HiredNum = HiredNum;
                        ViewBag.ConfirmNum = ConfirmNum;
                        ViewBag.CountNum = 1;
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("查询功能开发中...");
            }
         

        }

        //企业报名情况详情
        [BaseActionFilter] 
        public ActionResult EntEnrollInformationDetail(string EntNo, string PosID)
        {
            var q=(from f in ec.T_Enterprise where f.Ent_No==EntNo select f.Ent_Name).ToList();
            ViewBag.EntName = q[0];
            var q1 = (from f in ctc.C_Position where f.PositionID == PosID select f.PositionName).ToList();
            ViewBag.PosName = q1[0];

            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);

            string entprano=EntNo+"-"+prcbatchid;

            var q2 = (from f in sc.PracticeVolunteer where f.EntPracNo == entprano && f.PosID == PosID && f.PreVolStatus == "1" select f).ToList();

            //var q_s = (from f in sc.Student where f.StuClass == ClassID select f).ToList();
            string[] StuNo = new string[q2.Count];
            string[] StuName = new string[q2.Count];

            for (int i = 0; i < q2.Count; i++)
            {
                string prano = q2[i].PracticeNo;
                string userid = prano.Replace(prcbatchid, "");
                T_Student ts = sc.Student.Find(userid);
                StuNo[i] = ts.StuNo;
                StuName[i] = ts.StuName;
            }
            ViewBag.StuNo = StuNo;
            ViewBag.StuName = StuName;
            ViewBag.Volunteer = q2;
            return View();
        }

        //给学生找岗位
        [BaseActionFilter] 
        public ActionResult FindPosToStu()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //var q0 = (from f in sc.School where f.SchoolID == schoolid select f.SchoolName).ToList();
            //ViewBag.SchoolName = q0[0].ToString();
            //ViewBag.SchoolID = schoolid;

            string EntryYear1=string.Empty;
            string SpeNo = string.Empty;
            string ClassID = string.Empty;

                        //制作表格
            //List<T_PracticeVolunteer> tp = new List<T_PracticeVolunteer>();
            List<T_Student> tpp = new List<T_Student>();

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

                if (Class.Count!=0)
                {
                    //EntryYear1 = EntryYear[0].Value;
                    //SpeNo = Specialty[0].Value;
                    ClassID = Class[0].Value;
                }
                else
                {
                    return View();
                }
            }
            else
            {
                EntryYear1 = Request["EntryYear"].ToString();
                SpeNo = Request["Specialty"].ToString();
                ClassID = Request["Class"].ToString();
                //获取年级
                List<SelectListItem> EntryYear = SchoolHelper.GetEntryYearList(schoolid, EntryYear1);
                ViewData["EntryYear"] = EntryYear;

                //获取专业
                List<SelectListItem> Specialty = SchoolHelper.GetSpecialtyList(schoolid, SpeNo);
                ViewData["Specialty"] = Specialty;

                //获取班级
                List<SelectListItem> Class = SchoolHelper.GetClassList(schoolid, ClassID);
                ViewData["Class"] = Class;

            }
            //获取学校当前批次
            //string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            var q_s = (from f in sc.Student where f.StuClass == ClassID select f).ToList();
            for (int i = 0; i < q_s.Count; i++)
            {
                tpp.Add(q_s[i]);
            }
            ViewBag.Student = tpp;
            return View();
        }

        //给学生找岗位详情
        [BaseActionFilter] 
        public ActionResult FindPosToStuDetails(string userid)
        {
            ViewBag.NowOpStu = userid;
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);

            string PracticeNo = userid + prcbatchid;

            var q_PerEnroll_part = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.PreVolStatus == "1"  orderby f.PosSequence ascending select f).ToList();
            string[] PosName_1 = new string[q_PerEnroll_part.Count];//志愿1
            string[] PosName_2 = new string[q_PerEnroll_part.Count];//志愿二
            string[] Desc_1 = new string[q_PerEnroll_part.Count];//岗位描述
            string[] Desc_2 = new string[q_PerEnroll_part.Count];//岗位描述
            string[] Time_1 = new string[q_PerEnroll_part.Count];//截止时间
            string[] Time_2 = new string[q_PerEnroll_part.Count];//截止时间
            string[] EntName_1 = new string[q_PerEnroll_part.Count];//截止时间
            string[] EntName_2 = new string[q_PerEnroll_part.Count];//截止时间
            string[] HasEnrollNum_1 = new string[q_PerEnroll_part.Count];//已经报名人数
            string[] HasEnrollNum_2 = new string[q_PerEnroll_part.Count];//已经报名人数
            int index1 = 0;
            int index2 = 0;
            for (int i = 0; i < q_PerEnroll_part.Count; i++)
            {
                string posid = q_PerEnroll_part[i].PosID;
                string EntProNo_temp = q_PerEnroll_part[i].EntPracNo;
                T_PracticePosition tpp = ec.T_PracticePosition.Find(EntProNo_temp, posid);

                var q_temp = (from f in ctc.C_Position where f.PositionID == posid select f.PositionName).ToList();
                if (q_PerEnroll_part[i].VolunteerSequence == 1)
                {
                    PosName_1[index1] = q_temp[0];
                    if (tpp != null)
                    {
                        Desc_1[index1] = tpp.PosDesc;
                        Time_1[index1] = DateTimeFormat.ConvertIntDateTime(tpp.PosExpireDate).ToString();
                        EntName_1[index1] = tpp.EntName;

                        var q_hasrnrollnum1 = (from f in sc.PracticeVolunteer where f.PosID == tpp.PositionID select f).ToList();
                        HasEnrollNum_1[index1] = q_hasrnrollnum1.Count.ToString();
                    }
                    index1++;
                }
                else
                {
                    PosName_2[index2] = q_temp[0];
                    Desc_2[index2] = tpp.PosDesc;
                    Time_2[index2] = DateTimeFormat.ConvertIntDateTime(tpp.PosExpireDate).ToString();
                    EntName_2[index2] = tpp.EntName;

                    var q_hasrnrollnum2 = (from f in sc.PracticeVolunteer where f.PosID == tpp.PositionID select f).ToList();
                    HasEnrollNum_2[index2] = q_hasrnrollnum2.Count.ToString();

                    index2++;
                }

            }

            ViewBag.PosName_1 = PosName_1;
            ViewBag.PosName_2 = PosName_2;
            ViewBag.Desc_1 = Desc_1;
            ViewBag.Time_1 = Time_1;
            ViewBag.Desc_2 = Desc_2;
            ViewBag.Time_2 = Time_2;
            ViewBag.EntName_1 = EntName_1;
            ViewBag.EntName_2 = EntName_2;
            ViewBag.HasEnrollNum_1 = HasEnrollNum_1;
            ViewBag.HasEnrollNum_2 = HasEnrollNum_2;

            ViewBag.q_PerEnroll_part = q_PerEnroll_part;

            return View();
        }

        [HttpPost]
        [BaseActionFilter] 
        public ActionResult FindPosToStuDetailsOperation1(string action)
        {
            string userid = Request.Form["NowOpStu"];
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string PracticeNo = userid + prcbatchid;
            if (action == "删除第一志愿")
            {
                var q = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.VolunteerSequence == 1 && f.PreVolStatus == "1" select f).ToList();
                for(int i=0;i<q.Count;i++)
                {
                    T_PracticeVolunteer tp = q[i];
                    sc.PracticeVolunteer.Remove(tp);
                }
                sc.SaveChanges();
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            else if (action == "设为第二志愿")
            {
                var q1 = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.VolunteerSequence == 2 && f.PreVolStatus == "1" select f).ToList();
                if(q1.Count!=0&&q1!=null)
                {
                    //return Content("<script>alert('该学生的第二志愿不为空，设置失败！')window.location.href = document.referrer;</script>");//返回上一页并刷新
                    //return Content("该学生的第二志愿不为空，设置失败！");//返回上一页并刷新
                    return RedirectToAction("Results", "SubmitResults", new { results = "该学生的第二志愿不为空，设置失败！" });
                }
                else
                {
                    var q = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.VolunteerSequence == 1 && f.PreVolStatus == "1" select f).ToList();
                    for (int i = 0; i < q.Count; i++)
                    {
                        T_PracticeVolunteer tp = q[i];
                        tp.VolunteerSequence = 2;
                    }
                    sc.SaveChanges();
                    return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
                }
            }
            else
            {
                string url="/School/StuPosManager/AddEnrollToStu?userid="+userid;
                return Redirect(url);
            }
        }

        [HttpPost]
        [BaseActionFilter] 
        public ActionResult FindPosToStuDetailsOperation2(string action)
        {
            string userid = Request.Form["NowOpStu"];
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string PracticeNo = userid + prcbatchid;
            if (action == "删除第二志愿")
            {
                var q = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.VolunteerSequence == 2 && f.PreVolStatus == "1" select f).ToList();
                for (int i = 0; i < q.Count; i++)
                {
                    T_PracticeVolunteer tp = q[i];
                    sc.PracticeVolunteer.Remove(tp);
                }
                sc.SaveChanges();
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            else if (action == "设为第一志愿")
            {
                var q1 = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.VolunteerSequence == 1 && f.PreVolStatus == "1" select f).ToList();
                if (q1.Count != 0 && q1 != null)
                {
                    //return Content("该学生的第一志愿不为空，设置失败！");//返回上一页并刷新
                    return RedirectToAction("Results", "SubmitResults", new { results = "该学生的第一志愿不为空，设置失败！" });
                }
                else
                {
                    var q = (from f in sc.PracticeVolunteer where f.PracticeNo == PracticeNo && f.VolunteerSequence == 2 && f.PreVolStatus == "1" select f).ToList();
                    for (int i = 0; i < q.Count; i++)
                    {
                        T_PracticeVolunteer tp = q[i];
                        tp.VolunteerSequence = 1;
                    }
                    sc.SaveChanges();
                    return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
                }
            }
            else
            {
                string url = "/School/StuPosManager/AddEnrollToStu?userid=" + userid;
                return Redirect(url);
            }
        }

        //删除报名
        [BaseActionFilter] 
        public ActionResult DeletePreEnroll(string pracno, string entpracno, string posid)
        {
            T_PracticeVolunteer tp = sc.PracticeVolunteer.Find(pracno, entpracno, posid);
            if (tp != null)
            {
                sc.PracticeVolunteer.Remove(tp);
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新

        }

        //设为第二岗位
        [BaseActionFilter] 
        public ActionResult SetVolSecond(string pracno, string entpracno, string posid)
        {
            T_PracticeVolunteer tp = sc.PracticeVolunteer.Find(pracno, entpracno, posid);
            if (tp != null)
            {
                tp.PosSequence = 2;
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //设为第一岗位
        [BaseActionFilter] 
        public ActionResult SetVolFirst(string pracno, string entpracno, string posid)
        {
            T_PracticeVolunteer tp = sc.PracticeVolunteer.Find(pracno, entpracno, posid);
            if (tp != null)
            {
                tp.PosSequence = 1;
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //添加岗位
        [BaseActionFilter] 
        public ActionResult AddEnrollToStu(string userid)
        {
            #region 获取学生信息
            if (userid==null)
            {
                userid = Request.Form["UserID"];
            }
            var q_name = (from f in sc.Student where f.UserID == userid select f.StuName).ToList();
            ViewBag.StuName = q_name[0];
            ViewBag.UserID = userid;

            #endregion

            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            //获取岗位
            //string entno = Enterprise[0].Value;
            //string entprano = entno + "-" + prcbatchid;
            string EntNo = string.Empty;
            string PosNo = string.Empty;
            string Entprano = string.Empty;

            if (Request["Enterprise"]==null&&Request["Position"] == null)
            {
                //获取企业
                List<SelectListItem> Enterprise = SchoolHelper.GetEntList(prcbatchid);
                ViewData["Enterprise"] = Enterprise;
                EntNo = Enterprise[0].Value;
                ViewBag.EntName = Enterprise[0].Text;
                Entprano = SchoolHelper.GetEntPracticeNo(EntNo, prcbatchid);
                //获取岗位
                List<SelectListItem> Position = SchoolHelper.GetPosList(Entprano);
                ViewData["Position"] = Position;
                PosNo = Position[0].Value;
            }
            else
            {
                EntNo = Request["Enterprise"].ToString();
                ViewBag.EntNo = EntNo;
                PosNo = Request["Position"].ToString();
                //获取企业
                List<SelectListItem> Enterprise = SchoolHelper.GetEntList(prcbatchid, EntNo);
                var q_entname = (from f in Enterprise where f.Value == EntNo select f.Text).FirstOrDefault();
                ViewBag.EntName = q_entname;
                ViewData["Enterprise"] = Enterprise;
                //EntNo = Enterprise[0].Value;
                Entprano = SchoolHelper.GetEntPracticeNo(EntNo, prcbatchid);
                //获取岗位
                List<SelectListItem> Position = SchoolHelper.GetPosList(Entprano, PosNo);
                ViewData["Position"] = Position;

            }

            //var q_count1 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo select f).ToList();
            var q_count1 = (from f in ec.T_PracticePosition where f.EntPracNo == Entprano && f.PositionID == PosNo select f).ToList();
            if(q_count1.Count==0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "数据出错！（该公司对应的岗位不存在）" });
            }
            string[] PosID = new string[q_count1.Count];
            string[] PosName = new string[1];
            int[] EnrollNum = new int[1];
            int[] MeetingNum = new int[1];
            int[] MeetedNum = new int[1];
            int[] HiredNum = new int[1];
            int[] ConfirmNum = new int[1];
            PosID[0] = q_count1[0].PositionID;
            string posid = q_count1[0].PositionID;
            var q = (from f in ctc.C_Position where f.PositionID == PosNo select f.PositionName).ToList();
            PosName[0] = q[0];//记录岗位名
            var q0 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == posid select f).ToList();
            EnrollNum[0] = q0.Count();//记录报名人数
            var q1 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo && f.VolunteerStatus >= 1 select f).ToList();
            MeetingNum[0] = q1.Count();//记录安排面试人数
            var q2 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo && f.VolunteerStatus >= 2 select f).ToList();
            MeetedNum[0] = q2.Count();//记录已面试人数
            var q3 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo && f.VolunteerStatus >= 4 select f).ToList();
            HiredNum[0] = q3.Count();//录取人数
            var q4 = (from f in sc.PracticeVolunteer where f.EntPracNo == Entprano && f.PosID == PosNo && f.VolunteerStatus == 5 select f).ToList();
            ConfirmNum[0] = q4.Count();//学生已确认人数
            ViewBag.Entprano = Entprano;
            ViewBag.PosID = PosID;
            ViewBag.PosName = PosName;
            ViewBag.EnrollNum = EnrollNum;
            ViewBag.MeetingNum = MeetingNum;
            ViewBag.MeetedNum = MeetedNum;
            ViewBag.HiredNum = HiredNum;
            ViewBag.ConfirmNum = ConfirmNum;
            ViewBag.CountNum = 1;
            return View();
        }

        [BaseActionFilter] 
        public ActionResult AddEnrollToStuOP(string UserID, string Entprano, string PosID, string volunteer, string vorder)
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            T_PracticeVolunteer tp = new T_PracticeVolunteer();
            string pracno = SchoolHelper.GetStuPracticeNo(UserID);
            tp.PracticeNo = pracno;
            tp.EntPracNo = Entprano;
            tp.PosID = PosID;
            T_PracticeVolunteer tp_check = sc.PracticeVolunteer.Find(UserID + prcbatchid, Entprano, PosID);
            if (tp_check != null)
            {
                //return Content("该志愿/顺序下的岗位不为空,添加失败！");//返回上一页并刷新
                string url = "/School/StuPosManager/AddEnrollToStu?userid=" + UserID;
                return Content("<script>alert('该志愿/顺序下的岗位不为空,添加失败！');window.location = '" + url + "';</script>");//返回上一页并刷新
            }

            
            //string vol = Request.Form["volunteer"];
            //string order = Request.Form["vorder"];
            if (volunteer == "firstv")
            {
                tp.VolunteerSequence = 1;
            }
            else
            {
                tp.VolunteerSequence = 2;
            }
            if (vorder == "1")
            {
                tp.PosSequence = 1;
            }
            else
            {
                tp.PosSequence = 2;
            }
            tp.VolunteerStatus = 0;
            tp.PreVolStatus = "1";
            string url1 = "/School/StuPosManager/AddEnrollToStu?userid=" + UserID;
            var checkagain = (from f in sc.PracticeVolunteer where f.PracticeNo == pracno && f.EntPracNo == Entprano && f.VolunteerSequence != tp.VolunteerSequence && f.PreVolStatus == "1" select f).ToList();
            if (checkagain.Count==0)
            {
                sc.PracticeVolunteer.Add(tp);
                sc.SaveChanges();
            }
            else
            {
                return Content("<script>alert('正式报名同一公司的岗位须保持该公司的所有岗位在同一志愿顺序下！');window.location = '" + url1 + "';</script>");//返回上一页并刷新
            }

            return Content("<script>alert('添加成功');window.location = '" + url1 + "';</script>");//返回上一页并刷新
        }

        //给岗位找学生
        [BaseActionFilter] 
        public ActionResult FindStuToEnt()
        {
            return View();
        }



        //控制器里面：   年级专业班级联动
        [HttpPost]
        public JsonResult GetSpeList()//专业列表
        {
            string EntryYear = Request["EntryYearCode"].ToString();
            //string schoolid = "10385";
            int Year = Convert.ToInt32(EntryYear);
            var Spe = (from p in sc.C_Specialty where p.EntryYear == Year select new { p.SpeNo, p.SpeName }).ToList();

            return Json(new { count = Spe.Count, Pos = Spe });
        }

        [HttpPost]
        public JsonResult GetClassList()
        {
            string SpeNo = Request["SpeCode"].ToString();
            var Class = (from p in sc.T_Class where p.SpeNo == SpeNo select p).ToList();
            return Json(new { count = Class.Count, Pos = Class });
        }

        [HttpPost]
        public JsonResult GetClassNum()
        {
            string ClassID = Request["ClassID"].ToString();
            var Classnum = (from f in sc.Student where f.StuClass == ClassID select f).ToList();
            return Json(new { count = Classnum.Count, Pos = Classnum.Count });
        }
        private Repository _db;

        //获取企业提供的岗位
        [HttpPost]
        public JsonResult GetPosList(string schoolid,string EntNo)
        {
            if (string.IsNullOrEmpty(schoolid))
            {
                schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            }
            _db=new Repository();
            var qCate =_db.T_PracticePosition.Where(
                a =>
                    a.T_EntBatchReg.Ent_No == EntNo && a.T_EntBatchReg.T_PracBatch.SchoolID == schoolid &&
                    a.T_EntBatchReg.T_PracBatch.IsCurrentBatch == "是").Select(b=>new 
                    {
                        EntPracNo=b. EntPracNo,
                        PositionID = b.PositionID,
                        PubDate = b.PubDate,
                        PosDesc = b.PosDesc,
                        PosQuantity = b.PosQuantity,
                        PosExpireDate = b.PosExpireDate,
                        PositionName=b.C_Position.PositionName
                    }).ToList();
            
            return Json(new { count = qCate.Count, Pos = qCate });
        }
    }
}
