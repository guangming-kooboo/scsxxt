using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.CRUD
{
    public class ThemeEdit_M
    {
        public static ThemeEdit_M ToViewModel(Theme theme)
        {
            return new ThemeEdit_M() { columnid = theme.ColumnID, themeexplain = theme.ThemeExplain, themeid = theme.ID, themename = theme.ThemeName };
        }
        [Display(Name = "子板块ID")]
        public string columnid { get; set; }
        [Display(Name = "主题ID")]
        public string themeid { get; set; }
        [Display(Name = "主题名称")]
        public string themename { get; set; }
        [Display(Name = "主题介绍")]
        public string themeexplain { get; set; }
    }
}