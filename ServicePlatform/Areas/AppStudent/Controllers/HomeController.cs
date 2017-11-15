using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Areas.AppStudent.ViewModel.Home;
using ServicePlatform.Controllers.Base;
using WebGrease.Css.Extensions;
using Qx.Scsxxt.Core.Services;
using System;
using System.Web;
using System.Collections.Generic;

namespace ServicePlatform.Areas.AppStudent.Controllers
{

    public class HomeController : AppStudentFilter
    {
        //1.列出所有页面
        //2.找页面映射关系
        //3.加region ,HttpPost,RedirectToAction(ToNext("xxxx"));

        // /AppStudent/Home
        private IAppService _app;

        public HomeController(IAppService app)
        {
            _app = app;
        }

        public ActionResult Index()
        {
            AddVisitorUrl("登陆", "Login");
            AddVisitorUrl("忘记密码", "ForgetPsw");
            AddHomeUrl("完善信息", "CompleteInfo");
            AddHomeUrl("疑问解答", "AnswerQuestions");
            AddHomeUrl("所有企业", "AllEnt_List");
            AddHomeUrl("企业详情", "Ent_Detail");
            AddHomeUrl("企业介绍", "Ent_Des");
            AddHomeUrl("企业岗位", "Ent_Post");
            AddHomeUrl("培养计划", "Ent_TrainingPlan");
            AddHomeUrl("成功案例", "Ent_SucceedCase");
            AddHomeUrl("薪资待遇", "Ent_Salary");
            AddHomeUrl("员工食宿", "Ent_StaffAcc");
            AddHomeUrl("员工天地", "Ent_Staff");
            AddHomeUrl("资源下载", "Ent_Download");
            AddHomeUrl("疑问解答2", "Ent_FAQ");
            AddHomeUrl("主页", "Main");
            AddHomeUrl("招聘企业", "RecEnt_List");
            AddHomeUrl("招聘企业详情", "RecEnt_Detail");
            AddHomeUrl("招聘企业介绍", "RecEnt_Des");
            AddHomeUrl("招聘企业岗位", "RecEnt_Post");
            AddHomeUrl("招聘企业培养计划", "RecEnt_TrainingPlan");
            AddHomeUrl("招聘企业薪资待遇", "RecEnt_Salary");
            AddHomeUrl("招聘企业员工食宿", "RecEnt_StaffAcc");
            AddHomeUrl("招聘企业员工天地", "RecEnt_Staff");
            AddHomeUrl("招聘企业成功案例", "RecEnt_SucceedCase");
            AddHomeUrl("招聘企业资源下载", "RecEnt_Download");
            AddHomeUrl("招聘企业疑问解答", "RecEnt_FAQ");
            AddHomeUrl("个人中心", "PersonalSpace");
            AddHomeUrl("预报名志愿", "PrepareVolunteer");
            AddHomeUrl("正式志愿", "FormalVolunteer");
            AddHomeUrl("实习总览", "PracticeOverView");
            AddHomeUrl("修改信息", "ReviseInfo");
            AddHomeUrl("修改密码", "RevisePsw");
            AddHomeUrl("关于实学", "About");
            AddHomeUrl("添加第一篇周记", "FirstDiary");
            AddHomeUrl("周记列表", "DiaryList");
            AddHomeUrl("添加周记", "AddDiary");
            AddHomeUrl("编辑周记", "EditDiary");
            AddHomeUrl("添加第一篇案例", "FirstCase");
            AddHomeUrl("案例列表", "CaseList");
            AddHomeUrl("添加案例", "AddCase");
            AddHomeUrl("编辑案例", "EditCase");
            AddHomeUrl("添加实习报告", "AddPracticeReport");
            AddHomeUrl("编辑实习报告", "EditPracticeReport");
            AddHomeUrl("查看实习报告", "PracticeReportDetail");
            AddHomeUrl("评价学校", "EvaluateSchool");
            AddHomeUrl("查看评价学校", "EvaluateSchoolDetail");
            AddHomeUrl("修改学校评价", "ReviseEvaluateSchool");
            AddHomeUrl("评价企业", "EvaluateEnterprise");
            AddHomeUrl("查看评价企业", "EvaluateEnterpriseDetail");
            AddHomeUrl("修改评价企业", "ReviseEvaluateEnterprise");
            AddHomeUrl("实习鉴定表", "IdentificationTable");
            AddHomeUrl("岗位评价表", "PositionEvlTable");
            AddHomeUrl("简历", "Resume");
            AddHomeUrl("志愿详情", "VolunteerDetail");

            InitMenu();
            return View();
        }


        #region CompleteInfo(完善信息)

        public ActionResult CompleteInfo()
        {
        
            var student = _app.FindStudent(DataContext.UserID); //T_Student
            if (string.IsNullOrEmpty(student.StuCellphone)) //没有录入自己的详细数据
            {
                InitAppStudentLayout("完善信息");
                return View(CompleteInfo_M.ToViewModel(student));
            }
            else
                return Redirect("/AppStudent/Home/Main");
        }

