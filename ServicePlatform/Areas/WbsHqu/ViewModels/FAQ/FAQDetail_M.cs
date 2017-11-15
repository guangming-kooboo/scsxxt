using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;

namespace ServicePlatform.Areas.WbsHqu.ViewModels.FAQ
{
    public class FAQDetail_M
    {
        public static FAQDetail_M ToViewModel(List<SelectListItem> FAQtype,Qx.Scsxxt.Core.Services.FAQ DetailFAQ)
        {
            return new FAQDetail_M()
            {
                _FAQtype= FAQtype,
                _DetailFAQ = DetailFAQ
            };
        }

        public List<SelectListItem> _FAQtype { get; set; }
        public string typeid { get; set; }

        public Qx.Scsxxt.Core.Services.FAQ _DetailFAQ { get; set; }
    }
}