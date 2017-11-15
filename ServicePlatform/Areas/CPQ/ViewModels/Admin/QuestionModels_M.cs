using Qx.CPQ.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.CPQ.ViewModels.Admin
{
    public class QuestionModels_M
    {
        public static QuestionModels_M ToViewModel(List<CPQ_C_QuestionDomain> QuestionDomain, string QuestionnaireID, string ownerID)
        {
            var model = new QuestionModels_M();

            model.QuestionDomainListItem = QuestionDomain.Select(a => new SelectListItem()
            {
                Text = a.QuestionDomainName,
                Value = a.QuestionDomainID.ToString()
            }).ToList();
            model.QuestionnaireID = QuestionnaireID;
            model.ownerID = ownerID;
            return model;

        }

        public List<SelectListItem> QuestionDomainListItem;
        public int QuestionDomain;
        public string QuestionnaireID;
        public string ownerID;
    }
}