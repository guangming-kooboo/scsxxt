using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Models;
using System.ComponentModel.DataAnnotations;

namespace ServicePlatform.Areas.BBS.ViewModels.CRUD
{
    public class ForumEdit_M
    {
        public static ForumEdit_M ToViewModel(Forum forum)
        {
            return new ForumEdit_M() { forumid = forum.ID, forumexplain = forum.ForumExplain, forumname = forum.ForumName };
        }
        [Display(Name="板块名称")]
        public string forumname{get;set;}
         [Display(Name = "板块ID")]
        public string forumid { get; set; }
         [Display(Name = "板块介绍")]
        public string forumexplain { get; set; }
    }
}