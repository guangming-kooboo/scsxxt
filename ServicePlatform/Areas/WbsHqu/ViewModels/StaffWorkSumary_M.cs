using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class StaffWorkSumary_M
    {
        public static StaffWorkSumary_M ToViewModel(string wbstaskId)
        {
            return new StaffWorkSumary_M() {WbsTaskID=wbstaskId };
        }
        public string WbsTaskID { get; set; }
    }
}