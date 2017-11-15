using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.BBS.ViewModels.CRUD
{
    public class AddTheme_M
    {
        public static AddTheme_M ToViewModel(string id)
        {
            return new AddTheme_M() { columnid = id };
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