﻿using Qx.Permission.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.CRUD
{
    public class UserRoleAdd_M
    {
        public UserRole ToModel()
        {
            return new UserRole() { RoleID = roleid, UserRoleID = UserRoleID, UserID = userid, Note = note };
        }
        public static UserRoleAdd_M ToViewModel(string userid,string  roleid,string rolename)
        {
            return new UserRoleAdd_M() { userid = userid ,roleid=roleid,_rolename=rolename};
        }
        public static UserRoleAdd_M ToViewModel(string userid)
        {
            return new UserRoleAdd_M() { userid = userid};
        }
        [StringLength(40)]
        public string UserRoleID { get; set; }
        [Display(Name = "用户编号")]
        [Required]
        [StringLength(20)]
        public string userid { get; set; }
        [Display(Name = "角色名称")]
        public string _rolename { get; set; }
        [Display(Name = "角色编号")]
        [Required]
        [StringLength(20)]
        public string roleid { get; set; }
        [Display(Name = "备注")]
        public string note { get; set; }
    }
}