using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qx.CPQ.Models
{
    public class Questionnaire
    {

        public string QuestionnaireID { get; set; }

        public string QuestionnaireName { get; set; }

    
        public string Summarize { get; set; }

        public int QuestionnaireDomain { get; set; }

        public int CreateTime { get; set; }

      
        public string OwnerID { get; set; }

        public int Status { get; set; }

      
        public string OwnerCompany { get; set; }

        public int share { get; set; }

        public int Reference { get; set; }

        public int CollectSeting { get; set; }

        public int CollectNumber { get; set; }

        public int IsLock { get; set; }

        public List<Question> Questions;
    }
}