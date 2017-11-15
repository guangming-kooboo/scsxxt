//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

using Core.Services;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using Qx.Tools;
using ServicePlatform.Areas.App.ViewModel.Home;
using ServicePlatform.Areas.App.ViewModel.Visitor;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf.qrcode;
using Microsoft.Ajax.Utilities;
using Qx.Scsxxt.Core.Services.Permission;
using ServicePlatform.Config;

namespace ServicePlatform.Areas.App.Controllers
{
    public class OpenController : BaseAppController
    {
        //
        // GET: /App/Open/
            private IAppService _app;
            public OpenController(IAppService app)
            {
                _app = app;
            }
        public ActionResult ForgetPsw()
        {
            return View(ForgetPsw_M.ToViewModel(_app.GetSchoolIItems()));
        }
        [HttpPost]
        public ActionResult ForgetPsw(ForgetPsw_M form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Alert();
                }
                else
                {
                    var uid = F("UnitID") + "-" + F("UserID");
                    var pwd = F("UserPwd");
                    var SecondPwd = F("SecondPwd");
                    var StuName = F("StuName");
                    if (StuName == _app.GetStuName(uid))
                    {
                        if (pwd == SecondPwd)
                        {
                            if (_app.ForgetPsw(uid, pwd))
                            {
                                return Redirect("/App/Visitor/Login");
                            }
                            else
                            {
                                return Alert("操作错误");
                            }
                        }
                        else
                        {
                            return Alert("请检查密码", -1);
                        }
                    }
                    else
                    {
                        return Alert("名字错误，请检查", -1);
                    }
                }
            }
            catch (Exception ex)
            {
                return Alert(ex.Message);
            }
        }

        //public ActionResult Login()
        //{
        //    return View(Login_M.ToViewModel(_app.GetSchoolIItems()));
        //}
        //[HttpPost]
        //public ActionResult Login(Login_M form)
        //{
        //    var uid = F("UnitID") + "-" + F("UserID");
        //    var pwd = F("UserPwd");

        //    DataContext.SetFiled("UserID", uid); DataContext.SetFiled("UserPwd", pwd);
        //    var sucess = new UserServices().All(DataContext, "登录");
        //    if (sucess.Any())
        //    {
        //        #region 初始化用户信息
        //        var Vars = new ServicePlatform.Config.Vars(uid);
        //        Vars.Init();
        //        //Vars.msgCount = sc.T_MsgRec.Count(a => a.Receiver == form.UserID && a.ReadTime == -1);
        //        #endregion
        //        Session["Vars"] = Vars;
        //        #region 初始化扩展
        //        var IsLogin = (Session["Vars"] as Vars);
        //        var info = S<Core.Services.Entity.T_PracBatch>()
        //                 .All(o => o.IsActive == 1 && o.IsCurrentBatch == "是" && o.SchoolID == IsLogin.UserUnit)
        //                 .FirstOrDefault();
        //        var accountContext = new AccountContext();
        //        DataContext.UserID =
        //        accountContext.UserID = IsLogin.UserID;
        //        DataContext.UserName =
        //        accountContext.UserName =
        //        sucess.FirstOrDefault().NickName;
        //        DataContext.UserUnit = IsLogin.UserUnit;
        //        DataContext.PracBatchID = info == null ? "查找当前批次失败" : info.PracBatchID;

        //        var employee = S<Core.Services.Entity.T_Employee>().All().Where(a => a.UserID == DataContext.UserID).ToList();
        //        var enterprise = S<Core.Services.Entity.T_Enterprise>().All().Where(a => a.UserID == DataContext.UserID).ToList();
        //        var teacher = S<Core.Services.Entity.T_Faculty>().All().Where(a => a.UserID == DataContext.UserID).ToList();
        //        var student = S<Core.Services.Entity.T_Student>().All().Where(a => a.UserID == DataContext.UserID).ToList();
        //        var school = S<Core.Services.Entity.T_School>().All().Where(a => a.UserID == DataContext.UserID).ToList();
        //        var platformer = S<Core.Services.Entity.T_Platformer>().All().Where(a => a.UserID == DataContext.UserID).ToList();
        //        if (employee.Count > 0)
        //        {
        //            DataContext.IsEmployee = true;
        //        }
        //        if (enterprise.Count > 0)
        //        {
        //            DataContext.IsEnterprise = true;
        //        }
        //        if (teacher.Count > 0)
        //        {
        //            DataContext.IsTeacher = true;
        //        }
        //        if (student.Count > 0)
        //        {
        //            DataContext.IsStudent = true;
        //        }
        //        if (school.Count > 0)
        //        {
        //            DataContext.IsSchool = true;
        //        }
        //        if (platformer.Count > 0)
        //        {
        //            DataContext.IsPlatform = true;
        //        }
        //        #endregion

        //        DataContext.IsLogin = true;
        //        Session["AccountContext"] = accountContext;
        //        return Redirect("/App/Home/UserInfo?uid=" + uid);

        //    }
        //    else
        //    {
        //        return Json(new { result="false", msg="登录失败：用户名或密码错误！" });
        //    }

        //}

        //
        // GET: /App/Open/Login?uid=uid&psw=psw    
        public ActionResult Login(string uid,string psw)
        {
            if (_app.Login(DataContext, uid, psw))
            {
                return Json(
                    new
                    {
                        state = "200",
                        msg = "登陆成功"
                    },JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(
                    new
                    {
                        state = "403",
                        msg = "登陆失败"
                    },JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Resume(string uid)
            {
                var PracticeNo = _app.GetPracticeNoByUserID(uid);
                return View(Resume_M.ToViewModel(uid, _app.GetResumes(PracticeNo)));
            }

            public ActionResult AnswerQuestions(string Ent_No)
            {
                var AnswerQuestions = _app.GetAnswerQuestions(Ent_No);
                return View();
            }
            //所有企业
            public ActionResult Indexof_TrainingPlan(string uid, string Ent_No, int DLFileColumnID)
            {
                return View(IndexofDownLoadFiles_M.ToViewModel(uid, Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            public ActionResult Indexof_Salary(string uid, string Ent_No, int DLFileColumnID)
            {
                return View(IndexofDownLoadFiles_M.ToViewModel(uid, Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            public ActionResult Indexof_StaffAcc(string uid, string Ent_No, int DLFileColumnID)
            {
                return View(IndexofDownLoadFiles_M.ToViewModel(uid, Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            public ActionResult Indexof_Staff(string uid, string Ent_No, int DLFileColumnID)
            {
                return View(IndexofDownLoadFiles_M.ToViewModel(uid, Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            public ActionResult Indexof_SucceedCase(string uid, string Ent_No, int DLFileColumnID)
            {
                return View(IndexofDownLoadFiles_M.ToViewModel(uid, Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            public ActionResult Indexof_Download(string uid, string Ent_No, int DLFileColumnID)
            {
                return View(IndexofDownLoadFiles_M.ToViewModel(uid, Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            //招聘企业
            public ActionResult InfoIndexof_TrainingPlan(string uid, string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
            {
                var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
                return View(InfoIndexofDownLoadFiles_M.ToViewModel(uid, EntPracNo, VolunteerSequence, PosSequence, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }

            public ActionResult InfoIndexof_Salary(string uid, string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
            {
                var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
                return View(InfoIndexofDownLoadFiles_M.ToViewModel(uid, EntPracNo, VolunteerSequence, PosSequence, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            //食宿

            public ActionResult InfoIndexof_StaffAcc(string uid, string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
            {
                var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
                return View(InfoIndexofDownLoadFiles_M.ToViewModel(uid, EntPracNo, VolunteerSequence, PosSequence, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            //员工天地

            public ActionResult InfoIndexof_Staff(string uid, string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
            {
                var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
                return View(InfoIndexofDownLoadFiles_M.ToViewModel(uid, EntPracNo, VolunteerSequence, PosSequence, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }

            public ActionResult InfoIndexof_SucceedCase(string uid, string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
            {
                var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
                return View(InfoIndexofDownLoadFiles_M.ToViewModel(uid, EntPracNo, VolunteerSequence, PosSequence, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            public ActionResult InfoIndexof_Download(string uid, string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
            {
                var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
                return View(InfoIndexofDownLoadFiles_M.ToViewModel(uid, EntPracNo, VolunteerSequence, PosSequence, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
            }
            //实习总览
            public ActionResult PracticeOverView(string uid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var EntCount = _app.GetEnterpriseCount();
                        var YetSubmitCount = _app.YetSubmit(uid);
                        // var PracticeNo = _app.GetPracticeNoByUserID(uid);
                        var FinalVolunteerCount = _app.GetFinalVolunteerCount(uid);
                        if (FinalVolunteerCount != 0)
                        {
                            var FormalVolunteer = _app.GetFormaVolunteer(uid);
                        }
                        var PrepareVolunteerCount = _app.GetPerparVolunteerCount(uid);
                        var FormalVolunteerCount = _app.GetFormalVolunteerCount(uid);
                        var CareerStatus = _app.GetCareerStatus(uid);
                        var DiaryCount = _app.GetDiaryCount(uid);
                        var PracticeCaseCount = _app.GetCaseCount(uid);
                        var AllComment = _app.AllComment(uid);
                        var EvaluateEntCount = _app.GetEvaluateEntCount(uid);
                        var EvaluateEnt = _app.GetStuEvaluateEnt(uid);
                        var EvaluateSchool = _app.GetStuEvaluateSchool(uid);
                        var EntFindStu = _app.GetEntFindStu(uid);
                        var StuFindEnt = _app.GetStuFindEnt(uid);
                        if (FinalVolunteerCount != 0)
                        {
                            return View(PracticeOverView_M.ToViewModel(
                                uid,
                                YetSubmitCount,
                                EntCount,
                                FinalVolunteerCount,
                                PrepareVolunteerCount,
                                FormalVolunteerCount,
                                _app.GetFormaVolunteer(uid),
                                CareerStatus,
                                DiaryCount,
                                PracticeCaseCount,
                                AllComment,
                                EvaluateEntCount,
                                EvaluateEnt,
                                EvaluateSchool,
                                EntFindStu,
                                StuFindEnt
                                ));
                        }
                        else
                        {
                            return View(PracticeOverView_M.ToViewModel(
                               uid,
                               YetSubmitCount,
                               EntCount,
                               FinalVolunteerCount,
                               PrepareVolunteerCount,
                               FormalVolunteerCount,
                               CareerStatus,
                               DiaryCount,
                               PracticeCaseCount,
                               AllComment,
                               EvaluateEntCount,
                               EvaluateEnt,
                               EvaluateSchool,
                               EntFindStu,
                               StuFindEnt
                               ));
                        }
                    }
                    else
                    {
                        return Alert();
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            public ActionResult CheckReport(string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.GetVolunteerCount(uid) != 0)
                        {
                            if (_app.CheckVolunStatus(uid))//判断是否已经被岗位录取了
                            {
                                if (_app.ReportContent(uid) == null)
                                {
                                    return Redirect("/App/Home/AddPracticeReport?uid=" + uid);
                                }
                                else
                                {
                                    return Redirect("/App/Home/PracticeReport?uid=" + uid);
                                }
                            }
                            else
                            {
                                return Alert();
                            }
                        }
                        else
                            return Alert();
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }

            }
            public ActionResult AddPracticeReport(string uid)
            {
                return View(AddPracticeReport_M.ToViewModel(uid));
            }
            [HttpPost]
            public ActionResult AddPracticeReport(AddPracticeReport_M form, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        form.UserID = uid;
                        if (_app.AddReport(form.ToModel()))
                        {
                            return Redirect("/App/Home/PracticeReport?uid=" + uid);
                        }
                        else
                        {
                            return Alert("添加失败！", -1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Alert("请联系开发人员\n" + ex.Message, -1);
                }

            }
            public ActionResult PracticeReport(string uid)
            {
                return View(PracticeReport_M.ToViewModel(_app.AllComment(uid), uid));
            }
            public ActionResult UserInfo(string uid)
            {
                //     DataContext.PracBatchID
                var student = _app.FindStudent(uid);//T_Student
                if (student.StuCellphone == null)//没有录入自己的详细数据
                {
                    return View(UserInfo_M.ToViewModel(uid));
                }
                else
                    return Redirect("/App/Home/Main?uid=" + uid);
            }
            [HttpPost]
            public ActionResult UserInfo(UserInfo_M form, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        var coveredTime = DateTime.Parse(form.StuBirthday);//转化类型
                                                                           //var student =_app.FindStudent(uid);//T_Student
                        var stu = _app.FindStu(uid);//T_User            
                        UserInfo userinfo = new UserInfo();
                        userinfo = form.ToModel();
                        if (_app.UpdateInfo(DataContext, userinfo))
                        {
                            //return Alert("保存成功！");
                            return Redirect("/App/Home/Main?uid=" + uid);
                        }
                        else
                            return Alert("保存失败！", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert("请联系开发人员\n" + ex.Message, -1);
                }
            }
            //修改信息
            public ActionResult ReviseInfo(string uid)
            {
                return View(ReviseInfo_M.ToViewModel(_app.GetPersonalInfo(uid)));
            }
            [HttpPost]
            public ActionResult ReviseInfo(ReviseInfo_M form, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        var coveredTime = DateTime.Parse(form.StuBirthday);//转化类型
                                                                           //var student =_app.FindStudent(uid);//T_Student
                                                                           //  var stu = _app.FindStu(uid);//T_User            
                        UserInfo userinfo = new UserInfo();
                        userinfo = form.ToModel();
                        if (_app.UpdateInfo(DataContext, userinfo))
                        {
                            //return Alert("保存成功！");
                            return Redirect("/App/Home/PersonalSpace?uid=" + uid);
                        }
                        else
                            return Alert("保存失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert("请联系开发人员\n" + ex.Message, -1);
                }
            }
            //修改密码
            public ActionResult RevisePsw(string uid)
            {
                return View(RevisePsw_M.ToViewModel(uid));
            }
            [HttpPost]
            public ActionResult RevisePsw(RevisePsw_M form, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        UserInfo userinfo = new UserInfo();
                        userinfo = form.ToModel();
                        //var stu = _app.FindStu(uid);//T_User
                        if (_app.CheckPsw(form.OldPwd, uid))//检查现有密码是否和之前密码匹配
                        {
                            if (form.NewPwd == form.SecondPwd)
                            {
                                if (_app.RevisePsw(DataContext, userinfo))
                                {
                                    // return Alert("修改成功！")
                                    return Redirect("/App/Visitor/Login");
                                }
                                else
                                {
                                    return Alert("修改失败！", -1);
                                }
                            }
                            else
                                return Alert("请检查新密码！", -1);
                        }
                        else
                        {
                            return Alert("旧密码不对，请重新输入！", -1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Alert("请联系开发人员\n" + ex.Message, -1);
                }
            }
            public ActionResult Main(string uid, string EntCategoryID)
            {
                var EntCount = _app.GrogshopsCount(EntCategoryID);
                var AllCount = _app.GetEnterpriseCount();
                return View(Main_M.ToViewModel(uid, AllCount, EntCount));
            }
            //个人主页
            public ActionResult PersonalSpace(string uid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var PerVolCount = _app.GetPerparVolunteerCount(uid);
                        var ForVolCount = _app.GetFormalVolunteerCount(uid);
                        var StuInfo = _app.StuAllInfo(uid);
                        return View(PersonalSpace_M.ToViewModel(uid, PerVolCount, ForVolCount, StuInfo));
                    }
                    else
                    {
                        return Alert();
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }

            }
            //判断该学生的志愿状态
            public ActionResult CheckDiary(string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.GetVolunteerCount(uid) != 0)
                        {
                            if (_app.CheckVolunStatus(uid))//判断是否已经被岗位录取了
                            {
                                if (_app.GetDiaryCount(uid) == 0)//判断周记的数量
                                {
                                    return Redirect("/App/Home/Diary?uid=" + uid);
                                }
                                else
                                    return Redirect("/App/Home/DiaryList?uid=" + uid);
                            }
                            else
                            {
                                return Alert();
                            }
                        }
                        else
                            return Alert();
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }

            }
            //周记
            public ActionResult Diary(string uid)
            {
                return View(Diary_M.ToViewModel(uid));
            }
            public ActionResult AddDiary(string uid)
            {
                return View(AddDiary_M.ToViewModel(uid, _app.GetDiaryCount(uid)));
            }
            [HttpPost]
            public ActionResult AddDiary(HttpPostedFileBase[] file_head, AddDiary_M form, string uid)
            {
                //1. View中设置input标签 type=file name=file   
                //2.调用  Lib.FileOperate.UploadFile(file, "/UserFiles/Test/"); 获得返回值
                //3.将2的返回值给数据库     
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        Diary diary = new Diary();
                        Random random = new Random();
                        form.PracticeNo = _app.GetPracticeNoByUserID(uid);
                        form.RecordNo = random.Next();
                        form.RecordDate = Lib.DateTimeFormat.ConvertDateTimeInt(DateTime.Now);//把datetime型的时间转化为int      
                                                                                              //form.RecordDate =DateTime.Now;
                        form.Pic = Lib.FileOperate.UploadMultiFile(file_head, "/UserFiles/APP/Files/WeekRecords/");
                        diary = form.ToModel();
                        if (_app.AddDiary(diary))
                        {
                            // return Alert(" 保存成功");
                            return Redirect("/App/Home/DiaryList?uid=" + uid);
                        }
                        else
                            return Alert("保存失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert("请联系开发人员\n" + ex.Message, -1);
                }
            }
            public ActionResult EditDiary(string uid, int recordno)
            {
                return View(EditDiary_M.ToViewModel(_app.GetDiary(uid, recordno), _app.GetDiaryCount(uid)));
            }
            [HttpPost]
            public ActionResult EditDiary(EditDiary_M form, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        Diary diary = new Diary();
                        form.RecordDate = Lib.DateTimeFormat.ConvertDateTimeInt(DateTime.Now);//把datetime型的时间转化为int
                                                                                              // form.RecordDate=DateTime.Now;
                        diary = form.ToModel();
                        if (_app.EditDiary(diary))
                        {
                            // return Alert("修改成功");
                            return Redirect("/App/Home/DiaryList?uid=" + uid);
                        }
                        else
                            return Alert("修改失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            public ActionResult DeleteDiary(string PracticeNo, int recordno, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.DeleteDiary(PracticeNo, recordno))
                        {
                            return Redirect("/App/Home/DiaryList?uid=" + uid);
                        }
                        else
                            return Alert("删除失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert("请联系开发人员\n" + ex.Message, -1);
                }
            }
            //周记列表
            public ActionResult DiaryList(string uid)
            {
                if (_app.GetDiaryCount(uid) == 0)
                {
                    return Redirect("/App/Home/Diary?uid=" + uid);
                }
                else
                {
                    //var CaseCount = _app.GetCaseCount(uid);
                    var DiaryCount = _app.GetDiaryCount(uid);
                    var Diary = _app.GetDiarys(uid);
                    return View(DiaryList_M.ToViewModel(Diary, uid, DiaryCount));
                }
            }
            //判断用户是否已经写过案例了
            public ActionResult CheckCaseCount(string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.GetVolunteerCount(uid) != 0)
                        {
                            if (_app.CheckVolunStatus(uid)) //判断是否已经被岗位录取了
                            {
                                if (_app.GetCaseCount(uid) == 0) //判断案例的数量
                                {
                                    return Redirect("/App/Home/Case?uid=" + uid);
                                }
                                else
                                    return Redirect("/App/Home/PracticeCaseList?uid=" + uid);
                            }
                            else
                            {
                                return Alert();
                            }
                        }
                        else
                            return Alert();
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            public ActionResult Case(string uid)
            {
                return View(Case_M.ToViewModel(uid));
            }
            //添加具体案例
            public ActionResult AddPracticeCase(string uid)
            {
                //通过用户ID，查找实学的批次，再查到该批次所对应案例ItemNo（ItemNo：每一个案例下面所对应的具体的几个小分支）
                try
                {
                    if (ModelState.IsValid)
                    {
                        var caseitemno = _app.GetItemNobyUserID(uid);
                        return View(AddPracticeCase_M.ToViewModel(uid, caseitemno, _app.GetCaseCount(uid)));
                    }
                    else
                    {
                        return Alert();
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            [HttpPost]
            public ActionResult AddPracticeCase(HttpPostedFileBase[] file_head, List<string> ItemNo, List<string> ItemContent, string uid, string CaseName)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        Random reRandom = new Random();
                        var no = reRandom.Next();
                        var pic = Lib.FileOperate.UploadMultiFile(file_head, "/UserFiles/APP/Files/PracticeCases/");
                        var PracticeNo = _app.GetPracticeNoByUserID(uid);
                        List<PracticeCase> practicecases = new List<PracticeCase>();
                        for (int i = 0; i < ItemNo.Count(); i++)
                        {
                            PracticeCase practicecase = new PracticeCase();
                            practicecase.PracticeNo = PracticeNo;
                            practicecase.CaseNo = no;
                            practicecase.ItemNo = ItemNo[i];
                            if (CaseName != "")
                            {
                                practicecase.CaseName = CaseName;
                            }
                            else
                            {
                                return Alert("请填写标题", -1);
                            }
                            if (ItemContent[i] != "")
                            {
                                practicecase.ItemContent = ItemContent[i];
                            }
                            else
                            {
                                return Alert("请填写内容", -1);
                            }
                            practicecase.CaseTime = DateTime.Now;
                            practicecase.Pic = pic;
                            practicecases.Add(practicecase);
                        }
                        if (_app.AddPracticeCase(practicecases))
                        {
                            return Redirect("/App/Home/PracticeCaseList?uid=" + uid);
                        }
                        return Alert("添加失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            //编辑修改案例
            public ActionResult EditPracticeCase(string uid, int CaseNo, string CaseName)
            {
                var Case = _app.GetCase(uid, CaseNo);
                return View(EditPracticeCase_M.ToViewModel(Case, CaseNo, CaseName, _app.GetCaseCount(uid)));
            }
            [HttpPost]
            public ActionResult EditPracticeCase(List<string> ItemNo, List<string> ItemContent, string uid, string CaseName, List<int> CaseNo)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        var PracticeNo = _app.GetPracticeNoByUserID(uid);
                        List<PracticeCase> practicecases = new List<PracticeCase>();
                        for (int i = 0; i < ItemNo.Count(); i++)
                        {
                            PracticeCase practicecase = new PracticeCase();
                            practicecase.PracticeNo = PracticeNo;
                            practicecase.ItemNo = ItemNo[i];
                            practicecase.CaseNo = CaseNo[i];
                            practicecase.ItemContent = ItemContent[i];
                            practicecase.CaseName = CaseName;
                            practicecase.CaseTime = DateTime.Now;
                            practicecases.Add(practicecase);
                        }
                        if (_app.AddPracticeCase(practicecases))
                        {
                            return Redirect("/App/Home/PracticeCaseList?uid=" + uid);
                        }
                        return Alert("添加失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            //删除案例
            public ActionResult DeletePracticeCase(string uid, int CaseNo)
            {
                if (_app.DeleteCase(uid, CaseNo))
                {
                    return Alert("删除成功", "/App/Home/PracticeCaseList?uid=" + uid);
                }
                else
                    return Alert("删除失败1", -1);
            }
            //案例列表
            public ActionResult PracticeCaseList(string uid)
            {
                if (_app.GetCaseCount(uid) == 0)
                {
                    return Redirect("/App/Home/Case?uid=" + uid);
                }
                else
                {
                    var CaseCount = _app.GetCaseCount(uid);
                    //  var DiaryCount = _app.GetDiaryCount(uid);
                    var PracticeNo = _app.GetPracticeNoByUserID(uid);
                    var practicecase = _app.GetPraceticeCase(PracticeNo);
                    return View(PracticeCaseList_M.ToViewModel(practicecase, uid, CaseCount));
                }
            }
            //检查学生是否在该批次
            public ActionResult CheckStuBatch(string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.GetCareerStatus(uid) == 10)//查看学生是否正处在报名阶段
                        {
                            var PracticeNo = _app.GetPracticeNoByUserID(uid);
                            if (PracticeNo != null)
                            {
                                return Redirect("/App/Home/PrepareVolunteer?uid=" + uid);
                            }
                            else
                            {
                                return Alert();
                            }
                        }
                        else
                        {
                            return Alert("报名已结束", -1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }

            //所有企业列表
            public ActionResult AllEnterprise(string uid, string EntCategoryID)
            {
                var count = _app.GetPositionCount();
                var enterprises = _app.GetEnterprises(EntCategoryID);
                var EntCategory = _app.EntCategory();
                //var   enterprises.Select(a => GetEntPic(a.Ent_No));
                return View(AllEnterprise_M.ToViewModel(enterprises, uid, EntCategoryID, count, EntCategory));
            }

            public ActionResult EnterpriseIndex(string Ent_No, string uid)
            {
                var enterprise = _app.SingleEnterprise(Ent_No);
                return View(EnterpriseIndex_M.ToViewModel(enterprise, Ent_No, uid));
            }
            public ActionResult EnterpriseInfoIndex(string uid, string EntPracNo, int VolunteerSequence, int PosSequence)
            {
                var Enterprise = _app.Enterprise(EntPracNo);
                return View(EnterpriseInfoIndex_M.ToViewModel(Enterprise, EntPracNo, uid, VolunteerSequence, PosSequence));
            }
            //单个企业详情
            public ActionResult SingleEnterprise(string Ent_No, string uid)
            {
                //获取企业的轮播图
                //DataContext.SetFiled("UnitID", Ent_No);
                //DataContext.SetFiled("Ent_No", Ent_No);
                //var AdList = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的轮播广告图");
                var enterprise = _app.SingleEnterprise(Ent_No);
                return View(SingleEnterprise_M.ToViewModel(enterprise, Ent_No, uid));
            }

            public ActionResult CheckEntBatch(string Ent_No, string uid)
            {
                var entbatch = _app.CheckEntBatch(Ent_No, DataContext);
                if (entbatch == true)
                {
                    return RedirectToAction("ShowPost", "Home", new { Ent_No = Ent_No, uid = uid });
                }
                else
                {
                    return Alert("该批次暂无岗位", -1);
                }
            }
            //只能看的岗位详情
            public ActionResult ShowPost(string Ent_No, string uid)
            {
                var EntPracNo = _app.GetEntPracNoByEntNo(Ent_No);
                var job = _app.GetJobs(EntPracNo);
                return View(ShowPost_M.ToViewModel(job, Ent_No, uid));
            }
            //删除志愿   

            public ActionResult DeleteVolunteer(string uid, string PracticeNo, string EntPracNo, string PosID)
            {
                if (_app.DeleteVolunteer(PracticeNo, EntPracNo, PosID))//bool型
                {
                    return Alert("删除成功", "/App/Home/PrepareVolunteer?uid=" + uid);
                }
                else
                    return Alert("删除失败", -1);
            }
            public ActionResult DeleteFormalVolunteer(string uid, string PracticeNo, string EntPracNo, string PosID)
            {
                if (_app.DeleteVolunteer(PracticeNo, EntPracNo, PosID))//bool型
                {
                    return Alert("删除成功", "/App/Home/FormalVolunteer?uid=" + uid);
                }
                else
                    return Alert("删除失败", -1);
            }
            //确定志愿
            public ActionResult ConfirVolunteer(string uid, string PracticeNo, string EntPracNo, string PosID)
            {
                var count = _app.GetFormalVolunteerCount(uid);
                var volunteer = _app.GetVolunteer(PracticeNo, EntPracNo, PosID);
                if (volunteer.PreVolStatus == "1")//判断是否已经添加过该志愿了
                {
                    return Alert("该志愿已经添加，请勿重复添加", -1);
                }
                else
                {
                    if (count < 4)
                    {
                        if (_app.ConfirVolunteer(PracticeNo, EntPracNo, PosID))//bool型
                        {
                            return Redirect("/App/Home/PrepareVolunteer?uid=" + uid);
                        }
                        else
                            return Alert("添加失败", -1);
                    }
                    else
                        return Alert("志愿已经添加满了", -1);
                }
            }
            //预报名志愿
            public ActionResult PrepareVolunteer(string uid)
            {
                var Status = _app.GetCareerStatus(uid);
                var allVolunteer = _app.GetVolunteers(uid);//读取我的所有志愿
                return View(PrepareVolunteer_M.ToViewModel(uid, allVolunteer, Status));
            }
            //[HttpPost]
            //public ActionResult PrepareVolunteer(PrepareVolunteer_M form, string uid, string PracticeNo, string EntPracNo, string PosID)
            //{
            //    //判断志愿是不是只有四个
            //    var count = _app.GetVolunteers(uid).Count ;//计算志愿数
            //    if (count > 4)
            //    {
            //        return Alert("志愿数太多，请再次确认志愿！");
            //    }
            //    else
            //    {
            //        if (_app.ConfirVolunteer(PracticeNo, EntPracNo, PosID))
            //        {
            //            return Alert("添加成功！");
            //        }
            //        else
            //            return Alert("添加失败！");
            //    }
            //}
            //正式志愿
            public ActionResult FormalVolunteer(string uid)
            {
                var CareerStatus = _app.GetCareerStatus(uid);
                var volunteer = _app.GetVolunteers(uid);
                return View(FormalVolunteer_M.ToViewModel(uid, volunteer, CareerStatus));
            }
            public ActionResult DetailPrepareVolunteer(string PracticeNo, string EntPracNo, string PosID, string uid)
            {
                var Volunteer = _app.GetVolunteerDetail(PracticeNo, EntPracNo, PosID);
                return View(DetailPrepareVolunteer_M.ToViewModel(Volunteer, uid));
            }
            public ActionResult DetailFormalVolunteer(string PracticeNo, string EntPracNo, string PosID, string uid)
            {
                var Volunteer = _app.GetVolunteerDetail(PracticeNo, EntPracNo, PosID);
                return View(DetailFormalVolunteer_M.ToViewModel(Volunteer, uid));
            }
            //所有招聘企业资讯 ,列表
            public ActionResult EnterpriseInfo(string uid, int VolunteerSequence, int PosSequence, string EntCategoryID)
            {
                ViewData["CurrentUrl"] = CurrentUrl();
                // var PicList =new List<T_DownLoadFiles>();
                var Grogshop = _app.GetGrogshops(EntCategoryID);
                var PracticeNo = _app.GetPracticeNoByUserID(uid);
                var count = _app.GetPositionCount();
                var EntCategory = _app.EntCategory();
                //Grogshop.Where(a => true).Select(b => _app.GetEntNoByEntPracNo(b.EntPracNo)).ToList().
                //    ForEach(c =>
                //   {
                //       DataContext.SetFiled("UnitID", c);
                //       PicList.Add(S<T_DownLoadFiles>().All(DataContext, "某单位的介绍图集").FirstOrDefault());
                //   });
                return View(EnterpriseInfo_M.ToViewModel(Grogshop, EntCategoryID, EntCategory,/*PicList, */uid, VolunteerSequence, PosSequence, count));
            }

            //单个招聘企业详情页面
            public ActionResult Grogshop(string uid, string EntPracNo, int VolunteerSequence, int PosSequence)
            {
                //var Grogshop = _app.GetGrogshops();
                //var EntNo = _app.GetEntNoByEntPracNo(EntPracNo);
                //DataContext.SetFiled("UnitID", EntNo);
                //DataContext.SetFiled("Ent_No", EntNo);

                var Enterprise = _app.Enterprise(EntPracNo);
                //var job = _app.GetJobs(EntPracNo);
                ////获取企业的轮播图
                //var AdList = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的轮播广告图");

                return View(Grogshop_M.ToViewModel(uid, EntPracNo, VolunteerSequence, PosSequence, Enterprise));
            }
            //岗位详情
            public ActionResult Post(string uid, string EntPracNo, int VolunteerSequence, int PosSequence)
            {
                var job = _app.GetJobs(EntPracNo);
                return View(Post_M.ToViewModel(job, uid, EntPracNo, VolunteerSequence, PosSequence));
            }
            [HttpPost]
            public ActionResult Post(Post_M form, string uid, string EntPracNo, int VolunteerSequence, int PosSequence)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        var PosID = form.PosID;
                        var PracticeNo = _app.GetPracticeNoByUserID(uid);
                        if (_app.GetPrepareVolunteer(PracticeNo, EntPracNo, PosID))
                        {
                            if (_app.AddVolunteer(PracticeNo, EntPracNo, PosID, VolunteerSequence, PosSequence))
                            {
                                return Alert("添加志愿成功！", -1);
                            }
                            else
                                return Alert("志愿添加失败！", -1);
                        }
                        else
                            return Alert("该志愿已经添加", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }

            public ActionResult DetailEvaluateSchool(string uid)
            {
                var evaluateschool = _app.GetStuEvaluateSchool(uid);
                return View(DetailEvaluateSchool_M.ToViewModel(evaluateschool, uid));
            }
            public ActionResult EditDetailEvaluateSchool(string uid)
            {
                var evaluateschool = _app.GetStuEvaluateSchool(uid);
                //  var EvaluateSchoolItem = _app.GetEvaluateSchoolItem(_app.GetPracBatchIDByUserID(uid));
                var _StuEvaSchoolGradeLevelItem = _app.GetStuEvaSchoolGradeLevelItem(_app.GetPracBatchIDByUserID(uid));
                return View(EditDetailEvaluateSchool_M.ToViewModel(evaluateschool, _StuEvaSchoolGradeLevelItem, uid));
            }
            [HttpPost]
            public ActionResult EditDetailEvaluateSchool(List<string> ItemNo, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        var PracticeNo = _app.GetPracticeNoByUserID(uid);
                        List<StuEvaluateSchool> EvaluateSchools = new List<StuEvaluateSchool>();
                        for (int i = 0; i < ItemNo.Count(); i++)
                        {
                            StuEvaluateSchool EvaluateSchool = new StuEvaluateSchool();
                            EvaluateSchool.PracticeNo = PracticeNo;
                            EvaluateSchool.ItemNo = ItemNo[i];
                            EvaluateSchool.ItemContent = F("Content-" + ItemNo[i]);
                            EvaluateSchool.ItemGrade = F("DropDown-" + ItemNo[i]);
                            EvaluateSchools.Add(EvaluateSchool);
                        }
                        if (_app.AddStuEvaluateSchool(EvaluateSchools))
                        {
                            //return Alert("添加成功");
                            return Redirect("/App/Home/DetailEvaluateSchool?uid=" + uid);
                        }
                        return Alert("添加失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            public ActionResult EvaluateSchool(string uid)
            {
                if (_app.GetCareerStatus(uid) == 14 || _app.GetCareerStatus(uid) == 15) //判断是否已经实习结束
                {
                    if (_app.GetEvaluateSchoolCount(uid) == 0)
                    {
                        var EvaluateSchoolItem = _app.GetEvaluateSchoolItem(_app.GetPracBatchIDByUserID(uid));
                        var _StuEvaSchoolGradeLevelItem = _app.GetStuEvaSchoolGradeLevelItem(_app.GetPracBatchIDByUserID(uid));
                        return View(EvaluateSchool_M.ToViewModel(EvaluateSchoolItem, _StuEvaSchoolGradeLevelItem, uid));
                    }
                    else
                    {
                        return Redirect("/App/Home/DetailEvaluateSchool?uid=" + uid);
                    }
                }
                else
                {
                    return Alert("还不能评价呢", -1);
                }
            }
            [HttpPost]
            public ActionResult EvaluateSchool(List<string> ItemNo, List<string> ItemContent, List<string> ItemGrade, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        var PracticeNo = _app.GetPracticeNoByUserID(uid);
                        List<StuEvaluateSchool> EvaluateSchools = new List<StuEvaluateSchool>();
                        for (int i = 0; i < ItemNo.Count(); i++)
                        {
                            StuEvaluateSchool EvaluateSchool = new StuEvaluateSchool();
                            EvaluateSchool.PracticeNo = PracticeNo;
                            EvaluateSchool.ItemNo = ItemNo[i];
                            EvaluateSchool.ItemContent = ItemContent[i];
                            EvaluateSchool.ItemGrade = ItemGrade[i];
                            EvaluateSchools.Add(EvaluateSchool);
                        }
                        if (_app.AddStuEvaluateSchool(EvaluateSchools))
                        {
                            //return Alert("添加成功");
                            return Redirect("/App/Home/DetailEvaluateSchool?uid=" + uid);
                        }
                        return Alert("添加失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            public ActionResult DetailEvaluateEnterprise(string uid)
            {
                var PracticeNo = _app.GetPracticeNoByUserID(uid);
                var EntName = _app.GetEntName(PracticeNo);
                var evaluateent = _app.GetStuEvaluateEnt(uid);
                return View(DetailEvaluateEnterprise_M.ToViewModel(evaluateent, uid, EntName));
            }
            public ActionResult EditDetailEvaluateEnterprise(string uid)
            {
                var PracticeNo = _app.GetPracticeNoByUserID(uid);
                var EntName = _app.GetEntName(PracticeNo);
                var evaluateent = _app.GetStuEvaluateEnt(uid);
                var StuEvaEntGradeLevelItem = _app.GetStuEvaEntGradeLevelItem(_app.GetPracBatchIDByUserID(uid));
                return View(EditDetailEvaluateEnterprise＿Ｍ.ToViewModel(evaluateent, uid, EntName, StuEvaEntGradeLevelItem));
            }
            [HttpPost]
            public ActionResult EditDetailEvaluateEnterprise(List<string> ItemNo, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        var PracticeNo = _app.GetPracticeNoByUserID(uid);
                        List<StuEvaluateEnt> EvaluateEnts = new List<StuEvaluateEnt>();
                        for (int i = 0; i < ItemNo.Count(); i++)
                        {
                            StuEvaluateEnt EvaluateEnt = new StuEvaluateEnt();
                            EvaluateEnt.PracticeNo = PracticeNo;
                            EvaluateEnt.ItemNo = ItemNo[i];
                            EvaluateEnt.ItemContent = F("Content-" + ItemNo[i]);
                            EvaluateEnt.ItemGrade = F("DropDown-" + ItemNo[i]);
                            EvaluateEnts.Add(EvaluateEnt);
                        }
                        if (_app.AddStuEvaluateEnt(EvaluateEnts))
                        {
                            //return Alert("添加成功");
                            return Redirect("/App/Home/DetailEvaluateEnterprise?uid=" + uid);
                        }
                        return Alert("添加失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            public ActionResult EvaluateEnterprise(string uid)
            {
                if (_app.GetCareerStatus(uid) == 14 || _app.GetCareerStatus(uid) == 15) //判断是否已经实习结束
                {
                    if (_app.GetEvaluateEntCount(uid) == 0)
                    {
                        var EntName = _app.GetEntName(_app.GetPracticeNoByUserID(uid));
                        var EvaluateEntItem = _app.GetEvaluateEntItem(_app.GetPracBatchIDByUserID(uid));
                        var StuEvaEntGradeLevelItem = _app.GetStuEvaEntGradeLevelItem(_app.GetPracBatchIDByUserID(uid));
                        return View(EvaluateEnterprise_M.ToViewModel(EntName, EvaluateEntItem, StuEvaEntGradeLevelItem, uid));
                    }
                    else
                    {
                        return Redirect("/App/Home/DetailEvaluateEnterprise?uid=" + uid);
                    }
                }
                else
                {
                    return Alert("还不能评价呢", -1);
                }
            }
            [HttpPost]
            public ActionResult EvaluateEnterprise(List<string> ItemNo, List<string> ItemContent, List<string> ItemGrade, string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        var PracticeNo = _app.GetPracticeNoByUserID(uid);
                        List<StuEvaluateEnt> EvaluateEnts = new List<StuEvaluateEnt>();
                        for (int i = 0; i < ItemNo.Count(); i++)
                        {
                            StuEvaluateEnt EvaluateEnt = new StuEvaluateEnt();
                            EvaluateEnt.PracticeNo = PracticeNo;
                            EvaluateEnt.ItemNo = ItemNo[i];
                            EvaluateEnt.ItemContent = ItemContent[i];
                            EvaluateEnt.ItemGrade = ItemGrade[i];
                            EvaluateEnts.Add(EvaluateEnt);
                        }
                        if (_app.AddStuEvaluateEnt(EvaluateEnts))
                        {
                            //return Alert("添加成功");
                            return Redirect("/App/Home/DetailEvaluateEnterprise?uid=" + uid);
                        }
                        return Alert("添加失败", -1);
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }

            public ActionResult IdentificationTable(string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.GetCareerStatus(uid) == 14) //判断是否已经实习结束
                        {
                            var student = _app.StuAllInfo(uid);
                            var comment = _app.AllComment(uid);
                            var volunteer = _app.GetFormaVolunteer(uid);
                            return View(IdentificationTable_M.ToViewModel(uid, volunteer, student, comment));
                        }
                        else
                        {
                            return Alert("你还没有鉴定表", -1);
                            //    return Redirect("/App/Home/Main?uid=" + uid);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }

            }

            public ActionResult ChangeCareerStatus(string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.GetFormalVolunteerCount(uid) != 0)
                        {
                            if (_app.ChangeCareerStatus(uid))
                            {
                                return Alert("报名结束", "/App/Home/FormalVolunteer?uid=" + uid);
                            }
                            return Alert("添加失败！", -1);
                        }
                        else
                        {
                            return Alert("操作错误！", -1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }

            public ActionResult PositionEvlTable(string uid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.GetCareerStatus(uid) == 14 || _app.GetCareerStatus(uid) == 15) //判断是否已经实习结束
                        {
                            var EntEvaStu = _app.GetEntEvaluateStu(uid);
                            var StuAllInfo = _app.StuAllInfo(uid);
                            var volunteer = _app.GetFormaVolunteer(uid);
                            var comment = _app.AllComment(uid);
                            return View(PositionEvlTable_M.ToViewModel(EntEvaStu, StuAllInfo, volunteer, comment, uid));
                        }
                        else
                        {
                            return Alert("你还没有评价表", -1);
                            //    return Redirect("/App/Home/Main?uid=" + uid);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }

            }

            public ActionResult AgreeRecruit(string uid, string PracticeNo, string EntPracNo, string PosID)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.CheckAgreeCount(PracticeNo) == 0)
                        {
                            if (_app.AgreePosition(PracticeNo, EntPracNo, PosID))
                            {
                                return Alert("同意录取", "/App/Home/FormalVolunteer?uid=" + uid);
                            }
                            else
                            {
                                return Alert("录取失败", -1);
                            }
                        }
                        else
                        {
                            return Alert("已被其他岗位录取", -1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }
            public ActionResult DisagreeRecruit(string uid, string PracticeNo, string EntPracNo, string PosID)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return Alert();
                    }
                    else
                    {
                        if (_app.DisagreePosition(PracticeNo, EntPracNo, PosID))
                        {
                            return Alert("已拒绝", "/App/Home/FormalVolunteer?uid=" + uid);
                        }
                        else
                        {
                            return Alert("操作失败", -1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Alert(ex.Message, -1);
                }
            }

            public ActionResult About(string uid)
            {
                return View(About_M.ToViewModel(uid));
            }
            public ActionResult AnswerQuestion(string uid)
            {
                return View(AnswerQuestion_M.ToViewModel(uid));
            }
        }
    }
