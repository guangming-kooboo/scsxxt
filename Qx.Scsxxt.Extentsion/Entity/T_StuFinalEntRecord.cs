namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_StuFinalEntRecord
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string Ent_Name { get; set; }

        [StringLength(50)]
        public string EntCategory { get; set; }

        [StringLength(50)]
        public string EntRank { get; set; }

        [Required]
        [StringLength(500)]
        public string EntAddress { get; set; }

        [StringLength(2000)]
        public string EntResume { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string Contectinfo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string PracticeNo { get; set; }
    }
}
