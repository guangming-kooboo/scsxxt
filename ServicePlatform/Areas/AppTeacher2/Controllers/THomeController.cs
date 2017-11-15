using Core.Interfaces;
using ServicePlatform.Areas.AppTeacher2.ViewModel.THome;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Core.Model;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Entity;
using Qx.Wbs.Hqu.Interfaces;
using ServicePlatform.Areas.WbsHqu.Controllers;

namespace ServicePlatform.Areas.AppTeacher2.Controllers
{
    public class THomeController : BaseAppTeacherController
    {
        //
        // GET: /AppTeacher/Home/
        private IWbsService _IWbs;
        private TAppInterface _TApp;
        private IWbsService _wbsServices;
        private IRepository<QuantifyTask> _quantifytask;
        private IRepository<QuotaTaskStaffDistribution> _quotataskdis;
        private IRepository<QuantifyTaskCompletion> _quantifytaskcom;
        private IRepository<ScoringRule> _scoringrule;
        private Repository Db;
        public THomeController(
            TAppInterface TApp,
            IWbsService IWbs,
            Repository _db,
            IWbsService wbsServices,
            IRepository<ScoringRule> scoringrule,
            IRepository<QuantifyTask> quantifytask,
            IRepository<QuotaTaskStaffDistribution> quotataskdis,
            IRepository<QuantifyTaskCompletion> quantifytaskcom
            )
        {
            _TApp = TApp;
            _IWbs = IWbs;
            Db = _db;
            _scoringrule = scoringrule;
            _wbsServices = wbsServices;
            _quantifytask = quantifytask;
            _quotataskdis = quotataskdis;
            _quantifytaskcom = quantifytaskcom;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            var teacher = _TApp.TeacherInfo(DataContext.UserID);
            return View(HomePage_M.ToViewModel(teacher));
        }
        #region 班级联动
        //根据学校取年级 HttpPost: /AppTeacher2/THome/GetGrades
        [HttpPost]
        public JsonResult GetGrades(string schoolId)
        {
            return Json(Db.C_Specialty.Where(m => m.SchoolID == schoolId).
                GroupBy(n => n.EntryYear).
                Select(g => new { Value = g.Key, Text = g.FirstOrDefault().EntryYear }).ToList());
        }

        //根据年级取专业 HttpPost: /AppTeacher2/THome/GetMajors
        [HttpPost]
        public JsonResult GetMajors(string schoolId, int gradeId)
        {
            return Json(Db.C_Specialty.Where(m => m.EntryYear == gradeId && m.SchoolID == schoolId).
                Select(item => new { Value = item.SpeNo, Text = item.SpeName }).ToList());
        }
        //根据专业取班级 HttpPost: /AppTeacher2/THome/GetClasss
        [HttpPost]
        public JsonResult GetClasss(string majorId)
        {
            return Json(Db.T_Class.Where(m => m.SpeNo == majorId).
                Select(item => new { Value = item.ClassID, Text = item.ClassName }).ToList());
        }
        #endregion
        //获取学生信息 HttpPost: /AppTeacher2/THome/GetStudentInfo
        [HttpPost]
        public JsonResult GetStudentInfo(string classid)
        {
            if (!classid.HasValue())
            {
                return Json(Db.T_Student.Select(b => new Student()
                {
                    StuNo = b.StuNo,
                    StuName = b.StuName,
                    Phone = b.StuCellphone,
                    StuSex = b.StuSex,
                    UserID = b.UserID,
                    Class = b.T_Class.ClassName
                    // EntEnterprise=_TApp.GetPracEntName(_TApp.GetPracticeNo(b.UserID)),
                }));
            }
            else
            {
                return Json(Db.T_Student.Where(a => a.StuClass == classid).Select(b => new Student()
                {
                    StuNo = b.StuNo,
                    Phone = b.StuCellphone,
                    StuName = b.StuName,
                    StuSex = b.StuSex,
                    UserID = b.UserID,
                    Class = b.T_Class.ClassName
                    //  EntEnterprise = _TApp.GetPracEntName(_TApp.GetPracticeNo(b.UserID)),
                }));
            }
        }

