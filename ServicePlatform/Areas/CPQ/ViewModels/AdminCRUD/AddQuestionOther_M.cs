
using Qx.CPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class AddQuestionOther_M
    {
        public string QuestionID;
        public string QuestionName;
        public List<QuestionOption> QuestionOptions;//先放这个，现在先用js实现回收这道题的答案，复习一下js遍历元素的方法  
    }
}