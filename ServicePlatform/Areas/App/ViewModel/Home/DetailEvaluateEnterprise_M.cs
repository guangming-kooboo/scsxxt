using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class DetailEvaluateEnterprise_M
    {
        public List<StuEvaluateEnt> EvaluateEnt;
        public  string uid { get; set; }
        public string EntName { get; set; }
        public static DetailEvaluateEnterprise_M ToViewModel(List<StuEvaluateEnt> evaluateent,string uid,string EntName) 
        {
            return new DetailEvaluateEnterprise_M()
            { 
                EvaluateEnt = evaluateent,
                uid=uid,
                EntName= EntName
            };
        }

    }
}