using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Visitor
{
    public class Login2_M : BaseViewModel
    {
        public static Login2_M ToViewModel(List<SelectListItem> _UnitItems)
        {
            return new Login2_M()
            {
                _UnitItems = _UnitItems,
            };
        }

        public string UserID { get; set; }
        public string UserPwd { get; set; }
        public string UnitID { get; set; }
        public List<SelectListItem> _UnitItems { get; set; }
        public List<SelectListItem> _student=new List<SelectListItem>()
        {
            new SelectListItem(){Text="请选择",Value="请选择",Selected=true},
            new SelectListItem(){Text="1425121026",Value="1425121026"},
             new SelectListItem(){Text="1425121012",Value="1425121012"},
              new SelectListItem(){Text="1414141017",Value="1414141017",}
        };
    }
}