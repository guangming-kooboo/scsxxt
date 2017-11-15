using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class DetailEvaluateSchool_M
    {
        public List<StuEvaluateSchool> EvaluateSchool;

        public static DetailEvaluateSchool_M ToViewModel(List<StuEvaluateSchool> evaluateschool, string uid)
        {
            return new DetailEvaluateSchool_M()
            {
                EvaluateSchool= evaluateschool,
                uid=uid
            };
        }

        public string uid { get; set; }
    }
}