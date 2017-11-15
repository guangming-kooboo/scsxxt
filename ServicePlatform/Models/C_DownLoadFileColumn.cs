using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    public class C_DownLoadFileColumn
    {
        [Key]
        [Required]
        [Display(Name = "资源栏目编号")]
        public int DownLoadFileColumnID { get; set; }

        [Display(Name = "资源栏目名称")]
        public string DownLoadFileColumnName { get; set; }

        [Display(Name = "单位编号")]
        public string SybSystem { get; set; }
    }
}