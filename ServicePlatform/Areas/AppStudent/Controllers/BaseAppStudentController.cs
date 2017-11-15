using System.Collections.Generic;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Controllers.Base;
using System.Web.Mvc;
using Qx.Tools;

namespace ServicePlatform.Areas.AppStudent.Controllers
{
    public class BaseAppStudentController : BaseController
    {
     
        protected string _area = "AppStudent";
        protected string _vController = "Visitor";
        protected string _hController = "Home";
        protected AccountContext AccountContext
        {
            get
            {
                return Session["AccountContext"] as AccountContext;
            }
            set
            {
                Session["AccountContext"] = value;
            }
        }
   
        protected void AddHomeUrl(string title, string actionName)
        {
           AddUrl(title, actionName, _hController, _area);
        }
        protected void AddVisitorUrl(string title, string actionName)
        {
            AddUrl(title, actionName, _vController, _area);
        }



        #region  路由相关
        private RouteTable _table;
        private Route NewUrlParam(string action, bool isVisitor = false)
        {
            var route = isVisitor ? new Route(action, _vController, _area) :
                new Route(action, _hController, _area);
            _table.Regist(route);
            return route;
        }
        private void ConfigRoute()
        {
            _table = new RouteTable();

            #region 枚举所有路由并注册
            var login = NewUrlParam("Login", true);
            var login2 = NewUrlParam("Login2", true);
            var forgetPsw = NewUrlParam("ForgetPsw", true);
            var revisePassword = NewUrlParam("RevisePassword", true);
            var main = NewUrlParam("Main");
            var completeInfo = NewUrlParam("CompleteInfo");
            var answerQuestions = NewUrlParam("AnswerQuestions");
            var allEnt_List = NewUrlParam("AllEnt_List");
            var ent_Detail = NewUrlParam("Ent_Detail");
            var ent_Des = NewUrlParam("Ent_Des");
            var ent_Post = NewUrlParam("Ent_Post");
            var ent_TrainingPlan = NewUrlParam("Ent_TrainingPlan");
            var ent_SucceedCase = NewUrlParam("Ent_SucceedCase");
            var ent_Salary = NewUrlParam("Ent_Salary");
            var ent_StaffAcc = NewUrlParam("Ent_StaffAcc");
            var ent_Staff= NewUrlParam("Ent_Staff");
            var ent_Download= NewUrlParam("Ent_Download");
            var ent_FAQ = NewUrlParam("Ent_FAQ");
            var recEnt_List= NewUrlParam("RecEnt_List");
            var recEnt_Detail = NewUrlParam("RecEnt_Detail");
            var recEnt_Des = NewUrlParam("RecEnt_Des");
            var recEnt_Post = NewUrlParam("RecEnt_Post");
            var recEnt_TrainingPlan= NewUrlParam("RecEnt_TrainingPlan");
            var recEnt_Salary = NewUrlParam("RecEnt_Salary");
            var recEnt_StaffAcc = NewUrlParam("RecEnt_StaffAcc");
            var recEnt_Staff = NewUrlParam("RecEnt_Staff");
            var recEnt_SucceedCase = NewUrlParam("RecEnt_SucceedCase");
            var recEnt_Download = NewUrlParam("RecEnt_Download");
            var recEnt_FAQ = NewUrlParam("RecEnt_FAQ");
            var personalSpace = NewUrlParam("PersonalSpace");
            var prepareVolunteer = NewUrlParam("PrepareVolunteer");
            var prepareVolunteerList = NewUrlParam("PrepareVolunteerList");
            var formalVolunteer = NewUrlParam("FormalVolunteer");
            var practiceOverView = NewUrlParam("PracticeOverView");
            var reviseInfo = NewUrlParam("ReviseInfo");
            var revisePsw = NewUrlParam("RevisePsw");
            var about = NewUrlParam("About");
            var firstDiary = NewUrlParam("FirstDiary");
            var diaryList = NewUrlParam("DiaryList");
            var addDiary = NewUrlParam("AddDiary");
            var editDiary = NewUrlParam("EditDiary");
            var firstCase = NewUrlParam("FirstCase");
            var caseList = NewUrlParam("CaseList");
            var addCase = NewUrlParam("AddCase");
            var editCase = NewUrlParam("EditCase");
            var addPracticeReport = NewUrlParam("AddPracticeReport");
            var editPracticeReport = NewUrlParam("EditPracticeReport");
            var practiceReportDetail = NewUrlParam("PracticeReportDetail");
            var evaluateSchool = NewUrlParam("EvaluateSchool");
            var evaluateSchoolDetail = NewUrlParam("EvaluateSchoolDetail");
            var reviseEvaluateSchool = NewUrlParam("ReviseEvaluateSchool");
            var evaluateEnterprise = NewUrlParam("EvaluateEnterprise");
            var evaluateEnterpriseDetail = NewUrlParam("EvaluateEnterpriseDetail");
            var reviseEvaluateEnterprise = NewUrlParam("ReviseEvaluateEnterprise");
            var identificationTable  = NewUrlParam("IdentificationTable");
            var positionEvlTable = NewUrlParam("PositionEvlTable");
            var resume = NewUrlParam("Resume");
            var volunteerDetail = NewUrlParam("VolunteerDetail");
            var agreeRecruit = NewUrlParam("AgreeRecruit");
            var disagreeRecruit = NewUrlParam("DisagreeRecruit");
            #endregion



            #region 添加映射关系
            #region Login
            login.AddFriend(forgetPsw, "a");
            //login.AddFriend(completeInfo, "b");
            login.AddFriend(main, "c");
            login.AddFriend(login2, "d");
            #endregion
            #region Login2
            login2.AddFriend(forgetPsw, "a");
           // login2.AddFriend(completeInfo, "b");
            login2.AddFriend(main, "c");
            #endregion
            #region ForgetPsw
            forgetPsw.AddFriend(login, "a");
            forgetPsw.AddFriend(revisePassword, "b");
            #endregion
            #region revisePassword
            revisePassword.AddFriend(login,"a");
            #endregion
            #region CompleteInfo
            completeInfo.AddFriend(main, "a");
            completeInfo.AddFriend(login, "b");
            #endregion
            #region AnswerQuestions
            answerQuestions.AddFriend(personalSpace, "a");
            #endregion
            #region AllEnt_List
            allEnt_List.AddFriend(main, "a");
            allEnt_List.AddFriend(ent_Detail, "b");
            allEnt_List.AddFriend(allEnt_List, "c");
            #endregion
            #region Ent_Detail
            ent_Detail.AddFriend(ent_Des, "a");
            ent_Detail.AddFriend(ent_Post, "b");
            ent_Detail.AddFriend(ent_TrainingPlan, "c");
            ent_Detail.AddFriend(ent_SucceedCase, "d");
            ent_Detail.AddFriend(ent_Salary, "e");
            ent_Detail.AddFriend(ent_StaffAcc, "f");
            ent_Detail.AddFriend(ent_Staff, "g");
            ent_Detail.AddFriend(ent_Download, "h");
            ent_Detail.AddFriend(ent_FAQ, "i");
            ent_Detail.AddFriend(allEnt_List, "j");
            #endregion
            #region Ent_Des
            ent_Des.AddFriend(ent_Detail, "a");
            #endregion
            #region Ent_Post
            ent_Post.AddFriend(ent_Detail, "a");
            #endregion
            #region Ent_TrainingPlan
            ent_TrainingPlan.AddFriend(ent_Detail, "a");
            #endregion
            #region Ent_SucceedCase
            ent_SucceedCase.AddFriend(ent_Detail, "a");
            #endregion
            #region Ent_Salary
            ent_Salary.AddFriend(ent_Detail, "a");
            #endregion
            #region Ent_StaffAcc
            ent_StaffAcc.AddFriend(ent_Detail, "a");
            #endregion
            #region Ent_Staff
            ent_Staff.AddFriend(ent_Detail, "a");
            #endregion
            #region Ent_Download
            ent_Download.AddFriend(ent_Detail, "a");
            #endregion
            #region Ent_FAQ
            ent_FAQ.AddFriend(ent_Detail, "a");
            #endregion
            #region Main
            main.AddFriend(allEnt_List, "a");
            main.AddFriend(prepareVolunteer, "b");
            main.AddFriend(personalSpace, "c");
            main.AddFriend(addDiary, "d");
            main.AddFriend(diaryList, "e");
            main.AddFriend(addCase, "f");
            main.AddFriend(caseList, "g");
            main.AddFriend(addPracticeReport, "h");
            main.AddFriend(practiceReportDetail, "i");
            main.AddFriend(evaluateSchool, "j");
            main.AddFriend(evaluateSchoolDetail, "k");
            main.AddFriend(evaluateEnterprise, "l");
            main.AddFriend(evaluateEnterpriseDetail, "m");
            main.AddFriend(identificationTable, "n");
            main.AddFriend(positionEvlTable, "o");
            #endregion
            #region RecEnt_List
            recEnt_List.AddFriend(main, "a");
            recEnt_List.AddFriend(recEnt_Detail, "b");
            #endregion
            #region RecEnt_Detail
            recEnt_Detail.AddFriend(recEnt_Des, "a");
            recEnt_Detail.AddFriend(recEnt_Post, "b");
            recEnt_Detail.AddFriend(recEnt_TrainingPlan, "c");
            recEnt_Detail.AddFriend(recEnt_Salary, "d");
            recEnt_Detail.AddFriend(recEnt_StaffAcc, "e");
            recEnt_Detail.AddFriend(recEnt_Staff, "f");
            recEnt_Detail.AddFriend(recEnt_SucceedCase, "g");
            recEnt_Detail.AddFriend(recEnt_Download, "h");
            recEnt_Detail.AddFriend(recEnt_FAQ, "i");
            recEnt_Detail.AddFriend(recEnt_List, "j");
            #endregion
            #region RecEnt_Des
            recEnt_Des.AddFriend(recEnt_Detail, "a");
            #endregion
            #region RecEnt_Post
            recEnt_Post.AddFriend(recEnt_Detail, "a");
            #endregion
            #region RecEnt_TrainingPlan
            recEnt_TrainingPlan.AddFriend(recEnt_Detail, "a");
            #endregion
            #region RecEnt_Salary
            recEnt_Salary.AddFriend(recEnt_Detail, "a");
            #endregion
            #region RecEnt_StaffAcc
            recEnt_StaffAcc.AddFriend(recEnt_Detail, "a");
            #endregion
            #region RecEnt_Staff
            recEnt_Staff.AddFriend(recEnt_Detail, "a");
            #endregion
            #region RecEnt_SucceedCase
            recEnt_SucceedCase.AddFriend(recEnt_Detail, "a");
            #endregion
            #region RecEnt_Download
            recEnt_Download.AddFriend(recEnt_Detail, "a");
            #endregion
            #region RecEnt_FAQ
            recEnt_FAQ.AddFriend(recEnt_Detail, "a");
            #endregion
            #region PersonalSpace
            personalSpace.AddFriend(main, "a");
            personalSpace.AddFriend(prepareVolunteerList, "b");
            personalSpace.AddFriend(formalVolunteer, "c");
            personalSpace.AddFriend(practiceOverView, "d");
            personalSpace.AddFriend(reviseInfo, "e");
            personalSpace.AddFriend(revisePsw, "f");
            personalSpace.AddFriend(about, "g");
            personalSpace.AddFriend(login, "h");
            personalSpace.AddFriend(answerQuestions, "i");
            personalSpace.AddFriend(login2, "j");
            #endregion
            #region PrepareVolunteer
            prepareVolunteer.AddFriend(personalSpace, "a");
            prepareVolunteer.AddFriend(recEnt_List, "b");
            prepareVolunteer.AddFriend(main, "c");
            prepareVolunteer.AddFriend(formalVolunteer, "d");
            #endregion
            #region PrepareVolunteerList
            prepareVolunteerList.AddFriend(personalSpace, "a");
            prepareVolunteerList.AddFriend(formalVolunteer, "d");
            prepareVolunteerList.AddFriend(prepareVolunteerList, "-");
            #endregion
            #region FormalVolunteer
            formalVolunteer.AddFriend(personalSpace, "a");
            formalVolunteer.AddFriend(volunteerDetail, "b");
            formalVolunteer.AddFriend(formalVolunteer, "-");
            #endregion
            #region PracticeOverView
            practiceOverView.AddFriend(personalSpace, "a");
            practiceOverView.AddFriend(prepareVolunteer, "b");
            practiceOverView.AddFriend(recEnt_Detail, "c");
            practiceOverView.AddFriend(diaryList, "d");
            practiceOverView.AddFriend(caseList, "e");
            practiceOverView.AddFriend(practiceReportDetail, "f");
            practiceOverView.AddFriend(identificationTable, "g");
            practiceOverView.AddFriend(positionEvlTable, "h");
            practiceOverView.AddFriend(evaluateSchoolDetail, "i");
            practiceOverView.AddFriend(evaluateEnterpriseDetail, "j");
            practiceOverView.AddFriend(resume, "k");
            practiceOverView.AddFriend(recEnt_List, "l");
            practiceOverView.AddFriend(volunteerDetail, "m");
            practiceOverView.AddFriend(formalVolunteer, "n");
            #endregion
            #region ReviseInfo
            reviseInfo.AddFriend(personalSpace, "a");
            reviseInfo.AddFriend(reviseInfo, "-");
            #endregion
            #region RevisePsw
            revisePsw.AddFriend(personalSpace, "a");
            revisePsw.AddFriend(revisePsw, "-");
            #endregion
            #region About
            about.AddFriend(personalSpace, "a");
            #endregion
            #region FirstDiary
            firstDiary.AddFriend(addDiary, "a");
            firstDiary.AddFriend(main, "b");
            #endregion
            #region DiaryList
            diaryList.AddFriend(main, "a");
            diaryList.AddFriend(editDiary, "b");
            diaryList.AddFriend(addDiary, "c");
            diaryList.AddFriend(diaryList, "-");
            #endregion
            #region AddDiary
            addDiary.AddFriend(diaryList, "a");
            addDiary.AddFriend(main, "b");
            #endregion
            #region EditDiary
            editDiary.AddFriend(diaryList, "a");
            #endregion
            #region FirstCase
            firstCase.AddFriend(addCase, "a");
            firstCase.AddFriend(main, "b");
            #endregion
            #region CaseList
            caseList.AddFriend(addCase, "a");
            caseList.AddFriend(editCase, "b");
            caseList.AddFriend(main, "c");
            caseList.AddFriend(caseList, "-");
            #endregion
            #region AddCase
            addCase.AddFriend(caseList, "a");
            addCase.AddFriend(main, "b");
            #endregion
            #region EditCase
            editCase.AddFriend(caseList, "a");
            #endregion
            #region AddPracticeReport
            addPracticeReport.AddFriend(main, "a");
            addPracticeReport.AddFriend(practiceReportDetail, "b");
            #endregion
            #region EditPracticeReport
            editPracticeReport.AddFriend(practiceReportDetail, "a");
            editPracticeReport.AddFriend(main, "b");
            #endregion
            #region PracticeReportDetail
            practiceReportDetail.AddFriend(main, "a");
            practiceReportDetail.AddFriend(editPracticeReport, "b");
            #endregion
            #region EvaluateSchool
            evaluateSchool.AddFriend(main, "a");
            evaluateSchool.AddFriend(evaluateSchoolDetail, "b");
            //evaluateSchool.AddFriend(reviseEvaluateSchool, "c");
            #endregion
            #region EvaluateSchoolDetail
            evaluateSchoolDetail.AddFriend(main, "a");
            evaluateSchoolDetail.AddFriend(reviseEvaluateSchool, "b");
            #endregion
            #region ReviseEvaluateSchool
            reviseEvaluateSchool.AddFriend(evaluateSchoolDetail, "a");
            reviseEvaluateSchool.AddFriend(main, "b");
            #endregion
            #region EvaluateEnterprise
            evaluateEnterprise.AddFriend(main, "a");
            evaluateEnterprise.AddFriend(evaluateEnterpriseDetail, "b");
            #endregion
            #region EvaluateEnterpriseDetail
            evaluateEnterpriseDetail.AddFriend(main, "a");
            evaluateEnterpriseDetail.AddFriend(reviseEvaluateEnterprise, "b");
            #endregion
            #region ReviseEvaluateEnterprise
            reviseEvaluateEnterprise.AddFriend(evaluateEnterpriseDetail, "a");
            reviseEvaluateEnterprise.AddFriend(main, "b");
            #endregion
            #region IdentificationTable
            identificationTable.AddFriend(main, "a");
            #endregion
            #region PositionEvlTable
            positionEvlTable.AddFriend(main, "a");
            #endregion
            #region Resume
            resume.AddFriend(practiceOverView, "a");
            #endregion
            #region VolunteerDetail
            volunteerDetail.AddFriend(formalVolunteer, "a");
            #endregion

            #region ForgetPsw
            forgetPsw.AddFriend(login, "a");
            #endregion


            #endregion


        }
        private string ToNext(string condition , object targetParam )
        {
            ConfigRoute();
            return _table.GetTarget(CurrentFullUrl(), condition,targetParam);
        }

        protected ActionResult RedirectToNext(string condition = "", object targetParam = null)
        {
            return Redirect(ToNext(condition, targetParam));
        }
        #endregion
        protected void InitReport(string Title, string AddLink, int pageIndex, int perCount, bool showDeafultButton = true, string ExtraParam = "")
        {
            base.InitReport(Title, AddLink, pageIndex, perCount, ExtraParam, "Qx.Wbs.Hqu");
        }
       
    }
}