using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.Models
{
    public class Question
    {
        public int Sequence;
        public string QuestionName;
        public int QuestionType;
        public String QuestionID;
        public List<QuestionOption> QuestionOptions;
    }
}