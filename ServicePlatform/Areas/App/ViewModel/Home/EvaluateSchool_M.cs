using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EvaluateSchool_M
    {
        public string SchoolName { get; set; }

        public List<StuEvaluateSchoolItem> EvaluateSchoolItem { get; set; }
        public List<SelectListItem> _StuEvaSchoolGradeLevelItem { get; set; }

        public static EvaluateSchool_M ToViewModel( List<StuEvaluateSchoolItem> EvaluateSchoolItem, List<SelectListItem> _StuEvaSchoolGradeLevelItem, string uid)
        {
            return new EvaluateSchool_M()
            {
                EvaluateSchoolItem = EvaluateSchoolItem,
                _StuEvaSchoolGradeLevelItem = _StuEvaSchoolGradeLevelItem,
                uid = uid,
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
        public string uid { get; set; }
    }
}