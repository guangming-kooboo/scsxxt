using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.CRUD
{
    public class ColumnEdit_M
    {
        public static ColumnEdit_M ToViewModel(Column column)
        {
            return new ColumnEdit_M() { forumid = column.ForumID,columnid=column.ID,columnname=column.ColumnName,columnexplain=column.ColumnExplain };
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