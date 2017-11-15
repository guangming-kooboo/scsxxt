using Qx.CPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class PreviewquestionnaireModels_M
    {
        //public string QuestionnaireID;
        //public string QuestionnaireName;
        //public string Summarize;
        //public List<Question> Questions;

        public static PreviewquestionnaireModels_M ToViewModel(Questionnaire Questionnaire)
        {
            return new PreviewquestionnaireModels_M()
            {

                Questionnaire = Questionnaire
            };
        }
        public Questionnaire Questionnaire;
    }
}