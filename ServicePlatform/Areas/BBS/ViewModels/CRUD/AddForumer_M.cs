using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.CRUD
{
    public class AddForumer_M
    {
        public static AddForumer_M ToViewModel(Forum forum)
        {
            return new AddForumer_M() { forumid = forum.ID, forumname = forum.ForumName };
        }
        [Display(Name = "板块ID")]
        public string forumid { get; set; }
        [Display(Name = "板块名称")]
        public string forumname { get; set; }
        [Display(Name = "版主ID")]
        public string userid { get; set; }

    }
}