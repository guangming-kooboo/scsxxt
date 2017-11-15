using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EditDetailEvaluateSchool_M
    {
        public static EditDetailEvaluateSchool_M ToViewModel(List<StuEvaluateSchool> evaluateschool, List<SelectListItem> _StuEvaSchoolGradeLevelItem, string uid)
        {
            return new EditDetailEvaluateSchool_M()
            {
                evaluateschool= evaluateschool,
                uid =uid,
               // EvaluateSchoolItem= EvaluateSchoolItem,
                _StuEvaSchoolGradeLevelItem= _StuEvaSchoolGradeLevelItem
            };
        }

        public List<StuEvaluateSchool> evaluateschool { get; set; }

        public string uid { get; set; }
        public string ItemContent { get; set; }

        public List<StuEvaluateSchoolItem> EvaluateSchoolItem { get; set; }

        public List<SelectListItem> _StuEvaSchoolGradeLevelItem { get; set; }
    }
}