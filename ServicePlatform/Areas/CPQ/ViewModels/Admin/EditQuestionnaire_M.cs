using Qx.CPQ.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.CPQ.ViewModels.Admin
{
    public class EditQuestionnaire_M
    {
        public static EditQuestionnaire_M ToViewModel(List<CPQ_C_QuestionDomain> QuestionDomain, List<CPQ_C_QuestionType> QuestionType,string ownerID,string QuestionnaireID,string QuestionnaireName,string QuestionnaireSummary)
        {
            var model = new EditQuestionnaire_M();

            model.QuestionDomainListItem = QuestionDomain.Select(a => new SelectListItem()
            {
                Text = a.QuestionDomainName,
                Value = a.QuestionDomainID.ToString()
            }).ToList();
            model.QuestionTypeListItem = QuestionType.Select(b => new SelectListItem()
            {
                Text = b.QuestionTypeName,
                Value = b.QuestionTypeID.ToString()
            }).ToList();
            model.ownerID = ownerID;
            model.QuestionnaireID = QuestionnaireID;
            model.QuestionnaireName = QuestionnaireName;
            model.QuestionnaireSummary = QuestionnaireSummary;
            return model;

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