        public JsonResult MyGroupStudent(string data)
        {
            var param = data.Split(';');
            List<Student> students = new List<Student>();
            foreach (var item in param)
            {
                var model = Db.T_Student.FirstOrDefault(a => a.UserID == item);
                if (model != null)
                {
                    students.Add(new Student()
                    {
                        Class=model.T_Class.ClassName,
                        UserID = model.UserID,
                        StuName = model.StuName,
                        Phone = model.StuCellphone
                    });
                }
            }
            return Json(new { students = students }, JsonRequestBehavior.AllowGet);
        }

        #region 退出

        public ActionResult LogOut()
        {
            Session.Clear();
            return Redirect("/AppStudent/Visitor/Login");
        }

        #endregion

        public ActionResult StudentInfo()
        {
            try
            {
                var EntryYear = _TApp.GetEntryYear(DataContext.UserUnit);
                return View(StudentInfo_M.ToViewModel(EntryYear, DataContext.UserUnit,DataContext.UserID));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        public ActionResult StudentDetail(string uid)
        {
            try
            {
                var student = _TApp.StudentInfo(uid);
                return View(StudentDetail_M.ToViewModel(student));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }     
        }
        //记录工作量
        public ActionResult Record()
        {
            try
            {
                var tasks = _IWbs.GetTasks(DataContext.UserUnit);
                return View(Record_M.ToViewModel(tasks));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Record(Record_M form, string tasktype, string mytaskid, string note, HttpPostedFileBase[] uploaderInput)
        {
            try
            {
                var wbstaskid = form.WbsTaskID;
                var certificate = Lib.FileOperate.UploadMultiFile(uploaderInput, "/UserFiles/AppTeacher2/Files/Tesk/");
                _wbsServices.Record(wbstaskid, tasktype, mytaskid, note, certificate, DataContext.UserID);
                return WxAlert("该工作量已完成", "恭喜", "/AppTeacher2/THome/WorkLoad");
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        public ActionResult WorkLoad(string WbsTaskID, string type)
        {
            try
            {
                //var type = F("tasktype");
                var tasks = _IWbs.GetTasks(DataContext.UserUnit);
                var total = _IWbs.AppStaffSumary(WbsTaskID, DataContext.UserID, type);
                var tasklist = _IWbs.AppUserCheckRecord(WbsTaskID, DataContext.UserID, Convert.ToInt32(type));
                return View(WorkLoad_M.ToViewModel(tasks, total, tasklist, type));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }

        }
        public ActionResult MyTask()
        {
            return View(MyTask_M.ToViewModel(DataContext.UserID));
        }
        public ActionResult MsgDetail(string MsgID)
        {
            return View(MsgDetail_M.ToViewModel(DataContext.UserID,MsgID));
        }
        public ActionResult FAQ()
        {
            return View();
        }
        #region 企业
        //企业列表
        public ActionResult EnterpriseList(string EntCategoryID="")
        {
            var PositionCount = _TApp.GetPositionCount();
            var EntCategory = _TApp.EntCategory();
            var enterpriselist = _TApp.EntpriseList(DataContext.UserUnit,EntCategoryID);//含有EntPracNo
            return View(EnterpriseList_M.ToViewModel(enterpriselist, EntCategory, PositionCount));
        }
        //企业信息 (包含所有企业的栏目项)
        public ActionResult EnterpriseInfo(string Ent_No)
        {
            var enterprise = _TApp.EnterpriseDetail(Ent_No);
            return View(EnterpriseInfo_M.ToViewModel(enterprise));
        }
        //企业介绍
        public ActionResult EnterpriseIntroduce(string Ent_No)
        {
            var enterprise = _TApp.EnterpriseDetail(Ent_No);
            return View(EnterpriseIntroduce_M.ToViewModel(enterprise));
        }
        //企业岗位
        public ActionResult PositionDetail(string Ent_No)
        {
            var positionlist = _TApp.PositionList(Ent_No);
            return View(PositionDetail_M.ToViewModel(Ent_No, positionlist));
        }
        //培养计划
        public ActionResult RecEnt_TrainingPlan(string Ent_No, int DLFileColumnID)
        {
            var file = _TApp.GetDownLoadFiles(Ent_No, DLFileColumnID);
            return View(RecEnt_TrainingPlan_M.ToViewModel(Ent_No, file));
        }
        //成功案例
        public ActionResult RecEnt_SucceedCase(string Ent_No, int DLFileColumnID)
        {
            var file = _TApp.GetDownLoadFiles(Ent_No, DLFileColumnID);
            return View(RecEnt_SucceedCase_M.ToViewModel(Ent_No, file));
        }
        //员工薪资
        public ActionResult RecEnt_Salary(string Ent_No, int DLFileColumnID)
        {
            var file = _TApp.GetDownLoadFiles(Ent_No, DLFileColumnID);
            return View(RecEnt_Salary_M.ToViewModel(Ent_No, file));
        }
        //员工食宿
        public ActionResult RecEnt_StaffAcc(string Ent_No, int DLFileColumnID)
        {
            var file = _TApp.GetDownLoadFiles(Ent_No, DLFileColumnID);
            return View(RecEnt_StaffAcc_M.ToViewModel(Ent_No, file));
        }
        //员工天地
        public ActionResult RecEnt_Staff(string Ent_No, int DLFileColumnID)
        {
            var file = _TApp.GetDownLoadFiles(Ent_No, DLFileColumnID);
            return View(RecEnt_Staff_M.ToViewModel(Ent_No, file));
        }
        //资源下载
        public ActionResult RecEnt_Download(string Ent_No, int DLFileColumnID)
        {
            var file = _TApp.GetDownLoadFiles(Ent_No, DLFileColumnID);
            return View(RecEnt_Download_M.ToViewModel(Ent_No, file));
        }
        //疑问解答
        public ActionResult RecEnt_FAQ(string Ent_No, int DLFileColumnID)
        {
            var file = _TApp.GetDownLoadFiles(Ent_No, DLFileColumnID);
            return View(RecEnt_FAQ_M.ToViewModel(Ent_No, file));
        }
        public FilePathResult ExPort(string path, string filename)
        {
            var encoding = System.Text.Encoding.UTF8;
            Response.Charset = encoding.WebName;
            Response.HeaderEncoding = encoding;

            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", (Request.Browser.Browser == "IE") ? HttpUtility.UrlEncode(filename, encoding) : filename));

            //这里获取文件路径
            //var path = _goods.ToExcel(ReportExtraPara);
            //这里返回页面下载文件
            return File(path, filename);
        }
        #endregion
        public ActionResult TaskDetail(string finishid, int type, int state)
        {
            try
            {
                if (type == 0)
                {//定额
                    var taskdetail = _IWbs.AppQuotadis(finishid);
                    var taskname = _IWbs.AppQuotatask(taskdetail.QuotaTaskID, type);
                    var Name = taskdetail.QuotaTask.WbsTask.TaskName;
                    return View(TaskDetail_M.ToViewModel(taskdetail, type, state, taskname, Name));
                }
                else
                {//定量
                    var taskdetail = _IWbs.AppQuantifycom(finishid);
                    var taskname = _IWbs.AppQuotatask(taskdetail.ScoringRuleID, type);
                    var Name = taskdetail.QuantifyTask.WbsTask.TaskName;
                    return View(TaskDetail_M.ToViewModel(taskdetail, type, state, taskname, Name));
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        public ActionResult ForgetPsw()
        {
            return View();
        }
    }
}
