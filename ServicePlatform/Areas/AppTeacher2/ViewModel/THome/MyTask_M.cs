using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class MyTask_M
    {
        public static MyTask_M ToViewModel(string uid)
        {
            return new MyTask_M()
            {
                uid= uid
            };
        }

        public string uid { get; set; }
    }
}