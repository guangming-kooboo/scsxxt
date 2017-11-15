using System.Collections.Generic;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class EvaluateSchoolDetail_M : BaseViewModel
    {
        public List<StuEvaluateSchool> EvaluateSchool;

        public static EvaluateSchoolDetail_M ToViewModel(string SchoolName,List<StuEvaluateSchool> evaluateschool)
        {
            return new EvaluateSchoolDetail_M()
            {
                EvaluateSchool= evaluateschool,
                _SchoolName= SchoolName
            };
        }

        public string _SchoolName { get; set; }
    }
}