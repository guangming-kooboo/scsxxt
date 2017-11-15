using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Entity;
using Qx.Tools.CommonExtendMethods;
using System.ComponentModel.DataAnnotations;

namespace ServicePlatform.Areas.BBS.ViewModels.CRUD
{
    public class AddForum_M
    {
        [Display(Name = "板块ID")]
        public string forumid { get; set; }
        [Display(Name = "板块名称")]
        public string forumname { get; set; }
        [Display(Name = "板块介绍")]
        public string forumexplain { get; set; }
    }
}