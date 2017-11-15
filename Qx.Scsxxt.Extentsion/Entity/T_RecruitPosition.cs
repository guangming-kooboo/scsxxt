namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_RecruitPosition
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string Ent_No { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string PositionID { get; set; }

        public int PubDate { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string PosDesc { get; set; }

        public int PosQuantity { get; set; }

        public int PosExpireDate { get; set; }

        [Required]
        [StringLength(8000)]
        public string PosRequirement { get; set; }

        [Required]
        [StringLength(8000)]
        public string PosPay { get; set; }

        [StringLength(50)]
        public string Publisher { get; set; }

        public int IsActive { get; set; }
    }
}
