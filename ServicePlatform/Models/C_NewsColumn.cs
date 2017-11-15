using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    [Table("C_NewsColumn")]
    public class C_NewsColumn
    {
        [Key]
        public int NewsColumnID { get; set; }
        public string NewsColumnName { get; set; }
        public string SybSystem { get; set; }
    }
}