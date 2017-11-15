using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    [Table("C_ContentType")]
    public class C_ContentType
    {
        [Key]
        [Required]
        [Display(Name = "内容种类编号")]
        public int ConTypeID { get; set; }

        [Required]
        [Display(Name = "内容种类名称")]
        public string ConTypeName { get; set; }
    }
}