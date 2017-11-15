
using ServicePlatform.Areas.CPQ.ViewModels.HomeCRUD;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.CPQ.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;


namespace ServicePlatform.Areas.CPQ.Controllers
{
    public class HomeCRUDController : BaseCPQController//针对游客后面还会针对表格list
    {
        //
        // GET: /CPQ/HomeCRUD/

         private IRepository<CPQ_T_AnswerSheet> _answerSheetRespository;
        private IRepository<CPQ_T_AttachQuestionToQuestionnaire> _attachQuestionToQuestionnaireRespository;
        private IRepository<CPQ_T_Questionnaire> _questionnaireRespository;
        private IRepository<CPQ_T_QuestionOption> _questionOptionRespository;
        private IRepository<CPQ_T_Question> _questionRespository;

        public HomeCRUDController(IRepository<CPQ_T_AnswerSheet> answerSheetRespository, IRepository<CPQ_T_AttachQuestionToQuestionnaire> attachQuestionToQuestionnaireRespository, IRepository<CPQ_T_Questionnaire> questionnaireRespository, IRepository<CPQ_T_QuestionOption> questionOptionRespository, IRepository<CPQ_T_Question> questionRespository)
         {
             _answerSheetRespository = answerSheetRespository;
             _attachQuestionToQuestionnaireRespository = attachQuestionToQuestionnaireRespository;
             _questionnaireRespository = questionnaireRespository;
             _questionOptionRespository = questionOptionRespository;
             _questionRespository = questionRespository;
         }
       
        #region 答题界面
        //游客根据链接或者点击首页的问卷进入答题时展示一张完成的问卷信息
        public ActionResult answerOneQuestionnaire(string id)
        {
            
            AnswerOneQuestionnaire_M pq =
            AnswerOneQuestionnaire_M.ToViewModel(_cpqService.QuestionnaireInfo(id));
            return View(pq);
            
        }

        public ActionResult answerOneQuestionnaire_Form(string AnswerContent)
        {   
            //先获取当前访客IP
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

            string returnContent=_crudService.answerOneQuestionnaire_Form(AnswerContent, ip);
            if (returnContent == "对不起，您已经填过这份问卷不能再填写了")
            {
                return Alert("对不起，您已经填过这份问卷不能再填写了");
            }
            else 
            {
                return RedirectToAction("thanksForAnswer", "Home", new { area="CPQ"});
            }
            
        }
        #endregion 答题界面



    }
}
