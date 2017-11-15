using Qx.CPQ.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class EditQuestionnaire_M
    {

        public static EditQuestionnaire_M ToViewModel( string ownerID, string QuestionnaireID, string QuestionnaireName, string QuestionnaireSummary)
        {
            var model = new EditQuestionnaire_M();

         
          
            model.ownerID = ownerID;
            model.QuestionnaireID = QuestionnaireID;
            model.QuestionnaireName = QuestionnaireName;
            model.QuestionnaireSummary = QuestionnaireSummary;
            return model;

        }

        public static CPQ_T_Questionnaire ToModel(CPQ_T_Questionnaire Questionnaire, string QuestionnaireName, string QuestionnaireSummary)
        {
            Questionnaire.QuestionnaireName = QuestionnaireName;
            Questionnaire.Summarize = QuestionnaireSummary;
            return Questionnaire;         

        }


        public List<SelectListItem> QuestionDomainListItem;
        public List<SelectListItem> QuestionTypeListItem;
        public string QuestionDomain;
        public string QuestionType;
        public string ownerID;
        public string QuestionnaireID;
        public string QuestionnaireName;
        public string QuestionnaireSummary;
    }
}