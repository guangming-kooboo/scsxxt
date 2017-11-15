using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;
using ServicePlatform.Areas.Permission.Models;
using System.Text.RegularExpressions;
using Qx.Tools.CommonExtendMethods;

namespace ServicePlatform.Areas.School.Controllers
{
    public class S_MyWorkPlatController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private PermissionContext pc = new PermissionContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();

        [BaseActionFilter]
        public ActionResult MenuLinkToMyWorkPlat()
        {
            string UserID = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            var StuInBatch = (from f in sc.StuBatchReg where f.UserID == UserID select f).ToList();
            string PracticeNo = "";
            //一个学生多次参加实训的情况没有处理！！！！
            if (StuInBatch.Count > 0)
            {
                PracticeNo = StuInBatch[0].PracticeNo;
            }
            string hLink = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab1&StuPracNo=" + PracticeNo;
            return Redirect(hLink);
        }

        [BaseActionFilter]
        public ActionResult MyWorkPlat(string focus, string StuPracNo, string ForbidFlag)
        {
            //二次检验防恶意修改
            string recheck = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            var q_recheck = pc.UserRole.Where(a => a.UserID == recheck).Select(a => a.RoleID).ToList();
            if (!q_recheck.Contains("61"))
            {
                ForbidFlag = "true";
            }
            ViewBag.fobidList = (Session["Vars"] as ServicePlatform.Config.Vars).getButtons();

            List<string> aaaa = (Session["Vars"] as ServicePlatform.Config.Vars).getButtons();

            var q_canenter = (from f in sc.StuBatchReg where f.PracticeNo == StuPracNo select f.CareerStatusID).ToList();
            if (q_canenter.Count==0||q_canenter[0] < 13)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "当前状态下无法操作我的工作台(可能原因：1没有对应的批次号；2您当前的职业生涯状态不足以操作我的工作台)" });
            }
            List<string> temp_func = (Session["Vars"] as ServicePlatform.Config.Vars).getButtons();

            if (StuPracNo == null)
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
                    StuPracNo = temp;
                }
            }
            ViewBag.PraticeNo = StuPracNo;

            var q_batch = (from f in sc.StuBatchReg where f.PracticeNo == StuPracNo select new{f.PracBatchID,f.ReviewScore}).ToList();

            string temp_batch = q_batch[0].PracBatchID;

            var q_school = (from f in sc.PracBatch where f.PracBatchID == temp_batch select f.SchoolID).ToList();

            ViewBag.NowOpSchoolID = q_school[0];


            //获取我的周记
            #region 获取我的周记
            var q = (from f in sc.WeekRecord where f.PracticeNo == StuPracNo select f).ToList();

            //string[] tableTh=new string[7]{"编号","标题","发布时间","内容","评论","得分","操作"};
            string[] tableTh = SchoolHelper.GetModelInfo_Th(new T_WeekRecord());//获取模型属性列

            string[][] dataList = new string[q.Count][];
            if(ForbidFlag=="true")
            {
                for (int i = 0; i < q.Count; i++)
                {
                    string[] hang = SchoolHelper.GetModelInfo_Tr(q[i]);
                    string url1 = "/School/S_MyWorkPlat/DeleteWeekRecord?practiceno=" + hang[0] + "&recordno=" + hang[2];
                    string url_edit = "/School/S_MyWorkPlat/EditWeekRecord?practiceno=" + hang[0] + "&recordno=" + hang[2];
                    hang[hang.Length - 1] = "";
                    //    string.Format
                    //    ("<a onclick=show('{0})'id='vie_weekrec'>查看</a>",
                    //    hang[5].Replace("'", "\"").Replace("\r\n", "<br/>").Replace("\r", "").Replace("\n", ""));
                    dataList[i] = hang;
                }
            }
            else
            {
                for (int i = 0; i < q.Count; i++)
                {
                    string[] hang = SchoolHelper.GetModelInfo_Tr(q[i]);
                    string url1 = "/School/S_MyWorkPlat/DeleteWeekRecord?practiceno=" + hang[0] + "&recordno=" + hang[2];
                    string url_edit = "/School/S_MyWorkPlat/EditWeekRecord?practiceno=" + hang[0] + "&recordno=" + hang[2];
                    string url_view = "/School/S_MyWorkPlat/EditWeekRecord?practiceno=" + hang[0] + "&recordno=" + hang[2] + "&type=view";
                    //hang[hang.Length - 1] = "<a " + "onclick=" + "\"showEdit('" + url_edit + "','" + hang[4] + "','" + hang[5] + "')\"" + "id=\"edi_weekrec\">编辑</a>" + " || " + "<a " + "onclick=" + "\"show('" + hang[5] + "')\"" + " id=\"vie_weekrec\">查看</a>" + " || " + "<a " + "id=\"del_weekrec\"" + "href='" + url1 + "''>删除</a>";
                    //hang[hang.Length - 1] = "<a " + "onclick=" + "\"showEdit('" + url_edit + "','" + hang[4] + "','" + hang[5] + "')\"" + "id=\"edi_weekrec\">编辑</a>" + " || "  + "<a " + "id=\"del_weekrec\"" + "href='" + url1 + "''>删除</a>";
                    hang[hang.Length - 1] = "<a " + "href=" + "'" + url_edit + "'" + "id=\"edi_weekrec\">编辑</a>" + " || " + "<a " + "href=" + "'" + url_view + "'" + "id=\"edi_weekrec\">查看</a>" +"||"+ "<a " + "id=\"del_weekrec\"" + "href='" + url1 + "''>删除</a>";
                    dataList[i] = hang;
                }
            }


            TableHelper table1 = new TableHelper("T_WeekRec", tableTh, dataList);
            ViewData["Table1"] = table1;
            ViewBag.WeekRecNum = q.Count;
            #endregion
            #region 教训
            //ViewBag.tableId = "T_WeekRec";
            //ViewBag.tableTh=tableTh;
            //ViewBag.dataList=dataList;

            //ViewBag.Table1=table1;

            //string []Keys=new string[]{"Table1","Table2","Table3"};
            //ViewBag.Keys=Keys;
            //ViewBag.Table="Table1";
            #endregion

            //获取我的案例
            #region 获取我的案例
            var q_case = (from f in sc.PracticeCase where f.PracticeNo == StuPracNo group f by f.CaseNo into Case select Case.Key).ToList();

            //string[] tableTh2 = SchoolHelper.GetModelInfo_Th(new T_PracticeCase());//获取模型属性列

            string[] tableTh2 = new string[4] { "编号", "发布时间","案例标题", "操作" };

            string[][] dataList2 = new string[q_case.Count][];
            if (ForbidFlag=="true")
            {
                for (int i = 0; i < q_case.Count; i++)
                {
                    //string[] hang = SchoolHelper.GetModelInfo_Tr(q_case[i]);
                    int casenonow=q_case[i];
                    var q_casename = (from f in sc.PracticeCase where f.CaseNo == casenonow select f.CaseName).FirstOrDefault();
                    string[] hang = new string[4] { q_case[i].ToString(), DateTimeFormat.ConvertIntDateTime(q_case[i]).ToString(), q_casename, null };
                    string url1 = "/School/S_MyWorkPlat/DeletePracticeCase?caseno=" + q_case[i].ToString();
                    hang[3] = "<a " + "onclick=" + "\"jump('" + q_case[i].ToString() + "')\"" + ">查看</a>";
                    dataList2[i] = hang;
                }
            }
            else
            {
                for (int i = 0; i < q_case.Count; i++)
                {
                    //string[] hang = SchoolHelper.GetModelInfo_Tr(q_case[i]);
                    int casenonow = q_case[i];
                    var q_casename = (from f in sc.PracticeCase where f.CaseNo == casenonow select f.CaseName).FirstOrDefault();
                    string[] hang = new string[4] { q_case[i].ToString(), DateTimeFormat.ConvertIntDateTime(q_case[i]).ToString(), q_casename, null };
                    string url1 = "/School/S_MyWorkPlat/DeletePracticeCase?caseno=" + q_case[i].ToString();
                    hang[3] = "<a " + "onclick=" + "\"jumpedit('" + q_case[i].ToString() + "')\"" + " id=\"edi_case\">编辑</a>" + " || " + "<a " + "onclick=" + "\"jump('" + q_case[i].ToString() + "')\"" + ">查看</a>" + " || " + "<a " + "id=\"del_case\"" + "href='" + url1 + "''>删除</a>";
                    dataList2[i] = hang;
                }
            }


            TableHelper table2 = new TableHelper("T_PracticeCase", tableTh2, dataList2);
            ViewData["Table2"] = table2;
            ViewBag.CaseNum = q_case.Count;
            //ViewBag.PracticeCase = q_case;
            #endregion

            //获取我的实习报告
            #region 获取我的实习报告
            var q_report = (from f in sc.StuBatchReg where f.PracticeNo == StuPracNo select f).ToList();

            string[] tableTh3 = new string[4] { "编号", "发布时间","文件名", "操作" };

            string[][] dataList3 = new string[q_report.Count][];
            if (ForbidFlag=="true")
            {
                for (int i = 0; i < q_report.Count; i++)
                {
                    if (q_report[i].PracticeReport != null)
                    {
                        List<string> process = new List<string>();
                        if (q_report[i].PracticeReportFile.HasValue())
                        {
                            process = q_report[i].PracticeReportFile.Split('/').ToList();
                            string tempprocess = process[6].Split('.')[0];
                            //string[] hang = SchoolHelper.GetModelInfo_Tr(q_case[i]);
                            string[] hang = new string[4]
                            {
                                q_report[i].PracticeReport,
                                q_report[i].PracticeReport,
                                process[6], null
                            };
                            string url1 = "/School/S_MyWorkPlat/ViewReport?practiceno=" + q_report[i].PracticeNo;
                            string url2 = "/School/S_MyWorkPlat/DeleteReport?practiceno=" + q_report[i].PracticeNo;
                            hang[3] = "<a " + "id=\"view_rep\"" + "href='" + url1 + "''>预览</a>";
                            dataList3[i] = hang;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < q_report.Count; i++)
                {
                    if (q_report[i].PracticeReport != null)
                    {
                        List<string> process =new List<string>();
                        if (q_report[i].PracticeReportFile.HasValue())
                        {
                            process = q_report[i].PracticeReportFile.Split('/').ToList();
                            string tempprocess = process[6].Split('.')[0];
                            //string[] hang = SchoolHelper.GetModelInfo_Tr(q_case[i]);
                            string[] hang = new string[4]
                            { q_report[i].PracticeReport,
                           q_report[i].PracticeReport,
                            process[6], null };
                            string url1 = "/School/S_MyWorkPlat/ViewReport?practiceno=" + q_report[i].PracticeNo;
                            string url2 = "/School/S_MyWorkPlat/DeleteReport?practiceno=" + q_report[i].PracticeNo;
                            hang[3] = "<a " + "id=\"view_rep\"" + "href='" + url1 + "''>预览</a>" + " || " + "<a " + "id=\"del_rep\"" + "href='" + url2 + "''>删除</a>";
                            dataList3[i] = hang;
                        }
                       
                    }
                }
            }


            TableHelper table3 = new TableHelper("T_Report", tableTh3, dataList3);
            ViewData["Table3"] = table3;
            ViewBag.ReportNum = q_report.Count;
            //ViewBag.Report = q_report;
            #endregion

            //获取我的实习评分--企业
            //#region 获取我的实习评分--企业
            //var q_prasco = (from f in ec.T_EntStudentReviewLink where f.PracticeNo == StuPracNo select f).ToList();

            //string[] tableTh4 = new string[4] { "序号", "评分项目", "占比", "评分" };


            //string[][] dataList4 = new string[q_prasco.Count][];
            //for (int i = 0; i < q_prasco.Count; i++)
            //{
            //    if (q_prasco[i].EntPracNo != null)
            //    {
            //        //string[] hang = SchoolHelper.GetModelInfo_Tr(q_case[i]);
            //        string[] hang = new string[4] { q_prasco[i].ItemID, q_prasco[i].ItemName, q_prasco[i].ItemWeight.ToString(), q_prasco[i].ReviewScore.ToString() };
            //        //string url1 = "/School/S_MyWorkPlat/ViewReport?practiceno=" + q_prasco[i].PracticeNo;
            //        //string url2 = "/School/S_MyWorkPlat/DeleteReport?practiceno=" + q_prasco[i].PracticeNo;
            //        //hang[2] = "<a " + "id=\"view_rep\"" + "href='" + url1 + "''>预览</a>" + " || " + "<a " + "id=\"del_rep\"" + "href='" + url2 + "''>删除</a>";
            //        dataList4[i] = hang;
            //    }
            //}

            //TableHelper table4 = new TableHelper("T_ReviewScore", tableTh4, dataList4);
            //ViewData["Table4"] = table4;

            //#endregion

            //获取我的实习评分--学校
            //#region 获取我的实习评分--学校
            //var q_prascos = (from f in sc.T_SchoolStudentReviewLink where f.PracticeNo == StuPracNo select f).ToList();

            //string[] tableTh5 = new string[4] { "序号", "评分项目", "占比", "评分" };


            //string[][] dataList5 = new string[q_prascos.Count][];
            //for (int i = 0; i < q_prascos.Count; i++)
            //{
            //    if (q_prascos[i].PracticeNo != null)
            //    {
            //        //string[] hang = SchoolHelper.GetModelInfo_Tr(q_case[i]);
            //        string[] hang = new string[4] { q_prascos[i].ItemID, q_prascos[i].ItemName, q_prascos[i].ItemWeight.ToString(), q_prascos[i].ReviewScore.ToString() };
            //        //string url1 = "/School/S_MyWorkPlat/ViewReport?practiceno=" + q_prasco[i].PracticeNo;
            //        //string url2 = "/School/S_MyWorkPlat/DeleteReport?practiceno=" + q_prasco[i].PracticeNo;
            //        //hang[2] = "<a " + "id=\"view_rep\"" + "href='" + url1 + "''>预览</a>" + " || " + "<a " + "id=\"del_rep\"" + "href='" + url2 + "''>删除</a>";
            //        dataList5[i] = hang;
            //    }
            //}

            //TableHelper table5 = new TableHelper("T_ReviewScore", tableTh5, dataList5);
            //ViewData["Table5"] = table5;

            //#endregion


            if (focus == null)
            {
                ViewBag.Focus = "tab1";
            }
            else
            {
                ViewBag.Focus = focus;
            }

            //string[] FlagList = ForbidList.Split('!');
            ViewBag.ForbidFlag = ForbidFlag;
            ViewBag.fobidlist = temp_func;
            ViewBag.ScoreForAll = q_batch[0].ReviewScore;
            return View();
        }

        //添加周记
        [BaseActionFilter]
        [ValidateInput(false)]
        public ActionResult AddWeekRecord()
        {
            string title = Request.Form["title"];
            string content = Request.Form["content"];
            string summary = Request.Form["summary"];
            string practiceno = Request.Form["practiceno"];

            T_WeekRecord tw = new T_WeekRecord();
            int no = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
            tw.PracticeNo = practiceno;
            tw.RecordDate = no;
            tw.RecordContent = content;
            tw.RecordTitle = title;
            tw.RecordComment = summary;
            tw.RecordNo = no;

            sc.WeekRecord.Add(tw);
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //删除周记
        [BaseActionFilter]
        public ActionResult DeleteWeekRecord(string practiceno, int recordno)
        {
            T_WeekRecord tw = sc.WeekRecord.Find(practiceno, recordno);
            if (tw != null)
            {
                string temp = tw.RecordContent;
                string pattern = @"/UserFiles/image/";//识别模式,路径形式可自定义，找到所有路径的最小公共部分；
                Lib.FileOperate.DeleteContentFlie(temp, pattern);
                sc.WeekRecord.Remove(tw);
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //编辑周记
        [BaseActionFilter]
        [ValidateInput(false)]
        public ActionResult EditWeekRecord(string practiceno, int recordno,string type)
        {         
            if (practiceno != null)
            {
                T_WeekRecord tw = sc.WeekRecord.Find(practiceno, recordno);
                ViewBag.Edit = tw;
                ViewBag.PraticeNo = practiceno;
                ViewBag.Recordno = recordno;
                if (type == "view")
                {
                    ViewBag.Commit = "false";
                }
                else
                {
                    ViewBag.Commit = "true";
                }
                return View();
            }
            else
            {
                return View();
            }
            

        }

        [BaseActionFilter]
        [ValidateInput(false)]
        public ActionResult SubmitEditWeekRecord()
        {
            string pno = Request.Form["practiceno"];
            string rno = Request.Form["recordno"];
            int introno = Convert.ToInt32(rno);
            T_WeekRecord tw = sc.WeekRecord.Find(pno, introno);
            if (tw != null)
            {
                string temp = tw.RecordContent;
                string pattern = @"/UserFiles/image/";//识别模式,路径形式可自定义，找到所有路径的最小公共部分；
                Lib.FileOperate.DeleteContentFlie(temp, pattern);

                string title = Request.Form["title"];
                string content = Request.Form["content"];
                tw.RecordTitle = title;
                tw.RecordContent = content;
                sc.SaveChanges();
            }
            //return Content(/"<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            string url = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab1&StuPracNo=" + pno;
            return Redirect(url);
        }

        [BaseActionFilter]
        public ActionResult AddNewCase(string type, string caseno, string temp_sid)
        {
            string schoolid = string.Empty;
            if (temp_sid == null || temp_sid == "")
            {
                schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            }
            else
            {
                schoolid = temp_sid;
            }
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            ViewBag.CaseNo = DateTimeFormat.ConvertDateTimeInt(DateTime.Now).ToString();
            var q_caseitem = (from f in ctc.C_PracticeCaseItem
                              where f.PracBatchID == prcbatchid
                              orderby f.ItemSequence
                              select f).ToList();
            ViewBag.PracticeCaseItem = q_caseitem;
            if (type == null)
            {
                ViewBag.Type = "ADD";
            }
            else if (type == "Detail")
            {
                int NO = Convert.ToInt32(caseno);
                var qq = (from f in sc.PracticeCase where f.CaseNo == NO select f).ToList();
                ViewBag.PracticeCase = qq;
                ViewBag.Type = type;
                ViewBag.PracticeNo = qq[0].PracticeNo;
            }
            else
            {
                int NO = Convert.ToInt32(caseno);
                var qq = (from f in sc.PracticeCase where f.CaseNo == NO select f).ToList();
                ViewBag.PracticeCaseTitle = sc.PracticeCase.FirstOrDefault(a=>a.CaseNo== NO).CaseName;
                ViewBag.PracticeCase = qq;
                ViewBag.Type = type;
                ViewBag.No = NO;
            }
            return View();
        }


        //添加案例
        [BaseActionFilter]
        [ValidateInput(false)]
        public ActionResult AddPracticeCase()
        {
            string caseno = Request.Form["caseno"];
            string casename = Request.Form["casename"];
            int CASENO = Convert.ToInt32(caseno);
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //string praticeno=userid+prcbatchid;
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
            var q_caseitem = (from f in ctc.C_PracticeCaseItem where f.PracBatchID == prcbatchid orderby f.ItemSequence select f).ToList();
            string[] itemno = new string[q_caseitem.Count];
            for (int i = 0; i < q_caseitem.Count; i++)
            {
                itemno[i] = q_caseitem[i].ItemNo;
            }

            var q_caseexit = (from f in sc.PracticeCase where f.CaseNo == CASENO select f).ToList();
            if (q_caseexit.Count>0)//存在为修改
            {
                for(int i=0;i<q_caseexit.Count;i++)
                {
                    T_PracticeCase tp_modify = q_caseexit[i];

                    string content = tp_modify.ItemContent;
                    string pattern = @"/UserFiles/image/";//识别模式,路径形式可自定义，找到所有路径的最小公共部分；
                    Lib.FileOperate.DeleteContentFlie(content, pattern);
                    tp_modify.CaseName = casename;
                    tp_modify.ItemContent = Request.Form[q_caseexit[i].ItemNo];
                }
                sc.SaveChanges();
                
            }
            else//否则为添加
            {
                for (int i = 0; i < itemno.Count(); i++)
                {
                    T_PracticeCase tp = new T_PracticeCase();
                    tp.PracticeNo = praticeno;
                    tp.ItemNo = itemno[i];
                    tp.ItemContent = Request.Form[itemno[i]];
                    tp.CaseNo = CASENO;
                    tp.CaseName = casename;
                    sc.PracticeCase.Add(tp);
                }
                sc.SaveChanges();
            }

            string url = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab2&StuPracNo=" + praticeno;
            //string hLink = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab1&StuPracNo=" + PracticeNo;
            return Redirect(url);
        }

        //删除案例
        [BaseActionFilter]
        [ValidateInput(false)]
        public ActionResult DeletePracticeCase(int caseno)
        {
            var q = (from f in sc.PracticeCase where f.CaseNo == caseno select f).ToList();
            string practiceno = q[0].PracticeNo;
            for (int i = 0; i < q.Count; i++)
            {
                string temp = q[i].ItemContent;
                string pattern = @"/UserFiles/image/";//识别模式,路径形式可自定义，找到所有路径的最小公共部分；
                Lib.FileOperate.DeleteContentFlie(temp, pattern);

                string itemno = q[i].ItemNo;
                T_PracticeCase tp = sc.PracticeCase.Find(practiceno, caseno, itemno);
                sc.PracticeCase.Remove(tp);

            }
            sc.SaveChanges();
            //string url="/School/S_MyWorkPlat/MyWorkPlat?focus=tab2";
            string url = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab2&StuPracNo=" + practiceno;
            return Redirect(url);
            //return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //添加实习报告--文件
        [BaseActionFilter]
        public ActionResult AddNewReport_ByFile(HttpPostedFileBase Report)
        {
            //string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            string practiceno = Request.Form["practiceno"];
            T_StuBatchReg tu = sc.StuBatchReg.Find(practiceno);
            if (Report != null)//如果重新提交
            {
                string old = tu.PracticeReportFile;
                if (old != null)
                {
                    Lib.FileOperate.DeleteFlie(old);
                }

                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                string url = "/UserFiles/School/" + schoolid + "/OtherFiles/StuReport/";
                string result = Lib.FileOperate.UploadFile_OriName(Report, url);
                tu.PracticeReport = DateTimeFormat.ConvertDateTimeInt(DateTime.Now).ToString();
                tu.PracticeReportFile = result;
                sc.SaveChanges();
            }
            else
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "文件不能为空" });
            }
            //string u = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab3";
            string u = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab3&StuPracNo=" + practiceno;
            return Redirect(u);
            //return Content("<script>alert('文件上传成功!');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //删除实习报告
        [BaseActionFilter]
        public ActionResult DeleteReport(string practiceno)
        {
            T_StuBatchReg tu = sc.StuBatchReg.Find(practiceno);
            string old = tu.PracticeReportFile;
            if (old != null)
            {
                Lib.FileOperate.DeleteFlie(old);
            }
            tu.PracticeReportFile = null;
            tu.PracticeReport = null;
            sc.SaveChanges();
            //string u = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab3";
            string u = "/School/S_MyWorkPlat/MyWorkPlat?focus=tab3&StuPracNo=" + practiceno;
            return Redirect(u);
            //return Content("<script>alert('删除成功!');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //预览实习报告
        [BaseActionFilter]
        public ActionResult ViewReport(string practiceno)
        {
            string old = string.Empty;
            if (practiceno == null)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "数据出错，实习号为空!" });
            }
            else
            {
                T_StuBatchReg tu = sc.StuBatchReg.Find(practiceno);
                old = tu.PracticeReportFile;
            }
            //string path = AppDomain.CurrentDomain.BaseDirectory + "Areas/School/Content/Home/file/";
            //string fileName = "学生信息导入模版.xls";
            return Redirect(old);
        }


        //评价企业
        [BaseActionFilter]
        public ActionResult StuEvaluateEnt(string StuID)
        {
            string userid = string.Empty;
            if (StuID == null)
            {
                userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            }
            else
            {
                userid = StuID;
                ViewBag.Foibid = "true";
            }
            var q_check = (from f in sc.StuBatchReg where f.UserID == userid select f).ToList();
            if (q_check.Count==0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "你的批次信息有误，请联系高校管理员！（批次表查无此人）" });
            }
            if (q_check[0].CareerStatusID < 13)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "当前状态下无法评价企业！" });
            }
            else
            {
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                if(schoolid==null|| schoolid=="")
                {
                    schoolid = sc.Student.FirstOrDefault(a => a.UserID == StuID).SchoolID;//防止企业登录报错
                }
                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
                string practiceno = SchoolHelper.GetStuPracticeNo(userid);

                double Score = SchoolHelper.GetStuEvaEntSchoolScore(practiceno, "Ent");
                if (Score != 0.0)
                {
                    ViewBag.Score = Score;
                }

                var q = (from f in ctc.C_StuEvaluateEntItem orderby f.ItemSequence where f.PracBatchID == prcbatchid select f).ToList();

                //星星个数
                var q_num = (from f in ctc.C_StuEvaEntGradeLevelItem orderby f.Weight where f.PracBatchID == prcbatchid select f).ToList();

                string[] star = new string[q.Count];
                string[] result = new string[q.Count];
                string[] judge = new string[q.Count];
                for (int i = 0; i < q.Count; i++)
                {
                    star[i] = "star" + i;
                    result[i] = "result" + i;
                    judge[i] = "judge" + i;
                }
                ViewBag.StuEvaluateEntItem = q;
                ViewBag.StuEvaEntGradeLevelItem = q_num;
                ViewBag.Star = star;
                ViewBag.Judge = judge;
                ViewBag.Result = result;
                ViewBag.ItemCount = q.Count;

                //T_Student ts = sc.Student.Find(userid);
                //ViewBag.StuMessage = ts;
                T_StuBatchReg tsb = sc.StuBatchReg.Find(practiceno);
                if (tsb == null)
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "数据出错！错误原因：该学生未被注册到批次" });
                }
                ViewBag.StuMessage2 = tsb.PracticeStartEnd;

                var q_stutowhere = (from f in sc.PracticeVolunteer where f.PracticeNo == practiceno && f.PreVolStatus == "1" && f.VolunteerStatus == 5 select f).FirstOrDefault();
                if (q_stutowhere == null)
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "数据出错,错误原因：该学生的终录信息有误" });
                }
                T_PracticeVolunteer tp = q_stutowhere;
                ViewBag.StuMessage = tp;


                //获取已经评价的项目列表
                var q_hasp = (from f in sc.StuEvaluateEnt where f.PracticeNo == practiceno select f).ToList();
                ViewBag.HasEvaList = q_hasp;


                return View();
            }
        }

        //添加评价企业项目
        [BaseActionFilter]
        public ActionResult AddStuEvaEntItem(string final)
        {
            if (final == null)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "数据异常！错误原因：评价列表为空" });
            }
            else
            {
                string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
                string[] result = final.Split('!');
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
                string pacticeno = userid + prcbatchid;
                var q = (from f in ctc.C_StuEvaluateEntItem orderby f.ItemSequence where f.PracBatchID == prcbatchid select f).ToList();

                var q_grade = (from f in ctc.C_StuEvaEntGradeLevelItem where f.PracBatchID == prcbatchid orderby f.Weight ascending select f).ToList();
                int index = 0;
                for (int i = 0; i < q.Count; i++)
                {
                    if (index >= result.Length - 1)
                    {
                        break;
                    }
                    else
                    {
                        T_StuEvaluateEnt ts = new T_StuEvaluateEnt();
                        ts.ItemNo = q[i].ItemNo;
                        //string temp = string.Empty;
                        var q_temp = (from f in q_grade where f.ItemName == result[index] select f.ItemNo).ToList();
                        if (q_temp.Count==0)
                        {
                            var qqqq = (from f in q_grade orderby f.Weight descending select f.ItemNo).FirstOrDefault();
                            ts.ItemGrade = qqqq;
                        }
                        else
                        {
                            ts.ItemGrade = q_temp[0];
                        }
                        index++;
                        //if (result[index] == "")
                        //{
                        //temp = q_grade[q_grade.Count - 1].ItemNo;
                        //}
                        //else
                        //{
                        //int a = Convert.ToInt32(result[index++].Replace("0", ""));
                        //temp = q_grade[a - 1].ItemNo;
                        //}
                        
                        ts.ItemContent = result[index];
                        ts.PracticeNo = pacticeno;
                        sc.StuEvaluateEnt.Add(ts);
                        index++;
                    }
                }
                sc.SaveChanges();

                double Score = SchoolHelper.GetStuEvaEntSchoolScore(pacticeno, "Ent");
                T_StuBatchReg tbr = (from f in sc.StuBatchReg where f.UserID == userid select f).FirstOrDefault();
                tbr.StuEvaEntScore = Score;

                sc.SaveChanges();

                return Content("<script>alert('评价成功提交!');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }

        }

        [BaseActionFilter]
        public ActionResult DeleteStuEvaEntItem()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string practiceno = SchoolHelper.GetStuPracticeNo(userid);
            var q = (from f in sc.StuEvaluateEnt where f.PracticeNo == practiceno select f).ToList();
            if (q.Count == 0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "数据出错，请刷新重试" });
            }
            for (int i = 0; i < q.Count; i++)
            {
                T_StuEvaluateEnt temp = q[i];
                sc.StuEvaluateEnt.Remove(temp);
            }
            sc.SaveChanges();
            return Redirect("/School/S_MyWorkPlat/StuEvaluateEnt");
        }

        [BaseActionFilter]
        public ActionResult StuEvaluateSchool(string StuID)
        {
            string userid = string.Empty;
            if (StuID == null)
            {
                userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            }
            else
            {
                userid = StuID;
                ViewBag.Foibid = "true";
            }
            var q_check = (from f in sc.StuBatchReg where f.UserID == userid select f).ToList();
            
            if (q_check[0].CareerStatusID < 13)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "当前状态下无法评价学校！" });
            }
            else
            {
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
                string practiceno = SchoolHelper.GetStuPracticeNo(userid);
                double Score = SchoolHelper.GetStuEvaEntSchoolScore(practiceno, "School");
                if (Score != 0.0)
                {
                    ViewBag.Score = Score;
                }
                var q = (from f in ctc.C_StuEvaluateSchoolItem orderby f.ItemPower where f.PracBatchID == prcbatchid select f).ToList();

                //星星个数
                var q_num = (from f in ctc.C_StuEvaSchoolGradeLevelItem orderby f.Weight where f.PracBatchID == prcbatchid select f).ToList();
                string[] star = new string[q.Count];
                string[] result = new string[q.Count];
                string[] judge = new string[q.Count];
                for (int i = 0; i < q.Count; i++)
                {
                    star[i] = "star" + i;
                    result[i] = "result" + i;
                    judge[i] = "judge" + i;
                }
                ViewBag.StuEvaluateSchoolItem = q;
                ViewBag.StuEvaSchoolGradeLevelItem = q_num;
                ViewBag.Star = star;
                ViewBag.Judge = judge;
                ViewBag.Result = result;
                ViewBag.ItemCount = q.Count;


                T_StuBatchReg tsb = sc.StuBatchReg.Find(practiceno);
                if (tsb == null)
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "数据出错！错误原因：该学生未被注册到批次" });
                }
                ViewBag.StuMessage2 = tsb.PracticeStartEnd;

                var q_stutowhere = (from f in sc.PracticeVolunteer where f.PracticeNo == practiceno && f.PreVolStatus == "1" && f.VolunteerStatus == 5 select f).FirstOrDefault();
                if (q_stutowhere == null)
                {
                    return RedirectToAction("Results", "SubmitResults", new { results = "数据出错,错误原因：该学生的终录信息有误" });
                }
                T_PracticeVolunteer tp = q_stutowhere;
                ViewBag.StuMessage = tp;

                //获取已评价列表
                var q_hasp = (from f in sc.StuEvaluateSchool where f.PracticeNo == practiceno select f).ToList();
                ViewBag.HasEvaList = q_hasp;
                return View();
            }
        }


        //添加评价学校项目
        [BaseActionFilter]
        public ActionResult AddStuEvaSchoolItem(string final)
        {
            if (final == null)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "数据异常！错误原因：评价列表为空" });
            }
            else
            {
                string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
                string[] result = final.Split('!');
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
                string pacticeno = SchoolHelper.GetStuPracticeNo(userid);
                var q = (from f in ctc.C_StuEvaluateSchoolItem orderby f.ItemSequence where f.PracBatchID == prcbatchid select f).ToList();

                var q_grade = (from f in ctc.C_StuEvaSchoolGradeLevelItem where f.PracBatchID == prcbatchid orderby f.Weight ascending select f).ToList();
                int index = 0;
                for (int i = 0; i < q.Count; i++)
                {
                    if (index >= result.Length - 1)
                    {
                        break;
                    }
                    else
                    {
                        T_StuEvaluateSchool ts = new T_StuEvaluateSchool();
                        ts.ItemNo = q[i].ItemNo;
                        //string temp = string.Empty;
                        var q_temp = (from f in q_grade where f.ItemName == result[index] select f.ItemNo).ToList();
                        if (q_temp.Count == 0)
                        {
                            var qqqq = (from f in q_grade orderby f.Weight descending select f.ItemNo).FirstOrDefault();
                            ts.ItemGrade = qqqq;
                        }
                        else
                        {
                            ts.ItemGrade = q_temp[0];
                        }
                        index++;
                        //if (result[index] == "")
                        //{
                        //temp = q_grade[q_grade.Count - 1].ItemNo;
                        //}
                        //else
                        //{
                        //int a = Convert.ToInt32(result[index++].Replace("0", ""));
                        //temp = q_grade[a - 1].ItemNo;
                        //}
                        ts.ItemContent = result[index];
                        ts.PracticeNo = pacticeno;
                        sc.StuEvaluateSchool.Add(ts);
                        index++;
                    }
                }
                sc.SaveChanges();

                double Score = SchoolHelper.GetStuEvaEntSchoolScore(pacticeno, "School");
                T_StuBatchReg tbr = (from f in sc.StuBatchReg where f.UserID == userid select f).FirstOrDefault();
                tbr.StuEvaSchoolScore = Score;
                sc.SaveChanges();

                return Content("<script>alert('评价成功提交!');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }

        }

        [BaseActionFilter]
        public ActionResult DeleteStuEvaSchoolItem()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //获取学校当前批次
            string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
            string practiceno = SchoolHelper.GetStuPracticeNo(userid);
            var q = (from f in sc.StuEvaluateSchool where f.PracticeNo == practiceno select f).ToList();
            if (q.Count == 0)
            {
                return RedirectToAction("Results", "SubmitResults", new { results = "数据出错，请刷新重试" });
            }
            for (int i = 0; i < q.Count; i++)
            {
                T_StuEvaluateSchool temp = q[i];
                sc.StuEvaluateSchool.Remove(temp);
            }
            sc.SaveChanges();
            return Redirect("/School/S_MyWorkPlat/StuEvaluateSchool");
        }


        [BaseActionFilter]
        public ActionResult StuList()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //var q0 = (from f in sc.School where f.SchoolID == schoolid select f.SchoolName).ToList();
            //ViewBag.SchoolName = q0[0].ToString();
            //ViewBag.SchoolID = schoolid;




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
                //获取年级
                List<SelectListItem> EntryYear = SchoolHelper.GetEntryYearList(schoolid, EntryYear1);
                ViewData["EntryYear"] = EntryYear;

                //获取专业
                List<SelectListItem> Specialty = SchoolHelper.GetSpecialtyList(schoolid, SpeNo);
                ViewData["Specialty"] = Specialty;

                //获取班级
                List<SelectListItem> Class = SchoolHelper.GetClassList(schoolid, ClassID);
                ViewData["Class"] = Class;

                //获取学校当前批次
                string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);

                var q_s = (from Stu in sc.Student from PracStuInBatch in sc.StuBatchReg where Stu.StuClass == ClassID && Stu.UserID == PracStuInBatch.UserID && PracStuInBatch.PracBatchID == prcbatchid select new { Stu.StuNo, Stu.StuName, PracStuInBatch.PracticeNo, Stu.UserID,PracStuInBatch.ReviewScore }).ToList();
                string[] StuNo = new string[q_s.Count];
                string[] StuName = new string[q_s.Count];
                string[] StuPracNo = new string[q_s.Count];
                string[] StuID = new string[q_s.Count];
                string[] StuReviewScore = new string[q_s.Count];
                //string[] StuEvaSchoolScore = new string[q_s.Count];
                //string[] StuEvaEntScore = new string[q_s.Count];
                //int[] LoopNum = new int[q_s.Count];

                for (int i = 0; i < q_s.Count; i++)
                {
                    StuNo[i] = q_s[i].StuNo;
                    StuName[i] = q_s[i].StuName;
                    StuPracNo[i] = q_s[i].PracticeNo;
                    StuID[i] = q_s[i].UserID;
                    StuReviewScore[i] = q_s[i].ReviewScore.ToString();
                    //string practiceno = q_s[i].UserID + prcbatchid;
                    //var q_v = (from f in sc.PracticeVolunteer where f.PracticeNo == practiceno select f).ToList();
                    //LoopNum[i] = q_v.Count;
                    //double temp = SchoolHelper.GetStuEvaEntSchoolScore(q_s[i].PracticeNo, "Ent");
                    //if(temp==0.0)
                    //{
                    //    StuEvaEntScore[i] = "未评价";
                    //}
                    //else
                    //{
                    //    StuEvaEntScore[i] = temp.ToString();
                    //}
                    //double temp1 = SchoolHelper.GetStuEvaEntSchoolScore(q_s[i].PracticeNo, "School");
                    //if (temp1 == 0.0)
                    //{
                    //    StuEvaSchoolScore[i] = "未评价";
                    //}
                    //else
                    //{
                    //    StuEvaSchoolScore[i] = temp.ToString();
                    //}
                }
                ViewBag.StuNo = StuNo;
                ViewBag.StuName = StuName;
                ViewBag.StuPracNo = StuPracNo;
                ViewBag.StuID = StuID;
                ViewBag.StuReviewScore = StuReviewScore;
                ViewBag.DataCount = q_s.Count;            

                return View();
            }

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

    }
}
