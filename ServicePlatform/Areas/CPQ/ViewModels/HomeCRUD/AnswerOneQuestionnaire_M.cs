using Qx.CPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.HomeCRUD
{
    public class AnswerOneQuestionnaire_M
    {

        //public string QuestionnaireID;
        //public string QuestionnaireName;
        //public string Summarize;
        //public List<Question> Questions;
        public static AnswerOneQuestionnaire_M ToViewModel(Questionnaire Questionnaire)
        {
            return new AnswerOneQuestionnaire_M()
            {

                Questionnaire = Questionnaire
            };
        }
        public Questionnaire Questionnaire;
    }
}