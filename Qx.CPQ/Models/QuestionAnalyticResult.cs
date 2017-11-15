using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.CPQ.Models
{
    public class QuestionAnalyticResult
    {
        public List<QuestionOption> answers;//按照一道题后面接一个分析
        public int QuestionType;
        public string QuestionName;
        public string fill_Answer;//如果type==3，是填空题直接就是给fill——answer赋值就够了
    }
}
