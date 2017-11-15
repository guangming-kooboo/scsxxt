namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_SchoolEvaStuGradeLevelItem
    {
        [Key]
        [StringLength(50)]
        public string ItemNo { get; set; }

        [Required]
        [StringLength(1024)]
        public string ItemName { get; set; }

        [StringLength(20)]
        public string Note { get; set; }

        public int? Weight { get; set; }

        [StringLength(40)]
        public string PracBatchID { get; set; }

        public int? ItemSequence { get; set; }
    }
}
