using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class ReviseEvaluateSchool_M : BaseViewModel
    {
        public static ReviseEvaluateSchool_M ToViewModel(string SchoolName,List<StuEvaluateSchool> evaluateschool, List<SelectListItem> _StuEvaSchoolGradeLevelItem)
        {
            return new ReviseEvaluateSchool_M()
            {
                _SchoolName= SchoolName,
                evaluateschool = evaluateschool,
              
               // EvaluateSchoolItem= EvaluateSchoolItem,
                _StuEvaSchoolGradeLevelItem= _StuEvaSchoolGradeLevelItem
            };
        }

        public string _SchoolName { get; set; }

        public List<StuEvaluateSchool> evaluateschool { get; set; }

       
        public string ItemContent { get; set; }

        public List<StuEvaluateSchoolItem> EvaluateSchoolItem { get; set; }

        public List<SelectListItem> _StuEvaSchoolGradeLevelItem { get; set; }
    }
}