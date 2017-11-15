using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;

namespace ServicePlatform.Areas.WbsHqu.ViewModels.FAQ
{
    public class FAQList_M
    {
        public static FAQList_M ToViewModel(List<SelectListItem> FAQtype,List<Qx.Scsxxt.Core.Services.FAQ> FAQList)
        {
            return new FAQList_M()
            {
                _FAQtype= FAQtype,
                _FAQList = FAQList
            };
        }

        public static FAQList_M ToViewModel(List<SelectListItem> FAQState,string stateId)
        {
            return new FAQList_M()
            {
                _FAQState = FAQState,
                stateId = stateId
            };
        }

        public string typeid { get; set; }
        [Display(Name = "")]
        public string stateId { get; set; }
        public List<SelectListItem> _FAQState { get; set; }
        public List<SelectListItem> _FAQtype { get; set; }
        public List<Qx.Scsxxt.Core.Services.FAQ> _FAQList { get; set; }

    }
}