using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ServicePlatform.Areas.App.ViewModel.Visitor
{
    public class Login_M
    {
        public static Login_M ToViewModel(List<SelectListItem> _UnitItems)
        {
            return new Login_M() { _UnitItems = _UnitItems};
        }

        public string UserID { get; set; }
        public string UserPwd { get; set; }
        public string UnitID { get; set; }
        public List<SelectListItem> _UnitItems { get; set; }
    }
}