using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.BBS.ViewModels.CRUD
{
    public class AddColumn_M
    {
        public static AddColumn_M ToViewModel(string id)
        {
            return new AddColumn_M() { forumid = id };
        }
        [Display(Name = "子板块ID")]
        public string columnid { get; set; }
        [Display(Name = "父板块ID")]
        public string forumid { get; set; }
        [Display(Name = "子板块名称")]
        public string columnname { get; set; }
        [Display(Name = "子板块介绍")]
        public string columnexplain { get; set; }
    }
}