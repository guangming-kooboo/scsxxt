using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;
using System.Web.Mvc;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class EnterpriseList_M
    {
        public List<SelectListItem> _EntCategory;
        public List<Core.Model.Enterprise> _EntpriseList;
        public List<Core.Model.Enterprise> _PositionCount;

        public string EntCategoryID { get; set; }
        public static EnterpriseList_M ToViewModel(List<Core.Model.Enterprise> EntpriseList, List<SelectListItem> EntCategory, List<Core.Model.Enterprise> PositionCount)
        {
            return new EnterpriseList_M
            {
                _EntpriseList= EntpriseList,
                _EntCategory= EntCategory,
                _PositionCount= PositionCount
            };
        }
    }
}