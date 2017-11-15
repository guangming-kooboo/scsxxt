using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.Admin
{
    public class QuestionnaireAnalyticResult_M
    {
        public static QuestionnaireAnalyticResult_M ToViewModel(string owernid,string QuestionnaireID,int collectNumber)
        {
            var model = new QuestionnaireAnalyticResult_M();

            model.QuestionnaireID = QuestionnaireID;
            model.owernID = owernid;
            model.collectNumber = collectNumber;
            return model;

        }

        public string QuestionnaireID;
        public string owernID;
        public int collectNumber;
    }
}