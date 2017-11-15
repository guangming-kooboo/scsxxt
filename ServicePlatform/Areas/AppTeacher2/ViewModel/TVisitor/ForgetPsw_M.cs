using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.TVisitor
{
    public class ForgetPsw_M
    {
        public static ForgetPsw_M ToViewModel(List<SelectListItem> SchoolIItems)
        {
            return new ForgetPsw_M()
            {
                _SchoolIItems= SchoolIItems
            };
        }

        public string schoolid { get; set; }
        public List<SelectListItem> _SchoolIItems { get; set; }
    }
}