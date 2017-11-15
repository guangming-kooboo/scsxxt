using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class EvaluateSchool_M : BaseViewModel
    {
        public string _schoolname;
        public string SchoolName { get; set; }

        public List<StuEvaluateSchoolItem> EvaluateSchoolItem { get; set; }
        public List<SelectListItem> _StuEvaSchoolGradeLevelItem { get; set; }

        public static EvaluateSchool_M ToViewModel( string schoolname,List<StuEvaluateSchoolItem> EvaluateSchoolItem, List<SelectListItem> _StuEvaSchoolGradeLevelItem)
        {
            return new EvaluateSchool_M()
            {
                _schoolname= schoolname,
                EvaluateSchoolItem = EvaluateSchoolItem,
                _StuEvaSchoolGradeLevelItem = _StuEvaSchoolGradeLevelItem,
         
             //   EntName = EntName
            };
        }

        public StuEvaluateSchool ToModel()
        {
            return new StuEvaluateSchool()
            {
                PracticeNo = PracticeNo,
                ItemNo = ItemNo,
                ItemContent = ItemContent,
                ItemGrade = ItemGrade
            };
        }
        public string PracticeNo { get; set; }
        public string ItemNo { get; set; }

        [Required(ErrorMessage = "请填写内容")]
        public string ItemContent { get; set; }

        [Required(ErrorMessage = "请选择等级")]
        public string ItemGrade { get; set; }
   
    }
}