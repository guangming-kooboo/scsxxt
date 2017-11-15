using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{

    
    public class AllEnt_List_M : BaseViewModel
    {
        public List<AllEnterprise> _EntpriseList { get; set; }
        public string Ent_No { get; set; }

        public static AllEnt_List_M ToViewModel(List<AllEnterprise> EntpriseList, string EntCategoryID,List<AllEnterprise> PositionCount, List<SelectListItem> EntCategory)
        {
            return new AllEnt_List_M()
            {
                _EntpriseList = EntpriseList,
              
                _PositionCount= PositionCount,
                EntCategory= EntCategory,
                EntCategoryID= EntCategoryID
            };
        }

        public string EntCategoryID { get; set; }
        public List<SelectListItem> EntCategory { get; set; }

        public List<AllEnterprise> _PositionCount { get; set; }
    }
}