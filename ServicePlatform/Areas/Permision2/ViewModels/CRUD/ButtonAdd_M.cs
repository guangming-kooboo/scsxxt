﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Qx.Permission.Entity;

namespace ServicePlatform.Areas.Permision2.ViewModels.CRUD
{
    public class ButtonAdd_M
    {
        public Button ToModel()
        {
            return new Button() {  ButtonID=buttonid, MenuID=menuid, Name=name, Value=value, Note=note};
        }
        public static ButtonAdd_M ToViewModel(string menuid)
        {
            return new ButtonAdd_M() { menuid = menuid };
        }
        [StringLength(60)]
        [Display(Name = "按钮编号")]
        public string buttonid { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "按钮名称")]
        public string name { get; set; }
        [Display(Name = "所属菜单编号")]
        [Required]
        [StringLength(100)]
        public string menuid{ get; set; }
        [Required]
        [StringLength(40)]
        [Display(Name = "按钮值")]
        public string value { get; set; }
        [StringLength(10)]
        [Display(Name = "备注")]
        public string note { get; set; }
    }
}