﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Visitor
{
    public class ForgetPsw_M
    {
        public static ForgetPsw_M ToViewModel(List<SelectListItem> _UnitItems)
        {
            return new ForgetPsw_M() { _UnitItems = _UnitItems };
        }
        public string UnitID { get; set; }
        public string StuName { get; set; }
        public List<SelectListItem> _UnitItems { get; set; }

        [Required(ErrorMessage = "请填写用户名")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "请填写新密码")]
        public string UserPwd { get; set; }

        [Required(ErrorMessage = "请确定新密码")]
        public string SecondPwd { get; set; }
    }
}