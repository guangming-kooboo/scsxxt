using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EvaluateEnterprise_M
    {
        public string EntName { get; set; }

        public List<StuEvaluateEntItem> EvaluateEntItem { get; set; }
        public List<SelectListItem> _StuEvaEntGradeLevelItem { get; set; }

        public static EvaluateEnterprise_M ToViewModel(string EntName, List<StuEvaluateEntItem> EvaluateEntItem, List<SelectListItem> _StuEvaEntGradeLevelItem,string uid)
        {
            return new EvaluateEnterprise_M()
            {
                EvaluateEntItem = EvaluateEntItem,
                _StuEvaEntGradeLevelItem = _StuEvaEntGradeLevelItem,
                uid=uid,
                EntName= EntName
            };
        }

        public StuEvaluateEnt ToModel()
        {
            return new StuEvaluateEnt()
            {
                PracticeNo = PracticeNo,
                ItemNo = ItemNo,
                ItemContent = ItemContent,
                ItemGrade = ItemGrade
            };
        }
        public  string PracticeNo { get; set; }
        public string ItemNo { get; set; }

        [Required(ErrorMessage = "请填写内容")]
        public string ItemContent { get; set; }

        [Required(ErrorMessage = "请选择等级")]
        public string ItemGrade { get; set; }
        public string uid { get; set; }
    }
}