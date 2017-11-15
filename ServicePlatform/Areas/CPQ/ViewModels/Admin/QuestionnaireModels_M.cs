using Qx.CPQ.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.CPQ.ViewModels.Admin
{
    public class QuestionnaireModels_M
    {
        public static QuestionnaireModels_M ToViewModel(List<CPQ_C_QuestionnaireDomain> QuestionnaireDomain,string ownerID)
        {
            var model = new QuestionnaireModels_M();

            model.QuestionnaireDomainListItem = QuestionnaireDomain.Select(a => new SelectListItem()
            {
                Text = a.QuestionnaireDomainName,
                Value = a.QuestionnaireDomainID.ToString()
            }).ToList();
            model.ownerID = ownerID;
            return model;

        }

        public List<SelectListItem> QuestionnaireDomainListItem;
        public int QuestionnaireDomain;
        public string ownerID;
    }
}