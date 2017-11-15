using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class ReviseEvaluateEnterprise_M : BaseViewModel
    {
        public static ReviseEvaluateEnterprise_M ToViewModel(List<StuEvaluateEnt> evaluateent,  string EntName, List<SelectListItem> StuEvaEntGradeLevelItem)
        {
            return new ReviseEvaluateEnterprise_M()
            {
                EvaluateEnt = evaluateent,
              
                EntName= EntName,
                _StuEvaEntGradeLevelItem= StuEvaEntGradeLevelItem
            };
        }

        public List<SelectListItem> _StuEvaEntGradeLevelItem { get; set; }

        public List<StuEvaluateEnt> EvaluateEnt { get; set; }

      

        public string EntName { get; set; }
    }
}