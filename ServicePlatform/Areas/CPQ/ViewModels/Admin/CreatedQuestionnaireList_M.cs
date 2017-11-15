using Qx.CPQ.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.CPQ.ViewModels.Admin
{
    public class CreatedQuestionnaireList_M
    {
        public static CreatedQuestionnaireList_M ToViewModel(List<CPQ_C_QuestionnaireDomain> QuestionnaireDomain,string owernid)
        {
            var model = new CreatedQuestionnaireList_M();

            model.QuestionnaireDomainListItem = QuestionnaireDomain.Select(a => new SelectListItem()
            {
                Text = a.QuestionnaireDomainName,
                Value = a.QuestionnaireDomainID.ToString()
            }).ToList();
            model.owernID = owernid;
            return model;

        }

        public List<SelectListItem> QuestionnaireDomainListItem;
        public int QuestionnaireDomain;
        public string owernID;
    }
}