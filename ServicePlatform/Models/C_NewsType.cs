using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    [Table("C_NewsType")]
    public class C_NewsType
    {
        [Key]
        public int TypeID { get; set; }
        public string TypeName { get; set; }
    }
}