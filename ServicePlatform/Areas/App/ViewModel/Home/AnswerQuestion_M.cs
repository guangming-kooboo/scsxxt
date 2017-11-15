using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class AnswerQuestion_M
    {
        public static AnswerQuestion_M ToViewModel(string uid)
        {
            return new AnswerQuestion_M()
            {
                uid = uid
            };
        }
        public string uid { get; set; }
    }
}