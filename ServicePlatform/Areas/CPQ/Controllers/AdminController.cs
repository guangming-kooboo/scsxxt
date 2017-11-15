using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;
using Qx.CPQ.Entity;
using Qx.Tools.CommonExtendMethods;
using ServicePlatform.Areas.CPQ.ViewModels.Admin;

namespace ServicePlatform.Areas.CPQ.Controllers
{
    public class AdminController : BaseCPQController// BaseCPQController //针对会员
    {
        //
        // GET: /CPQ/Admin/

        //导航页
        public ActionResult index()
        {
            Index_M index = Index_M.ToViewModel(DataContext.UserID);
            InitAdminLayout("问卷系统导航", "");
            return View(index);
        }


       

        //试卷模板库
        //CPQ/Admin/questionnaireModels?ReportID=CPQ问卷模板库&Params=;
        public ActionResult questionnaireModels(string ReportID = "CPQ问卷模板库", string Params = "问卷领域", int pageIndex = 1, int perCount = 10)
        {
            QuestionnaireModels_M qm = QuestionnaireModels_M.ToViewModel(_cpqService.GetQuestionnaireDomainList(),DataContext.UserID);                             
            InitReport("问卷模板库", "#", pageIndex, perCount);
            return View(qm);
        }


 

        //问题模板库
        //CPQ/Admin/questionModels?ReportID=CPQ问题模板库&Params=;
        public ActionResult questionModels(string ReportID , string Params , string QuestionnaireID,int pageIndex = 1,int perCount = 10)
        {
            QuestionModels_M qm = QuestionModels_M.ToViewModel(_cpqService.GetQuestionDomainList(),QuestionnaireID,DataContext.UserID); 
            InitReport("问题模板库", "#", pageIndex, perCount,"QuestionnaireID="+QuestionnaireID);
            return View(qm);
        }


        //已经创建的问卷列表
        //CPQ/Admin/createdQuestionnaireList?ReportID=CPQ已创建的问卷列表&Params=;
        public ActionResult createdQuestionnaireList(string ReportID = "CPQ已创建的问卷列表", string Params = "问卷创建者;问卷领域", int pageIndex = 1, int perCount = 10)
        {
            CreatedQuestionnaireList_M qm = CreatedQuestionnaireList_M.ToViewModel(_cpqService.GetQuestionnaireDomainList(), DataContext.UserID);
          
            InitReport("已创建问卷列表", "/CPQ/AdminCRUD/createQuestionnaireOutline", pageIndex, perCount);
            return View(qm);
        }


        //编辑问卷页面
        ///CPQ/Admin/editQuestionnaire?ReportID=CPQ编辑问卷&params=10385-11089-20160707-23456;;
        public ActionResult editQuestionnaire(string ReportID = "CPQ编辑问卷", string Params = "问卷id;题目领域;题目类型", int pageIndex = 1, int perCount = 10)
        {
            string[] paramsArray=Params.Split(';');
            string QuestionnaireID=paramsArray[0];
            //根据问卷ID获取问卷名称和简介
            string QuestionnaireName = _cpqService.GetQuestionnaireTitle(QuestionnaireID);
            string QuestionnaireSummary = _cpqService.GetQuestionnairesummary(QuestionnaireID);
            EditQuestionnaire_M qm = EditQuestionnaire_M.ToViewModel(_cpqService.GetQuestionDomainList(),_cpqService.GetQuestionTypeList(),DataContext.UserID,QuestionnaireID,QuestionnaireName,QuestionnaireSummary);
            InitReport("编辑问卷", "/CPQ/AdminCRUD/addQuestionSome?questionnaireID=" + QuestionnaireID, pageIndex, perCount);
            return View(qm);
        }
        


        //分析一份问卷的数据
        //CPQ/Admin/questionnaireAnalyticResult?ReportID=CPQ分析一份问卷&Params=10385-11089-20160707-23456
        public ActionResult questionnaireAnalyticResult(string ReportID = "CPQ分析一份问卷", string Params = "问卷id", int pageIndex = 1, int perCount = 10)
        {
            QuestionnaireAnalyticResult_M qm = QuestionnaireAnalyticResult_M.ToViewModel(DataContext.UserID, Params, _cpqService.GetQuestionnaireCollectNumber(Params));

            InitReport("分析一份问卷", "#", pageIndex, perCount);
            return View(qm);
        }



       



       
       

    }
}