        [HttpPost]
        public ActionResult CompleteInfo(CompleteInfo_M form)
        {
            try
            {
                if (form._condition == "b")
                {
                    return RedirectToNext(form._condition);
                }
                else
                {
                    if (!string.IsNullOrEmpty(form.StuCellphone) &&
                        !string.IsNullOrEmpty(form.StuBirthday) &&
                        !string.IsNullOrEmpty(form.StuEMail) &&
                        !string.IsNullOrEmpty(form.StuQQ) &&
                        !string.IsNullOrEmpty(form.StuSex) &&
                        !string.IsNullOrEmpty(form.StuResume))
                    {
                        var coveredTime = DateTime.Parse(form.StuBirthday); //转化类型
                        //var student =_app.FindStudent(uid);//T_Student
                        var stu = _app.FindStu(DataContext.UserID); //T_User            
                        UserInfo userinfo = new UserInfo();
                        userinfo = form.ToModel();
                        if (_app.UpdateInfo(DataContext, userinfo))
                        {
                            //return Alert("保存成功！");
                            return WxAlert("保存成功", "恭喜", "/AppStudent/Home/Main");
                        }
                        else
                            return WxAlert("保存失败！");
                    }
                    else
                    {
                        return WxAlert("请将信息填写完整", "提示", "/AppStudent/Home/CompleteInfo");
                    }
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
            // return RedirectToNext(m._condition);
            //   return RedirectToAction("Main");
        }

        #endregion

        #region  AnswerQuestions(疑问解答)

        public ActionResult AnswerQuestions(string uid)
        {
            InitAppStudentLayout("疑问解答");
            return View(AnswerQuestions_M.ToViewModel());
        }

        [HttpPost]
        public ActionResult AnswerQuestions(AnswerQuestions_M form)
        {
            return RedirectToNext(form._condition);
        }

        //public ActionResult AnswerQuestions(AnswerQuestions_M m)
        //{
        //    return RedirectToAction("Main");
        //}

        #endregion

        #region AllEnt_List(所有企业)

        public ActionResult AllEnt_List(string EntCategoryID="")
        {
            try
            {
                var count = _app.GetPositionCount();
                var enterprises = _app.GetEnterprises(EntCategoryID);
                var EntCategory = _app.EntCategory();
                InitAppStudentLayout("所有企业");
                //var   enterprises.Select(a => GetEntPic(a.Ent_No));
                return View(AllEnt_List_M.ToViewModel(enterprises, EntCategoryID, count, EntCategory));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }       
        }

        [HttpPost]
        public ActionResult AllEnt_List(AllEnt_List_M form)
        {
            switch (form._condition)
            {
                case "a": //主页
                    return RedirectToNext(form._condition);
                case "b": //企业详情
                    return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
                default: //本页面  所有企业
                    return RedirectToNext(form._condition);
                    break;
            }
        }

        #endregion

        #region Ent_Detail(企业详情)

        public ActionResult Ent_Detail(string Ent_No)
        {         
            var enterprise = _app.SingleEnterprise(Ent_No);
            InitAppStudentLayout("企业详情");
            return View(Ent_Detail_M.ToViewModel(enterprise, Ent_No));
        }

        [HttpPost]
        public ActionResult Ent_Detail(Ent_Detail_M form)
        {
            switch (form._condition)
            {
                case "a": //企业介绍
                    return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
                case "b": //企业岗位
                    return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
                case "c": //培训计划
                    return RedirectToNext(form._condition,
                        new { Ent_No = form.Ent_No, DLFileColumnID = form.DLFileColumnID });
                case "d": //成功案例
                    return RedirectToNext(form._condition,
                        new { Ent_No = form.Ent_No, DLFileColumnID = form.DLFileColumnID });
                case "e": //薪资待遇
                    return RedirectToNext(form._condition,
                        new { Ent_No = form.Ent_No, DLFileColumnID = form.DLFileColumnID });
                case "f": //员工食宿
                    return RedirectToNext(form._condition,
                        new { Ent_No = form.Ent_No, DLFileColumnID = form.DLFileColumnID });
                case "g": //员工天地
                    return RedirectToNext(form._condition,
                        new { Ent_No = form.Ent_No, DLFileColumnID = form.DLFileColumnID });
                case "h": //资源下载
                    return RedirectToNext(form._condition,
                        new { Ent_No = form.Ent_No, DLFileColumnID = form.DLFileColumnID });
                case "i": //企业疑问解答
                    return RedirectToNext(form._condition, new { Ent_No = form.Ent_No, DLFileColumnID = form.DLFileColumnID });
                default: //所有企业
                    return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
            }
        }

        #endregion

        # region Ent_Des(企业介绍)

        public ActionResult Ent_Des(string Ent_No)
        {

            //   return View();
            //获取企业的轮播图
            //DataContext.SetFiled("UnitID", Ent_No);
            //DataContext.SetFiled("Ent_No", Ent_No);
            //var AdList = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的轮播广告图");
            var enterprise = _app.SingleEnterprise(Ent_No);
            InitAppStudentLayout("企业介绍");
            return View(Ent_Des_M.ToViewModel(enterprise, Ent_No));

        }

        [HttpPost]
        public ActionResult Ent_Des(Ent_Des_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        #region Ent_Post(企业岗位)

        public ActionResult Ent_Post(string Ent_No)
        {
            try
            {           
                var EntPracNo = _app.GetEntPracNoByEntNo_AllPrac(Ent_No);
                var job = _app.GetJobs(EntPracNo);
                InitAppStudentLayout("企业岗位");
                return View(Ent_Post_M.ToViewModel(job, Ent_No));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Ent_Post(Ent_Post_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        #region Ent_TrainingPlan(培养计划)

        public ActionResult Ent_TrainingPlan(string Ent_No, int DLFileColumnID)
        {
            InitAppStudentLayout("培养计划");
            // return View();
            return View(Ent_TrainingPlan_M.ToViewModel(Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult Ent_TrainingPlan(Ent_TrainingPlan_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        #region Ent_SucceedCase(成功案例)

        public ActionResult Ent_SucceedCase(string Ent_No, int DLFileColumnID)
        {
            InitAppStudentLayout("成功案例");
            //return View();
            return View(Ent_SucceedCase_M.ToViewModel(Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult Ent_SucceedCase(Ent_SucceedCase_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        #region Ent_Salary(薪资待遇)

        public ActionResult Ent_Salary(string Ent_No, int DLFileColumnID)
        {
            InitAppStudentLayout("薪资待遇");
            //return View();
            return View(Ent_Salary_M.ToViewModel(Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult Ent_Salary(Ent_Salary_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        #region Ent_StaffAcc(员工食宿)

        public ActionResult Ent_StaffAcc(string Ent_No, int DLFileColumnID)
        {
            InitAppStudentLayout("员工食宿");
            //return View();
            return View(Ent_StaffAcc_M.ToViewModel(Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult Ent_StaffAcc(Ent_StaffAcc_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        #region Ent_Staff(员工天地)

        public ActionResult Ent_Staff(string Ent_No, int DLFileColumnID)
        {
            InitAppStudentLayout("员工天地");
            //   return View();
            return View(Ent_Staff_M.ToViewModel(Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult Ent_Staff(Ent_Staff_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        #region Ent_Download(资源下载)

        public ActionResult Ent_Download(string Ent_No, int DLFileColumnID)
        {
            InitAppStudentLayout("资源下载");
            //  return View();
            return View(Ent_Download_M.ToViewModel(Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult Ent_Download(Ent_Download_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        #region Ent_FAQ(疑问解答)

        public ActionResult Ent_FAQ(string Ent_No, int DLFileColumnID)
        {
            InitAppStudentLayout("常见问题");
            //var AnswerQuestions = _app.GetAnswerQuestions(Ent_No);
            return View(Ent_FAQ_M.ToViewModel(Ent_No, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult Ent_FAQ(Ent_FAQ_M form)
        {
            return RedirectToNext(form._condition, new { Ent_No = form.Ent_No });
        }

        #endregion

        //js有问题

        #region Main(主页)

        public ActionResult Main(string EntCategoryID)
        {
          
                var EvaluateSchoolCount = _app.GetEvaluateSchoolCount(DataContext.UserID);
                var EvaluateEntCount = _app.GetEvaluateEntCount(DataContext.UserID);
                var EntCount = _app.GrogshopsCount(EntCategoryID, DataContext.UserUnit);
                var AllCount = _app.GetEnterpriseCount();
                var AllComment = _app.AllComment(DataContext.UserID);
                var DiaryCount = _app.GetDiaryCount(DataContext.UserID);
                var CaseCount = _app.CaseCount(DataContext.UserID);
                var careerstatus = _app.GetCareerStatus(DataContext.UserID);
                InitAppStudentLayout("主页");
                return
                    View(Main_M.ToViewModel(careerstatus, DataContext.UserID, AllComment, AllCount, EntCount, EvaluateSchoolCount,
                        EvaluateEntCount, DiaryCount, CaseCount));
            
         
        }

        [HttpPost]
        public ActionResult Main(Main_M form)
        {
            try
            {
                switch (form._condition)
                {
                    case "a": //所有企业
                        return RedirectToNext(form._condition);
                    case "b": //预报名志愿
                        return RedirectToNext(form._condition);
                    case "c": //个人中心
                        return RedirectToNext(form._condition);
                    case "d": //添加周记
                        return RedirectToNext(form._condition);
                    case "e": //周记列表
                        return RedirectToNext(form._condition);
                    case "f": //添加案例
                        return RedirectToNext(form._condition);
                    case "g": //案例列表
                        return RedirectToNext(form._condition);
                    case "h": //添加实习报告
                        return RedirectToNext(form._condition);
                    case "i": //查看实习报告
                        return RedirectToNext(form._condition);
                    case "j": //评价学校
                        return RedirectToNext(form._condition);
                    case "k": //查看评价学校
                        return RedirectToNext(form._condition);
                    case "l": //评价企业
                        return RedirectToNext(form._condition);
                    case "m": //查看评价企业
                        return RedirectToNext(form._condition);
                    case "n": //实习鉴定表
                        return RedirectToNext(form._condition);
                    default: //岗位评价表
                        return RedirectToNext(form._condition);
                }
                ;
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        #endregion

        #region RecEnt_List(招聘企业)

        public ActionResult RecEnt_List(int VolunteerSequence, int PosSequence, string EntCategoryID="")
        {
            try
            {
           
                // return View();
                //ViewData["CurrentUrl"] = CurrentUrl();
                //// var PicList =new List<T_DownLoadFiles>();
                var enterpriselist = _app.GetGrogshops(EntCategoryID, DataContext.UserUnit);
                //var PracticeNo = _app.GetPracticeNoByUserID(uid);
                var PositionCount = _app.GetPositionCount();
                var EntCategory = _app.EntCategory();
                ////Grogshop.Where(a => true).Select(b => _app.GetEntNoByEntPracNo(b.EntPracNo)).ToList().
                ////    ForEach(c =>
                ////   {
                ////       DataContext.SetFiled("UnitID", c);
                ////       PicList.Add(S<T_DownLoadFiles>().All(DataContext, "某单位的介绍图集").FirstOrDefault());
                ////   }); 
                  InitAppStudentLayout("招聘企业");
                return
                    View(RecEnt_List_M.ToViewModel(enterpriselist, EntCategoryID, EntCategory, VolunteerSequence,
                        PosSequence, PositionCount));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult RecEnt_List(RecEnt_List_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        #region RecEnt_Detail(招聘企业详情)

        public ActionResult RecEnt_Detail(string EntPracNo, int VolunteerSequence, int PosSequence)
        {
            var Enterprise = _app.Enterprise(EntPracNo);
            InitAppStudentLayout("招聘企业详情");
            return View(RecEnt_Detail_M.ToViewModel(Enterprise, EntPracNo, VolunteerSequence, PosSequence));
        }

        [HttpPost]
        public ActionResult RecEnt_Detail(RecEnt_Detail_M form)
        {
            switch (form._condition)
            {
                case "a":
                    return RedirectToNext(form._condition,
                        new
                        {
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence
                        });
                case "b":
                    return RedirectToNext(form._condition,
                        new
                        {
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence
                        });
                case "c":
                    return RedirectToNext(form._condition,
                        new
                        {
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence,
                            DLFileColumnID = form.DLFileColumnID
                        });
                case "d":
                    return RedirectToNext(form._condition,
                        new
                        {
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence,
                            DLFileColumnID = form.DLFileColumnID
                        });
                case "e":
                    return RedirectToNext(form._condition,
                        new
                        {
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence,
                            DLFileColumnID = form.DLFileColumnID
                        });
                case "f":
                    return RedirectToNext(form._condition,
                        new
                        {
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence,
                            DLFileColumnID = form.DLFileColumnID
                        });
                case "g":
                    return RedirectToNext(form._condition,
                        new
                        {
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence,
                            DLFileColumnID = form.DLFileColumnID
                        });
                case "h":
                    return RedirectToNext(form._condition,
                        new
                        {
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence,
                            DLFileColumnID = form.DLFileColumnID
                        });
                case "i":
                    return RedirectToNext(form._condition,
                        new
                        {
                            Ent_No = form.Ent_No,
                            EntPracNo = form.EntPracNo,
                            VolunteerSequence = form.VolunteerSequence,
                            PosSequence = form.PosSequence,
                            DLFileColumnID = form.DLFileColumnID
                        });
                default:
                    return RedirectToNext(form._condition,
                        new { VolunteerSequence = form.VolunteerSequence, PosSequence = form.PosSequence });
            }
        }

        #endregion

        #region RecEnt_Des(招聘企业介绍)

        public ActionResult RecEnt_Des(string EntPracNo, int VolunteerSequence, int PosSequence)
        {

            ////var Grogshop = _app.GetGrogshops();
            ////var EntNo = _app.GetEntNoByEntPracNo(EntPracNo);
            ////DataContext.SetFiled("UnitID", EntNo);
            ////DataContext.SetFiled("Ent_No", EntNo);

            var Enterprise = _app.Enterprise(EntPracNo);
            ////var job = _app.GetJobs(EntPracNo);
            //////获取企业的轮播图
            ////var AdList = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的轮播广告图");
            InitAppStudentLayout("招聘企业介绍");
            return View(RecEnt_Des_M.ToViewModel(EntPracNo, VolunteerSequence, PosSequence, Enterprise));

        }

        [HttpPost]
        public ActionResult RecEnt_Des(RecEnt_Des_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        #region RecEnt_Post(招聘企业岗位)

        public ActionResult RecEnt_Post(string EntPracNo, int VolunteerSequence, int PosSequence)
        {
     
            var job = _app.GetJobs(EntPracNo);
            InitAppStudentLayout("招聘企业岗位");
            return View(RecEnt_Post_M.ToViewModel(job, EntPracNo, VolunteerSequence, PosSequence));
        }

        [HttpPost]
        public ActionResult RecEnt_Post(RecEnt_Post_M form, string EntPracNo, int VolunteerSequence, int PosSequence)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return WxAlert("");
                }
                else
                {
                    if (form._condition == "a")
                    {
                        return RedirectToNext(form._condition,
                            new
                            {
                                EntPracNo = form.EntPracNo,
                                VolunteerSequence = form.VolunteerSequence,
                                PosSequence = form.PosSequence
                            });
                    }
                    else
                    {
                        var PosID = form.PosID;
                        var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                        if (_app.GetPrepareVolunteer(PracticeNo, EntPracNo, PosID))
                        {
                            if (_app.AddVolunteer(PracticeNo, EntPracNo, PosID, VolunteerSequence, PosSequence))
                            {
                                return WxAlert("添加志愿成功", "恭喜", "-1");
                                //return WxAlert("添加志愿成功！");
                            }
                            else
                                return WxAlert("志愿添加失败！");
                        }
                        else
                            return WxAlert("该志愿已经添加");
                    }
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        #endregion

        #region RecEnt_TrainingPlan(招聘企业培养计划)

        public ActionResult RecEnt_TrainingPlan(string EntPracNo, int VolunteerSequence, int PosSequence,
            int DLFileColumnID)
        {

            var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
            InitAppStudentLayout("招聘企业培养计划");
            return
                View(RecEnt_TrainingPlan_M.ToViewModel(EntPracNo, VolunteerSequence, PosSequence,
                    _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult RecEnt_TrainingPlan(RecEnt_TrainingPlan_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        #region RecEnt_Salary(招聘企业薪资待遇)

        public ActionResult RecEnt_Salary(string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
        {
          
            var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
            InitAppStudentLayout("招聘企业薪资待遇");
            return
                View(RecEnt_Salary_M.ToViewModel(EntPracNo, VolunteerSequence, PosSequence,
                    _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));

        }

        [HttpPost]
        public ActionResult RecEnt_Salary(RecEnt_Salary_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        #region RecEnt_StaffAcc (招聘企业员工食宿)

        public ActionResult RecEnt_StaffAcc(string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
        {
            var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
            InitAppStudentLayout("招聘企业员工食宿");
            return
                View(RecEnt_StaffAcc_M.ToViewModel(EntPracNo, VolunteerSequence, PosSequence,
                    _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));

        }

        [HttpPost]
        public ActionResult RecEnt_StaffAcc(RecEnt_StaffAcc_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        #region RecEnt_Staff(招聘企业员工天地)

        public ActionResult RecEnt_Staff(string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
        {
         
            var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
            InitAppStudentLayout("招聘企业员工天地");
            return
                View(RecEnt_Staff_M.ToViewModel(EntPracNo, VolunteerSequence, PosSequence,
                    _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));

        }

        [HttpPost]
        public ActionResult RecEnt_Staff(RecEnt_Staff_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        #region RecEnt_SucceedCase(招聘企业成功案例)

        public ActionResult RecEnt_SucceedCase(string EntPracNo, int VolunteerSequence, int PosSequence,
            int DLFileColumnID)
        {
   
            var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
            InitAppStudentLayout("招聘企业成功案例");
            return
                View(RecEnt_SucceedCase_M.ToViewModel(EntPracNo, VolunteerSequence, PosSequence,
                    _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));

        }

        [HttpPost]
        public ActionResult RecEnt_SucceedCase(RecEnt_SucceedCase_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        #region RecEnt_Download(招聘企业资源下载)

        public ActionResult RecEnt_Download(string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
        {
         
            var Ent_No = _app.GetEntNoByEntPracNo(EntPracNo);
            InitAppStudentLayout("招聘企业资源下载");
            return
                View(RecEnt_Download_M.ToViewModel(EntPracNo, VolunteerSequence, PosSequence,
                    _app.GetDownLoadFiles(Ent_No, 205)));

        }

        [HttpPost]
        public ActionResult RecEnt_Download(RecEnt_Download_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        #region RecEnt_FAQ(招聘企业疑问解答)

        public ActionResult RecEnt_FAQ(string Ent_No, string EntPracNo, int VolunteerSequence, int PosSequence, int DLFileColumnID)
        {
            InitAppStudentLayout("招聘企业常见问题");
            // var AnswerQuestions = _app.GetAnswerQuestions(Ent_No);
            return View(RecEnt_FAQ_M.ToViewModel(Ent_No, EntPracNo, VolunteerSequence, PosSequence, _app.GetDownLoadFiles(Ent_No, DLFileColumnID)));
        }

        [HttpPost]
        public ActionResult RecEnt_FAQ(RecEnt_FAQ_M form)
        {
            return RedirectToNext(form._condition,
                new
                {
                    EntPracNo = form.EntPracNo,
                    VolunteerSequence = form.VolunteerSequence,
                    PosSequence = form.PosSequence
                });
        }

        #endregion

        //有报错

        #region PersonalSpace(个人中心)

        public ActionResult PersonalSpace()
        {
            try
            {
                var StuInfo = _app.StuAllInfo(DataContext.UserID);
                if (StuInfo != null)
                {
                
                    var PerVolCount = _app.GetPerparVolunteerCount(DataContext.UserID);
                    var ForVolCount = _app.GetFormalVolunteerCount(DataContext.UserID);
                    InitAppStudentLayout("个人中心");
                    return View(PersonalSpace_M.ToViewModel(DataContext.UserID, PerVolCount, ForVolCount, StuInfo));
                }
                else
                {
                    return WxAlert("不存在该学生");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult PersonalSpace(PersonalSpace_M form)
        {
            if (form._condition == "h")
            {
                Session.Clear();
            }
            return RedirectToNext(form._condition);
        }

        #endregion

        #region  PrepareVolunteer(预报名志愿)

        public ActionResult PrepareVolunteer()
        {
            try
            {
                var Status = _app.GetCareerStatus(DataContext.UserID);
                if (Status == 10)
                {
                
                    var allVolunteer = _app.GetVolunteers(DataContext.UserID); //读取我的所有志愿   
                    InitAppStudentLayout("预报名志愿");
                    return View(PrepareVolunteer_M.ToViewModel(allVolunteer, Status));
                }
                else
                {
                    return WxAlert("报名已结束,招聘信息可以去全新资讯查看");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult PrepareVolunteer(PrepareVolunteer_M form)
        {
            try
            {
                switch (form._condition)
                {
                    case "c": //主页
                        return RedirectToNext(form._condition);
                    case "b": //主招聘企业
                        return RedirectToNext(form._condition,
                            new { VolunteerSequence = form.VolunteerSequence, PosSequence = form.PosSequence });
                    case "d":
                        {
                            var count = _app.GetFormalVolunteerCount(DataContext.UserID); //计算志愿数
                            if (count >= 4)
                            {
                                return WxAlert("志愿已达到上限");
                            }
                            else
                            {
                                if (_app.ConfirVolunteer(DataContext.UserID, form.EntPracNo, form.PosID))
                                {
                                    return WxAlert("添加成功！", "恭喜", "/appstudent/home/PrepareVolunteer");
                                }
                                else
                                    return WxAlert("添加失败！");
                            }
                        }
                    default:
                        {
                            var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                            if (_app.DeleteVolunteer(PracticeNo, form.EntPracNo, form.PosID)) //bool型
                            {
                                return WxAlert("删除成功", "提示", "/appstudent/home/PrepareVolunteer");
                            }
                            else
                                return WxAlert("删除失败");
                        }
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        #endregion

        #region PrepareVolunteerList
        public ActionResult PrepareVolunteerList()
        {
            try
            {        
                var Status = _app.GetCareerStatus(DataContext.UserID);
                var allVolunteer = _app.GetVolunteers(DataContext.UserID); //读取我的所有志愿    
                InitAppStudentLayout("预报名志愿");
                return View(PrepareVolunteerList_M.ToViewModel(allVolunteer, Status));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult PrepareVolunteerList(PrepareVolunteerList_M form)
        {
            try
            {
                switch (form._condition)
                {
                    case "a":
                        return RedirectToNext(form._condition);
                    //case "c": //主页
                    //    return RedirectToNext(form._condition);
                    //case "b": //主招聘企业
                    //    return RedirectToNext(form._condition,new { VolunteerSequence = form.VolunteerSequence, PosSequence = form.PosSequence });
                    case "d":
                        {
                            var count = _app.GetFormalVolunteerCount(DataContext.UserID); //计算志愿数
                            if (count >= 4)
                            {
                                return WxAlert("志愿已达到上限");
                            }
                            else
                            {
                                if (_app.ConfirVolunteer(DataContext.UserID, form.EntPracNo, form.PosID))
                                {
                                    return WxAlert("添加成功！", "恭喜", "/appstudent/home/PrepareVolunteerList");
                                }
                                else
                                    return WxAlert("添加失败！");
                            }
                        }
                    default:
                        {
                            // return RedirectToNext(form._condition);
                            var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                            if (_app.DeleteVolunteer(PracticeNo, form.EntPracNo, form.PosID)) //bool型
                            {
                                return WxAlert("删除成功", "提示", "/appstudent/home/PrepareVolunteerList");
                            }
                            else
                                return WxAlert("删除失败");
                        }
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion

        #region FormalVolunteer(正式志愿)

        public ActionResult FormalVolunteer()
        {
            var data= _app.IsEnroll(DataContext.UserID);
            var CareerStatus = _app.GetCareerStatus(DataContext.UserID);
            var volunteer = _app.GetVolunteers(DataContext.UserID);
            InitAppStudentLayout("正式志愿");
            return View(FormalVolunteer_M.ToViewModel(volunteer, CareerStatus, data));
        } 

        [HttpPost]
        public ActionResult FormalVolunteer(FormalVolunteer_M form)
        {
            try
            {
                switch (form._condition)
                {
                    case "a": //个人空间
                        return RedirectToNext(form._condition);
                    case "b": //志愿详情
                        return RedirectToNext(form._condition, new { EntPracNo = form.EntPracNo, PosID = form.PosID });
                    default:
                        {
                            if (form.id == 0) //报名结束
                            {
                                if (_app.GetFormalVolunteerCount(DataContext.UserID) != 0)
                                {
                                    if (_app.ChangeCareerStatus(DataContext.UserID))
                                    {
                                        return WxAlert("报名结束", "提示", "/appstudent/home/FormalVolunteer");
                                    }
                                    return WxAlert("添加失败！");
                                }
                                else
                                {
                                    return WxAlert("你还没有正式志愿，不能结束报名", "警告", "-1");
                                }
                            }
                            if (form.id == 1) //同意录取
                            {
                                var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                                if (_app.CheckAgreeCount(PracticeNo) == 0)
                                {
                                    if (_app.AgreePosition(PracticeNo, form.EntPracNo, form.PosID))
                                    {
                                        if (_app.OverBaoming(DataContext.UserID))
                                        {
                                            return WxAlert("同意录取", "恭喜", "/appstudent/home/FormalVolunteer");
                                        }
                                        else
                                        {
                                            return WxAlert("录取失败", "提示", "/appstudent/home/FormalVolunteer");
                                        }
                                    }
                                    else
                                    {
                                        return WxAlert("录取失败");
                                    }
                                }
                                else
                                {
                                    return WxAlert("已被其他岗位录取", "提示", "/appstudent/home/FormalVolunteer");
                                }
                            }
                            if (form.id == -1) //拒绝录取
                            {
                                var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                                if (_app.DisagreePosition(PracticeNo, form.EntPracNo, form.PosID))
                                {
                                    return WxAlert("你已拒绝该岗位", "提示", "/appstudent/home/FormalVolunteer");
                                }
                                else
                                {
                                    return WxAlert("操作失败");
                                }
                            }
                            if (form.id == -2)
                            {
                                var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                                if (_app.DeleteVolunteer(PracticeNo, form.EntPracNo, form.PosID)) //bool型
                                {
                                    return WxAlert("删除成功", "提示", "/appstudent/home/FormalVolunteer");
                                }
                                else
                                {
                                    return WxAlert("删除失败");
                                }
                            }
                        }
                        return RedirectToNext(form._condition);
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        #endregion

        #region PracticeOverView(实习总览)

        public ActionResult PracticeOverView()
        {

            try
            {
                var PracticeCaseCount = 0;
                var EntCount = _app.GetEnterpriseCount();
                var YetSubmitCount = _app.YetSubmit(DataContext.UserID);
                // var PracticeNo = _app.GetPracticeNoByUserID(uid);
                var FinalVolunteerCount = _app.GetFinalVolunteerCount(DataContext.UserID);
                if (FinalVolunteerCount != 0)
                {
                    var FormalVolunteer = _app.GetFormaVolunteer(DataContext.UserID);
                }
                var PrepareVolunteerCount = _app.GetPerparVolunteerCount(DataContext.UserID);
                var FormalVolunteerCount = _app.GetFormalVolunteerCount(DataContext.UserID);
                var CareerStatus = _app.GetCareerStatus(DataContext.UserID);
                var DiaryCount = _app.GetDiaryCount(DataContext.UserID);
                var casecount = _app.CaseCount(DataContext.UserID);
                if (casecount != 0)
                {
                    PracticeCaseCount = _app.GetCaseCount(DataContext.UserID);
                }
                else
                {
                    PracticeCaseCount = casecount;
                }
                var AllComment = _app.AllComment(DataContext.UserID);
                var EvaluateEntCount = _app.GetEvaluateEntCount(DataContext.UserID);
                var EvaluateEnt = _app.GetStuEvaluateEnt(DataContext.UserID);
                var EvaluateSchool = _app.GetStuEvaluateSchool(DataContext.UserID);
                var EntFindStu = _app.GetEntFindStu(DataContext.UserID);
                var StuFindEnt = _app.GetStuFindEnt(DataContext.UserID);
                if (FinalVolunteerCount != 0)
                {
                    InitAppStudentLayout("实习总览");
                    return View(PracticeOverView_M.ToViewModel(
                        YetSubmitCount,
                        EntCount,
                        FinalVolunteerCount,
                        PrepareVolunteerCount,
                        FormalVolunteerCount,
                        _app.GetFormaVolunteer(DataContext.UserID),
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
                    InitAppStudentLayout("实习总览");
                    return View(PracticeOverView_M.ToViewModel(
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
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult PracticeOverView(PracticeOverView_M form)
        {
            switch (form._condition)
            {
                case "a":
                    return RedirectToNext(form._condition);
                case "b":
                    return RedirectToNext(form._condition);
                case "c":
                    return RedirectToNext(form._condition);
                case "d":
                    return RedirectToNext(form._condition);
                case "e":
                    return RedirectToNext(form._condition);
                case "f":
                    return RedirectToNext(form._condition);
                case "g":
                    return RedirectToNext(form._condition);
                case "h":
                    return RedirectToNext(form._condition);
                case "i":
                    return RedirectToNext(form._condition);
                case "j":
                    return RedirectToNext(form._condition);
                case "k":
                    return RedirectToNext(form._condition);
                case "l":
                    return RedirectToNext(form._condition);
                case "m":
                    return RedirectToNext(form._condition, new { EntPracNo = form.FormalVolunteer.EntPracNo, PosID = form.FormalVolunteer.PosID });
                default:
                    return RedirectToNext(form._condition);
            }
        }

        #endregion

        #region ReviseInfo(修改信息Post)

        public ActionResult ReviseInfo()
        {
            InitAppStudentLayout("修改信息");
            return View(ReviseInfo_M.ToViewModel(_app.GetPersonalInfo(DataContext.UserID)));
        }

        [HttpPost]
        public ActionResult ReviseInfo(ReviseInfo_M form)
        {
            // return RedirectToNext(form._condition);
            try
            {
                if (form._condition == "-") //保存
                {
                    if (!string.IsNullOrEmpty(form.StuCellphone) &&
                        !string.IsNullOrEmpty(form.StuBirthday) &&
                        !string.IsNullOrEmpty(form.StuEMail) &&
                        !string.IsNullOrEmpty(form.StuQQ) &&
                        !string.IsNullOrEmpty(form.StuSex) &&
                        !string.IsNullOrEmpty(form.StuResume))
                    {
                        var coveredTime = DateTime.Parse(form.StuBirthday); //转化类型
                        //var student =_app.FindStudent(uid);//T_Student
                        //  var stu = _app.FindStu(uid);//T_User            
                        UserInfo userinfo = new UserInfo();
                        form.Uid = DataContext.UserID;
                        userinfo = form.ToModel();
                        if (_app.UpdateInfo(DataContext, userinfo))
                        {
                            //return Alert("保存成功！");
                            return WxAlert("修改成功", "恭喜", "/AppStudent/Home/ReviseInfo");
                        }
                        else
                            return WxAlert("保存失败");
                    }
                    else
                    {
                        return WxAlert("请将信息填写完整", "提示", "/AppStudent/Home/ReviseInfo");
                    }
                }
                else
                {
                    //个人空间
                    return RedirectToNext(form._condition);
                }
            }
            catch (Exception ex)
            {
                return WxAlert("ex.Message");
            }
        }

        #endregion

        #region  RevisePsw(修改密码Post)

        public ActionResult RevisePsw()
        {
            InitAppStudentLayout("修改密码");
            return View();
            //   return View(RevisePsw_M.ToViewModel(uid));
        }

        [HttpPost]
        public ActionResult RevisePsw(RevisePsw_M form)
        {
            //return RedirectToNext(form._condition);
            try
            {
                if (form._condition == "a")
                {
                    return RedirectToNext(form._condition);
                }
                else
                {
                    if (string.IsNullOrEmpty(form.OldPwd))
                    {
                        return WxAlert("旧密码不能为空");
                    }
                    else
                    {
                        if (_app.CheckPsw(form.OldPwd, DataContext.UserID)) //检查现有密码是否和之前密码匹配
                        {
                            if (string.IsNullOrEmpty(form.NewPwd))
                            {
                                return WxAlert("新密码不能为空", "提示", "-1");
                            }
                            else
                            {
                                if (form.NewPwd == form.SecondPwd)
                                {
                                    UserInfo userinfo = new UserInfo();
                                    form.Uid = DataContext.UserID;
                                    userinfo = form.ToModel();
                                    if (_app.RevisePsw(DataContext, userinfo))
                                    {
                                        Session.Clear();
                                        return WxAlert("修改成功", "提示", "/AppStudent/Visitor/Login");
                                    }
                                    else
                                    {
                                        return WxAlert("修改失败");
                                    }
                                }
                                else
                                    return WxAlert("请检查新密码", "提示", "-1");
                            }
                        }
                        else
                        {
                            return WxAlert("旧密码不对，请重新输入");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        #endregion

        #region About(关于实学)

        public ActionResult About(string uid)
        {
            InitAppStudentLayout("关于实学");
            return View(About_M.ToViewModel());
        }

        [HttpPost]
        public ActionResult About(About_M form)
        {
            return RedirectToNext(form._condition);
        }

        #endregion

        #region FirstDiary(添加第一篇周记)

        public ActionResult FirstDiary(string uid)
        {
            InitAppStudentLayout("添加第一篇周记");
            return View();
            return View(FirstDiary_M.ToViewModel());
        }

        [HttpPost]
        public ActionResult FirstDiary(FirstDiary_M form)
        {
            return RedirectToNext(form._condition);
        }

        #endregion

        #region DiaryList(周记列表)

        public ActionResult DiaryList()
        {
            try
            {
                if (_app.GetCareerStatus(DataContext.UserID) == 13 || _app.GetCareerStatus(DataContext.UserID) == 14 || _app.GetCareerStatus(DataContext.UserID) == 15) //结束报名,结束招聘
                {

                    InitAppStudentLayout("周记列表");
                    if (_app.GetDiaryCount(DataContext.UserID) == 0)
                    {
                        return Redirect("/AppStudent/Home/AddDiary");
                    }
                    else
                    {
                        var DiaryCount = _app.GetDiaryCount(DataContext.UserID);
                        var Diary = _app.GetDiarys(DataContext.UserID);
                        var careerstatus = _app.GetCareerStatus(DataContext.UserID);
                        return View(DiaryList_M.ToViewModel(careerstatus, Diary, DiaryCount));
                    }
                }
                else
                {
                    return WxAlert("等到企业录取之后才能进行操作");
                }

            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DiaryList(DiaryList_M form)
        {
            try
            {
                switch (form._condition)
                {
                    case "a": //主页
                        return RedirectToNext(form._condition);
                    case "b": //编辑周记
                        return RedirectToNext(form._condition, new { RecordNo = form.RecordNo });
                    case "c": //添加周记
                        return RedirectToNext(form._condition);
                    default: //删除周记
                        {
                            var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                            if (_app.DeleteDiary(PracticeNo, form.RecordNo))
                            {
                                return WxAlert("删除成功", "提示", "/AppStudent/Home/DiaryList");
                            }
                            else
                                return WxAlert("删除失败");
                        }
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        #endregion

        #region AddDiary(添加周记)

        public ActionResult AddDiary()
        {
            try
            {
                    if (_app.GetCareerStatus(DataContext.UserID)==13) //判断是否已经被岗位录取了
                    {
                        InitAppStudentLayout("添加周记");
                        return View(AddDiary_M.ToViewModel(_app.GetDiaryCount(DataContext.UserID)));
                    }
                    else
                    {
                        return WxAlert("等到企业录取之后才能进行操作");
                    }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult AddDiary(string piclist, AddDiary_M form)
        {
            //1. View中设置input标签 type=file name=file   
            //2.调用  Lib.FileOperate.UploadFile(file, "/UserFiles/Test/"); 获得返回值
            //3.将2的返回值给数据库     
            try
            {
                switch (form._condition)
                {
                    case "a"://保存周记列表
                        {
                            if (string.IsNullOrEmpty(form.RecordTitle))
                            {
                                return WxAlert("请填写标题");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(form.RecordContent))
                                {
                                    return WxAlert("请填写内容");
                                }
                                else
                                {
                                  //  HttpPostedFileBase[] uploaderInput= uploaderInput.
                                    Diary diary = new Diary();
                                    form.PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                                    form.RecordNo = DateTime.Now.Random();
                                    form.RecordDate = Lib.DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
                                    //把datetime型的时间转化为int       
                                    form.Pic = piclist;
                                    //form.Pic = Lib.FileOperate.UploadMultiFile(uploaderInput,"/UserFiles/APP/Files/WeekRecords/");
                                    diary = form.ToModel();
                                    if (_app.AddDiary(diary))
                                    {
                                        // return Alert(" 保存成功");
                                        return WxAlert("添加成功", "恭喜", "/AppStudent/Home/DiaryList");
                                    }
                                    else
                                    {
                                        return WxAlert("保存失败");
                                    }
                                }
                            }
                        }
                    default://主页
                        return RedirectToNext(form._condition);
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion
        #region EditDiary(编辑周记Post)
        public ActionResult EditDiary(int RecordNo)
        {
            InitAppStudentLayout("编辑周记");
            var careerstatus = _app.GetCareerStatus(DataContext.UserID);
            return View(EditDiary_M.ToViewModel(careerstatus, _app.GetDiary(DataContext.UserID, RecordNo), _app.GetDiaryCount(DataContext.UserID)));
        }
        [HttpPost]
        public ActionResult EditDiary(EditDiary_M form)
        {
            try
            {
                if (form.id == 0) //保存
                {

                    Diary diary = new Diary();
                    form.RecordDate = Lib.DateTimeFormat.ConvertDateTimeInt(DateTime.Now); //把datetime型的时间转化为int
                    diary.Pic = form.Pic;
                    diary.PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                    diary.RecordContent = form.RecordContent;
                    diary.RecordTitle = form.RecordTitle;
                    diary.RecordNo = form.RecordNo;
                    diary.RecordDate = form.RecordDate;
                    if (_app.EditDiary(diary))
                    {
                        return WxAlert("修改成功", "恭喜", "/AppStudent/Home/DiaryList");
                    }
                    else
                        return WxAlert("修改失败");
                }
                else
                {
                    return RedirectToNext(form._condition);
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion

        #region FirstCase(添加第一篇案例)
        public ActionResult FirstCase(string uid)
        {
            InitAppStudentLayout("添加第一篇案例");
            return View();
            return View(FirstCase_M.ToViewModel());
        }
        [HttpPost]
        public ActionResult FirstCase(FirstCase_M form)
        {
            return RedirectToNext(form._condition);
        }
        #endregion

        #region CaseList(案例列表)
        public ActionResult CaseList()
        {
            try
            {
                if (_app.GetCareerStatus(DataContext.UserID) == 13 || _app.GetCareerStatus(DataContext.UserID) == 14 || _app.GetCareerStatus(DataContext.UserID) == 15) //结束报名,结束招聘
                {
                    InitAppStudentLayout("案例列表");
                    if (_app.GetCaseCount(DataContext.UserID) == 0)//判断是否已经写过案例
                    {
                        return Redirect("/AppStudent/Home/AddCase");
                    }
                    else
                    {
                        var CaseCount = _app.GetCaseCount(DataContext.UserID);
                        var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                        var practicecase = _app.GetPraceticeCase(PracticeNo);
                        var careerstatus = _app.GetCareerStatus(DataContext.UserID);
                        return View(CaseList_M.ToViewModel(careerstatus, practicecase, CaseCount));
                    }
                }
                else
                {
                    return WxAlert("等到企业录取之后才能进行操作");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
            //InitAppStudentLayout("案例列表");
            //var CaseCount = _app.GetCaseCount(DataContext.UserID);
            //var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
            //var practicecase = _app.GetPraceticeCase(PracticeNo);
            //return View(CaseList_M.ToViewModel(practicecase, CaseCount));
        }
        [HttpPost]
        public ActionResult CaseList(CaseList_M form)
        {
            try
            {
                switch (form._condition)
                {
                    case "a"://添加案例
                        return RedirectToNext(form._condition);
                    case "b"://编辑查看案例
                        return RedirectToNext(form._condition, new { CaseNo = form.CaseNo, CaseName = form.CaseName });
                    case "c"://主页
                        return RedirectToNext(form._condition);
                    default:
                        {
                            if (_app.DeleteCase(DataContext.UserID, form.CaseNo))
                            {
                                return WxAlert("删除成功","提示", "/AppStudent/Home/CaseList");
                            }
                            else
                                return WxAlert("删除失败");
                        }
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion
        #region AddCase(添加案例Post)
        public ActionResult AddCase()
        {
            //通过用户ID，查找实学的批次，再查到该批次所对应案例ItemNo（ItemNo：每一个案例下面所对应的具体的几个小分支）        
            try
            {
                    if (_app.GetCareerStatus(DataContext.UserID)==13) //判断是否已经被岗位录取了
                    {
                        var caseitemno = _app.GetItemNobyUserID(DataContext.UserID);
                        InitAppStudentLayout("添加案例");
                        return View(AddCase_M.ToViewModel(caseitemno, _app.GetCaseCount(DataContext.UserID)));
                    }
                    else
                    {
                        return WxAlert("等到企业录取之后才能进行操作");
                    }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message, "提示", "-1");
            }
        }
        [HttpPost]
        public ActionResult AddCase(AddCase_M form, string piclist, List<string> ItemNo, List<string> ItemContent, string CaseName)
        {
            try
            {
                if (form._condition == "a") //提交
                {
                    var no = DateTime.Now.Random();
                    //var pic = "";
                    //for (int i = 0; i < piclist.Length; i++)
                    //{
                    //    pic += piclist[i];
                    //}
                 //   var pic = Lib.FileOperate.UploadMultiFile(uploaderInput, "/UserFiles/APP/Files/PracticeCases/");
                    var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                    List<PracticeCase> practicecases = new List<PracticeCase>();
                    for (int i = 0; i < ItemNo.Count; i++)
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
                            return WxAlert("请填写标题");
                        }
                        if (ItemContent[i] != "")
                        {
                            practicecase.ItemContent = ItemContent[i];
                        }
                        else
                        {
                            return WxAlert("请填写内容");
                        }
                        practicecase.CaseTime = DateTime.Now;
                        practicecase.Pic = piclist;
                        practicecases.Add(practicecase);
                    }
                    if (_app.AddPracticeCase(practicecases))
                    {
                        return WxAlert("添加成功", "恭喜", "/AppStudent/Home/CaseList");
                    }
                    return WxAlert("添加失败");
                }
                else
                {
                    return RedirectToNext(form._condition);
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion
        #region AddPracticeReport(添加实习报告Post)
        public ActionResult AddPracticeReport()
        {
            try
            {
                    if (_app.GetCareerStatus(DataContext.UserID) == 13 || _app.GetCareerStatus(DataContext.UserID) == 14 || _app.GetCareerStatus(DataContext.UserID) == 15) //判断是否已经被岗位录取了
                    {
                        //var caseitemno = _app.GetItemNobyUserID(DataContext.UserID);
                        InitAppStudentLayout("添加实习报告");
                        return View(AddPracticeReport_M.ToViewModel());
                    }
                    else
                    {
                        return WxAlert("等到企业录取之后才能进行操作");
                    }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult AddPracticeReport(AddPracticeReport_M form)
        {
            //return RedirectToNext(form._condition);
            try
            {
                if (form._condition == "b")
                {
                    if (!string.IsNullOrEmpty(form.PracticeReport))
                    {
                        form.UserID = DataContext.UserID;
                        if (_app.AddReport(form.ToModel()))
                        {
                            return WxAlert("保存成功", "恭喜", "/AppStudent/Home/PracticeReportDetail");
                        }
                        else
                        {
                            return WxAlert("添加失败");
                        }
                    }
                    else
                    {
                        return WxAlert("请填写实习报告");
                    }
                }
                else
                {
                    return RedirectToNext(form._condition);
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }

        #endregion
        #region EditCase(编辑案例Post)
        public ActionResult EditCase(int CaseNo, string CaseName)
        {
            try
            {
                InitAppStudentLayout("编辑案例");
                var Case = _app.GetCase(DataContext.UserID, CaseNo);
                var careerstatus = _app.GetCareerStatus(DataContext.UserID);
                return View(EditCase_M.ToViewModel(careerstatus, Case, CaseNo, CaseName, _app.GetCaseCount(DataContext.UserID)));
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult EditCase(EditCase_M form, List<string> ItemNo, List<string> ItemContent, string CaseName, List<int> CaseNo)
        {
            //InitAppStudentLayout("编辑案例");
            //     return RedirectToNext(form._condition);
            try
            {
                if (form.id == 0) //保存修改
                {
                    var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                    List<PracticeCase> practicecases = new List<PracticeCase>();
                    for (int i = 0; i < ItemNo.Count; i++)
                    {
                        PracticeCase practicecase = new PracticeCase();
                        practicecase.PracticeNo = PracticeNo;
                        practicecase.ItemNo = ItemNo[i];
                        practicecase.CaseNo = CaseNo[i];
                        practicecase.ItemContent = ItemContent[i];
                        practicecase.CaseName = CaseName;
                        practicecase.CaseTime = DateTime.Now;
                        practicecase.Pic = form.Pic;
                        practicecases.Add(practicecase);
                    }
                    if (_app.AddPracticeCase(practicecases))
                    {
                        return WxAlert("修改成功", "恭喜", "/AppStudent/Home/CaseList");
                    }
                    return WxAlert("添加失败");
                }
                else
                {
                    return RedirectToNext(form._condition);
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion
        //ViewModel为空

        #region EditPracticeReport(编辑实习报告)
        public ActionResult EditPracticeReport()
        {
            InitAppStudentLayout("编辑实习报告");
            var report = _app.AllComment(DataContext.UserID);
            return View(EditPracticeReport_M.ToViewModel(report));
        }
        [HttpPost]
        public ActionResult EditPracticeReport(EditPracticeReport_M form)
        {
            try
            {
                if (form._condition == "a")
                {
                    if (!string.IsNullOrEmpty(form.PracticeReport))
                    {
                        form.UserID = DataContext.UserID;
                        if (_app.AddReport(form.ToModel()))
                        {
                            return WxAlert("保存成功", "恭喜", "/AppStudent/Home/PracticeReportDetail");
                        }
                        else
                        {
                            return WxAlert("添加失败");
                        }
                    }
                    else
                    {
                        return WxAlert("请填写实习报告");
                    }
                }
                else
                {
                    return RedirectToNext(form._condition);
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion
        #region PracticeReportDetail(查看实习报告)
        public ActionResult PracticeReportDetail()
        {
            try
            {
                if (_app.GetCareerStatus(DataContext.UserID)==13|| _app.GetCareerStatus(DataContext.UserID) == 14|| _app.GetCareerStatus(DataContext.UserID) == 15) //判断是否已经被岗位录取了
                {
                    InitAppStudentLayout("查看实习报告");
                    var careerstatus = _app.GetCareerStatus(DataContext.UserID);
                    return View(PracticeReportDetail_M.ToViewModel(careerstatus, _app.AllComment(DataContext.UserID), DataContext.UserID));
                }
                else
                {
                    return WxAlert("等到企业录取之后才能进行操作");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult PracticeReportDetail(PracticeReportDetail_M form)
        {
            return RedirectToNext(form._condition);
        }
        #endregion

        #region EvaluateSchool(评价学校)
        public ActionResult EvaluateSchool()
        {
            try
            {
                if (_app.GetCareerStatus(DataContext.UserID) == 14 ||
                    _app.GetCareerStatus(DataContext.UserID) == 15 ||
                    _app.GetCareerStatus(DataContext.UserID) == 13 ||
                    _app.GetCareerStatus(DataContext.UserID) == 11) //判断是否已经实习结束
                {
                    if (_app.GetEvaluateSchoolCount(DataContext.UserID) == 0)
                    {
                        var schoolname = _app.GetSchoolName(DataContext.UserUnit);
                        var EvaluateSchoolItem = _app.GetEvaluateSchoolItem(_app.GetPracBatchIDByUserID(DataContext.UserID));
                        var _StuEvaSchoolGradeLevelItem = _app.GetStuEvaSchoolGradeLevelItem(_app.GetPracBatchIDByUserID(DataContext.UserID));
                        if (EvaluateSchoolItem.Count == 0)
                        {
                            return WxAlert("请联系相关人员设置评分项", "抱歉", "/AppStudent/Home/Main");
                        }
                        else
                        {
                            if (_StuEvaSchoolGradeLevelItem.Count == 0)
                            {
                                return WxAlert("请联系相关人员设置评分等级", "抱歉", "/AppStudent/Home/Main");
                            }
                            else
                            {
                                InitAppStudentLayout("评价学校");
                                return View(EvaluateSchool_M.ToViewModel(schoolname, EvaluateSchoolItem, _StuEvaSchoolGradeLevelItem));
                            }
                        }
                    }
                    else
                    {
                        return Redirect("/AppStudent/Home/ReviseEvaluateSchool");
                    }
                }
                else
                {
                    return WxAlert("请在实习结束后进行评价", "抱歉", "/AppStudent/Home/Main");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult EvaluateSchool(EvaluateSchool_M form, List<string> ItemNo, List<string> ItemContent, List<string> ItemGrade)
        {
            //return RedirectToNext(form._condition);
            try
            {
                if (form._condition == "a")
                {
                    return RedirectToNext(form._condition);
                }
                else
                {
                    var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                    List<StuEvaluateSchool> EvaluateSchools = new List<StuEvaluateSchool>();
                    for (int i = 0; i < ItemNo.Count; i++)
                    {
                        StuEvaluateSchool EvaluateSchool = new StuEvaluateSchool();
                        EvaluateSchool.PracticeNo = PracticeNo;
                        EvaluateSchool.ItemNo = ItemNo[i];
                        if (string.IsNullOrEmpty(ItemContent[i]))
                        {
                            return WxAlert("请将内容填写完整");
                        }
                        else
                        {
                            EvaluateSchool.ItemContent = ItemContent[i];
                        }
                        EvaluateSchool.ItemGrade = ItemGrade[i];
                        EvaluateSchools.Add(EvaluateSchool);
                    }
                    if (_app.AddStuEvaluateSchool(EvaluateSchools))
                    {
                        return WxAlert("评价成功", "恭喜", "/AppStudent/Home/EvaluateSchoolDetail");
                    }
                    {
                        return WxAlert("添加失败");
                    }
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion
        #region EvaluateSchoolDetail(查看评价学校)
        public ActionResult EvaluateSchoolDetail()
        {
            if (_app.GetCareerStatus(DataContext.UserID) == 14 ||
                    _app.GetCareerStatus(DataContext.UserID) == 15 ||
                    _app.GetCareerStatus(DataContext.UserID) == 13 ||
                    _app.GetCareerStatus(DataContext.UserID) == 11)
            //判断是否已经实习结束
            {
                InitAppStudentLayout("查看评价学校");
                var schoolname = _app.GetSchoolName(DataContext.UserUnit);
                var evaluateschool = _app.GetStuEvaluateSchool(DataContext.UserID);
                return View(EvaluateSchoolDetail_M.ToViewModel(schoolname, evaluateschool));
            }
            else
            {
                return WxAlert("请在实习结束后进行评价");
            }
        }
        [HttpPost]
        public ActionResult EvaluateSchoolDetail(EvaluateSchoolDetail_M form)
        {
            return RedirectToNext(form._condition);
        }
        #endregion
        #region ReviseEvaluateSchool(修改学校评价Post)
        public ActionResult ReviseEvaluateSchool()
        {
            try
            {
                var schoolname = _app.GetSchoolName(DataContext.UserUnit);
                if (!string.IsNullOrEmpty(schoolname))
                {
                    InitAppStudentLayout("修改学校评价");
                    var evaluateschool = _app.GetStuEvaluateSchool(DataContext.UserID);
                    //  var EvaluateSchoolItem = _app.GetEvaluateSchoolItem(_app.GetPracBatchIDByUserID(uid));
                    var _StuEvaSchoolGradeLevelItem = _app.GetStuEvaSchoolGradeLevelItem(_app.GetPracBatchIDByUserID(DataContext.UserID));
                    return View(ReviseEvaluateSchool_M.ToViewModel(schoolname, evaluateschool, _StuEvaSchoolGradeLevelItem));
                }
                else
                {
                    return WxAlert("学校不存在");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }

        }
        [HttpPost]
        public ActionResult ReviseEvaluateSchool(ReviseEvaluateSchool_M form, List<string> ItemNo)
        {
            try
            {
                if (form._condition == "b") //返回主页
                {
                    return RedirectToNext(form._condition);
                }
                else//保存修改
                {
                    var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                    List<StuEvaluateSchool> EvaluateSchools = new List<StuEvaluateSchool>();
                    for (int i = 0; i < ItemNo.Count; i++)
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
                        return WxAlert("修改成功", "提示", "/AppStudent/Home/EvaluateSchoolDetail");
                    }
                    return WxAlert("添加失败");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion

        #region EvaluateEnterprise(评价企业Post)
        public ActionResult EvaluateEnterprise()
        {
            try
            {
                if (_app.GetCareerStatus(DataContext.UserID) == 14 ||
                    _app.GetCareerStatus(DataContext.UserID) == 15 ||
                    _app.GetCareerStatus(DataContext.UserID) == 13) //判断是否已经实习结束
                {
                    if (_app.GetEvaluateEntCount(DataContext.UserID) == 0)
                    {
                        var EntName = _app.GetEntName(_app.GetPracticeNoByUserID(DataContext.UserID));
                        var EvaluateEntItem = _app.GetEvaluateEntItem(_app.GetPracBatchIDByUserID(DataContext.UserID));
                        var StuEvaEntGradeLevelItem = _app.GetStuEvaEntGradeLevelItem(_app.GetPracBatchIDByUserID(DataContext.UserID));
                        if (EvaluateEntItem.Count == 0)
                        {
                            return WxAlert("请联系相关人员设置评分项", "抱歉", "/AppStudent/Home/Main");
                        }
                        else
                        {
                            if (StuEvaEntGradeLevelItem.Count == 0)
                            {
                                return WxAlert("请联系相关人员设置评分等级", "抱歉", "/AppStudent/Home/Main");
                            }
                            else
                            {
                                InitAppStudentLayout("评价企业");
                                return View(EvaluateEnterprise_M.ToViewModel(EntName, EvaluateEntItem, StuEvaEntGradeLevelItem, DataContext.UserID));
                            }
                        }
                    }
                    else
                    {
                        return Redirect("/AppStudent/Home/EvaluateEnterpriseDetail");
                    }
                }
                else
                {
                    return WxAlert("请在实习结束后进行评价");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult EvaluateEnterprise(EvaluateEnterprise_M form, List<string> ItemNo, List<string> ItemContent, List<string> ItemGrade)
        {
            //return RedirectToNext(form._condition);
            try
            {
                if (form._condition == "a") //返回主页
                {
                    return RedirectToNext(form._condition);
                }
                else
                {
                    var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                    List<StuEvaluateEnt> EvaluateEnts = new List<StuEvaluateEnt>();
                    for (int i = 0; i < ItemNo.Count; i++)
                    {
                        StuEvaluateEnt EvaluateEnt = new StuEvaluateEnt();
                        EvaluateEnt.PracticeNo = PracticeNo;
                        EvaluateEnt.ItemNo = ItemNo[i];
                        if (string.IsNullOrEmpty(ItemContent[i]))
                        {
                            return WxAlert("请将内容填写完整");
                        }
                        else
                        {
                            EvaluateEnt.ItemContent = ItemContent[i];
                        }
                        EvaluateEnt.ItemGrade = ItemGrade[i];
                        EvaluateEnts.Add(EvaluateEnt);
                    }
                    if (_app.AddStuEvaluateEnt(EvaluateEnts))
                    {
                        //return Alert("添加成功");
                        return WxAlert("评价成功", "恭喜", "/AppStudent/Home/EvaluateEnterpriseDetail");
                    }
                    return WxAlert("添加失败");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion
        #region EvaluateEnterpriseDetail(查看评价企业)
        public ActionResult EvaluateEnterpriseDetail()
        {
            if (_app.GetCareerStatus(DataContext.UserID) == 14 ||
                    _app.GetCareerStatus(DataContext.UserID) == 15 ||
                    _app.GetCareerStatus(DataContext.UserID) == 13)
            //判断是否已经实习结束
            {
                InitAppStudentLayout("查看评价企业");
                var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                var EntName = _app.GetEntName(PracticeNo);
                var evaluateent = _app.GetStuEvaluateEnt(DataContext.UserID);
                return View(EvaluateEnterpriseDetail_M.ToViewModel(evaluateent, EntName));
            }
            else
            {
                return WxAlert("请在实习结束后进行评价");
            }
        }
        [HttpPost]
        public ActionResult EvaluateEnterpriseDetail(EvaluateEnterpriseDetail_M form)
        {
            return RedirectToNext(form._condition);
        }
        #endregion
        #region ReviseEvaluateEnterprise(修改评价企业Post)
        public ActionResult ReviseEvaluateEnterprise()
        {
            InitAppStudentLayout("修改评价企业");
            var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
            var EntName = _app.GetEntName(PracticeNo);
            var evaluateent = _app.GetStuEvaluateEnt(DataContext.UserID);
            var StuEvaEntGradeLevelItem = _app.GetStuEvaEntGradeLevelItem(_app.GetPracBatchIDByUserID(DataContext.UserID));
            return View(ReviseEvaluateEnterprise_M.ToViewModel(evaluateent, EntName, StuEvaEntGradeLevelItem));

        }
        [HttpPost]
        public ActionResult ReviseEvaluateEnterprise(ReviseEvaluateEnterprise_M form, List<string> ItemNo)
        {
            //    return RedirectToNext(form._condition);
            try
            {
                if (form._condition == "b") //返回
                {
                    return RedirectToNext(form._condition);
                }
                else
                {
                    var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
                    List<StuEvaluateEnt> EvaluateEnts = new List<StuEvaluateEnt>();
                    for (int i = 0; i < ItemNo.Count; i++)
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
                        return WxAlert("修改成功", "恭喜", "/AppStudent/Home/EvaluateEnterpriseDetail");
                    }
                    return WxAlert("添加失败");
                }
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        #endregion
        #region IdentificationTable(实习鉴定表)
        public ActionResult IdentificationTable()
        {
            try
            {
                //if (_app.GetCareerStatus(DataContext.UserID) == 14 || _app.GetCareerStatus(DataContext.UserID) == 15) //判断是否已经实习结束
                //{
                InitAppStudentLayout("实习鉴定表");
                var schoolname = _app.GetSchoolName(DataContext.UserUnit);
                var student = _app.StuAllInfo(DataContext.UserID);
                var comment = _app.AllComment(DataContext.UserID);
                var volunteer = _app.GetFormaVolunteer(DataContext.UserID);
                return View(IdentificationTable_M.ToViewModel(schoolname, volunteer, student, comment));
                //}
                //else
                //{
                //    return WxAlert("请在实习结束后查看鉴定表","提示","/AppStudent/Home/Main");
                //}
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult IdentificationTable(IdentificationTable_M form)
        {
            return RedirectToNext(form._condition);
        }
        #endregion
        #region PositionEvlTable(岗位评价表)
        public ActionResult PositionEvlTable()
        {
            try
            {
                //if (_app.GetCareerStatus(DataContext.UserID) == 14 || _app.GetCareerStatus(DataContext.UserID) == 15) //判断是否已经实习结束
                //{
                InitAppStudentLayout("岗位评价表");
                var EntEvaStu = _app.GetEntEvaluateStu(DataContext.UserID);
                var StuAllInfo = _app.StuAllInfo(DataContext.UserID);
                var volunteer = _app.GetFormaVolunteer(DataContext.UserID);
                var comment = _app.AllComment(DataContext.UserID);
                return View(PositionEvlTable_M.ToViewModel(EntEvaStu, StuAllInfo, volunteer, comment));
                //}
                //else
                //{
                //    return WxAlert("请在实习结束后查看评价表");
                //}
            }
            catch (Exception ex)
            {
                return WxAlert(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult PositionEvlTable(PositionEvlTable_M form)
        {
            return RedirectToNext(form._condition);
        }
        #endregion



        #region Resume(简历)
        public ActionResult Resume(string uid)
        {
            InitAppStudentLayout("简历");
            return View();
            var PracticeNo = _app.GetPracticeNoByUserID(uid);
            return View(Resume_M.ToViewModel(_app.GetResumes(PracticeNo)));

        }
        [HttpPost]
        public ActionResult Resume(Resume_M form)
        {
            return RedirectToNext(form._condition);
        }

        #endregion

        #region VolunteerDetail(志愿详情)
        public ActionResult VolunteerDetail(string EntPracNo, string PosID)
        {
            InitAppStudentLayout("志愿详情");
            var PracticeNo = _app.GetPracticeNoByUserID(DataContext.UserID);
            var Volunteer = _app.GetVolunteerDetail(PracticeNo, EntPracNo, PosID);
            return View(VolunteerDetail_M.ToViewModel(Volunteer));
        }
        [HttpPost]
        public ActionResult VolunteerDetail(VolunteerDetail_M form)
        {
            return RedirectToNext(form._condition);
        }

        #endregion

        #region 退出

        public ActionResult LogOut()
        {
            Session.Clear();
            return Redirect("/AppStudent/Visitor/Login");
        }

        #endregion



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
    }
}