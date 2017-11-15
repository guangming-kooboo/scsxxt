using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EditDetailEvaluateEnterprise＿Ｍ
    {
        public static EditDetailEvaluateEnterprise＿Ｍ ToViewModel(List<StuEvaluateEnt> evaluateent, string uid, string EntName, List<SelectListItem> StuEvaEntGradeLevelItem)
        {
            return new EditDetailEvaluateEnterprise＿Ｍ()
            {
                EvaluateEnt = evaluateent,
                uid= uid,
                EntName= EntName,
                _StuEvaEntGradeLevelItem= StuEvaEntGradeLevelItem
            };
        }

        public List<SelectListItem> _StuEvaEntGradeLevelItem { get; set; }

        public List<StuEvaluateEnt> EvaluateEnt { get; set; }

        public string uid { get; set; }

        public string EntName { get; set; }
    }
}