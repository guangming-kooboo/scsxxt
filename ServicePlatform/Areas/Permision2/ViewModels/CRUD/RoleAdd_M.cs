﻿using Qx.Permission.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.Permision2.ViewModels.CRUD
{
    public class RoleAdd_M
    {
        public Role ToModel()
        {
            return new Role() { RoleID = roleid, Name = name, IsDefault = isDefault, RoleType = roletype, subSystem = subSystem };
        }
        [StringLength(20)]
        [Display(Name = "角色编号")]
        public string roleid { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "角色名称")]
        public string name { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "角色类型")]
        public string roletype { get; set; }
        
        [StringLength(20)]
        [Display(Name = "子系统")]
        public string subSystem { get; set; }
        [Display(Name = "是否默认")]
        public int? isDefault { get; set; }
        public List<SelectListItem> selectItems = new List<SelectListItem>(){
                  new SelectListItem(){Text="是",Value="1",Selected=true},
                  new SelectListItem(){Text="否",Value="0"}};

    }
}