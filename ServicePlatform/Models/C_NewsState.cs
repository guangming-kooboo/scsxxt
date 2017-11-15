using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    [Table("C_NewsState")]
    public class C_NewsState
    {
        [Key]
        public int NewsStateID { get; set; }
        public string NewsStateName { get; set; }
    }
}