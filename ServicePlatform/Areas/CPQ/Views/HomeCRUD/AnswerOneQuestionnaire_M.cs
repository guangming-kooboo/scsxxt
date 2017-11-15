using ServicePlatform.Areas.CPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.Views.HomeCRUD
{
    public class AnswerOneQuestionnaire_M
    {

        public string QuestionnaireID;
        public string QuestionnaireName;
        public string Summarize;
        public List<Question> Questions;
    }
}