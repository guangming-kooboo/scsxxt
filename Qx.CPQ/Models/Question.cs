using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qx.CPQ.Models
{
    public class Question
    {
        
        public int Sequence;//一个问题的时候这个可以为空
        public string QuestionName;
        public int QuestionType;
        public String QuestionID;
        public int QuestionDomain;
        public int share ;
        public int Reference;
        public List<QuestionOption> QuestionOptions;
    }
}