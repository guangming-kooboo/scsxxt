namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Enterprise_ToCheck
    {
        [Key]
        [StringLength(20)]
        public string Ent_No { get; set; }

        public int EntState { get; set; }

        [Required]
        [StringLength(1024)]
        public string Ent_Name { get; set; }

        [Required]
        [StringLength(6)]
        public string EntCategoryID { get; set; }

        [Required]
        [StringLength(6)]
        public string EntRank { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }
    }
}
