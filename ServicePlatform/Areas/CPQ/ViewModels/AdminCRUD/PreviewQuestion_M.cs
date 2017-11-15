using Qx.CPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class PreviewQuestion_M
    {
        //public string QuestionID;      
        //public string QuestionName;
        //public int QuestionType;
        //public List<QuestionOption> QuestionOptions;
        public static PreviewQuestion_M ToViewModel(Question Question)
        {
            return new PreviewQuestion_M()
            {

                Question = Question,
               
            };
        }
        public Question Question;
        
    }
}