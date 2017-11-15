using Qx.CPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class PreviewQuestionnaire_M
    {

        public static PreviewQuestionnaire_M ToViewModel(Questionnaire Questionnaire)
        {
            return new PreviewQuestionnaire_M() {
              
                Questionnaire = Questionnaire,
               
            };
        }
        public Questionnaire Questionnaire;
       
        //public string QuestionnaireID;
        //public string QuestionnaireName;
        //public string Summarize;
        //public List<Question> Questions;
    }
}