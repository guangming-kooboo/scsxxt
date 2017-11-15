using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{

    
    public class AllEnterprise_M
    {
        public List<AllEnterprise>Enterprises { get; set; }
        public string Ent_No { get; set; }
        public string Uid { get; set; }
        public static AllEnterprise_M ToViewModel(List<AllEnterprise>enterprises,string uid, string EntCategoryID,List<AllEnterprise> PositionCount, List<SelectListItem> EntCategory)
        {
            return new AllEnterprise_M()
            {
                Enterprises = enterprises,
                Uid=uid,
                PositionCount= PositionCount,
                EntCategory= EntCategory,
                EntCategoryID= EntCategoryID
            };
        }

        public string EntCategoryID { get; set; }
        public List<SelectListItem> EntCategory { get; set; }

        public List<AllEnterprise> PositionCount { get; set; }
    }
}