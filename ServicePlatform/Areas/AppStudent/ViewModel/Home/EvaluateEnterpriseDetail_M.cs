using System.Collections.Generic;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class EvaluateEnterpriseDetail_M : BaseViewModel
    {
        public List<StuEvaluateEnt> EvaluateEnt;
        public  string uid { get; set; }
        public string EntName { get; set; }
        public static EvaluateEnterpriseDetail_M ToViewModel(List<StuEvaluateEnt> evaluateent,string EntName) 
        {
            return new EvaluateEnterpriseDetail_M()
            { 
                EvaluateEnt = evaluateent,
             
                EntName= EntName
            };
        }

    }
}