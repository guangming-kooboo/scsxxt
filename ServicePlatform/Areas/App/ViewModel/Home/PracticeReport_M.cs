using Core.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class PracticeReport_M
    {
        public static PracticeReport_M ToViewModel(T_StuBatchReg AllComment,string uid)
        {
            return new PracticeReport_M()
            {
                AllComment= AllComment,
                uid= uid
            };
        }

        public string uid { get; set; }

        public T_StuBatchReg AllComment { get; set; }
    }
}