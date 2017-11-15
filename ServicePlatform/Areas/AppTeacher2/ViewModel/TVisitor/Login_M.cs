using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.TVisitor
{
    public class Login_M
    {
        [Required(ErrorMessage = "请输入账号")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        public string UserPwd { get; set; }

        [Required(ErrorMessage = "请选择学校")]
        public string UnitID { get; set; }
        public List<SelectListItem> _UnitItems { get; set; }
        public static Login_M ToViewModel(List<SelectListItem> _UnitItems)
        {
            return new Login_M() {
                UserID = "Liu",
                _UnitItems= _UnitItems,
                UserPwd = "Liu",
                UnitID = "10385"
            };
        }
    }
}