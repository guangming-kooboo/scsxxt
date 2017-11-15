using Qx.Permission.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Permision2.ViewModels.CRUD
{
    public class UserAdd_M
    {
        public User ToModel()
        {
            return new User() { UserID = userid, UserPwd = userpwd, UserType = usertype, NickName = nickname, Note = note };
        }
        [Display(Name = "单位管理员工号")]
        [StringLength(20)]
        public string userid { get; set; }
        [Display(Name = "单位管理员姓名")]
        [StringLength(100)]
        public string nickname { get; set; }
        [Display(Name = "单位管理员密码")]
        [Required]
        [StringLength(100)]
        public string userpwd { get; set; }
        [Display(Name = "用户类型")]
        public int usertype { get; set; }
        [Display(Name = "备注")]
        [Column(TypeName = "text")]
        public string note { get; set; }
    }
}