using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.WbsHqu.ViewModels.FAQ
{
    public class FaqTypeList_M
    {
        public static FaqTypeList_M ToViewModel(List<SelectListItem> FAQtype)
        {
            return new FaqTypeList_M()
            {
                _FAQtype= FAQtype
            };
        }

        public List<SelectListItem> _FAQtype { get; set; }
    }
}