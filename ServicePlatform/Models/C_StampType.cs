using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    public class C_StampType
    {
        [Key]
        [Required]
        [Display(Name = "印章种类编号")]
        public int TypeID { get; set; }

        [Display(Name = "印章种类名称")]
        public string TypeName { get; set; }

        [Display(Name = "单位编号")]
        public string SybSystem { get; set; }
    }
}