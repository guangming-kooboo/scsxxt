using Qx.CPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class QuestionAnalyticResult_M
    {
       //public  List<QuestionOption> answers;//按照一道题后面接一个分析
       //public int QuestionType;
       //public string QuestionName;
       //public string fill_Answer;//如果type==3，是填空题直接就是给fill——answer赋值就够了 

        public static QuestionAnalyticResult_M ToViewModel(QuestionAnalyticResult showAnswer)
        {
            return new QuestionAnalyticResult_M()
            {

                showAnswer = showAnswer
            };
        }
        
       public QuestionAnalyticResult showAnswer;

    }
}