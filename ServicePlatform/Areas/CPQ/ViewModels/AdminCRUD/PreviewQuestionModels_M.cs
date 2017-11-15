using Qx.CPQ.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class PreviewQuestionModels_M
    {
        //public string QuestionnaireID;
        //public string QuestionID;
        //public string QuestionName;
        //public int QuestionType;
        //public List<QuestionOption> QuestionOptions;

        public static PreviewQuestionModels_M ToViewModel(Question Question, string QuestionnaireID)
        {
            return new PreviewQuestionModels_M()
            {

                Question = Question,
                QuestionnaireID = QuestionnaireID
            };
        }
        public Question Question;
        public string QuestionnaireID;
    }
}