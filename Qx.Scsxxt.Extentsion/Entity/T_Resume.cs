namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Resume
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ResumeName { get; set; }

        [Required]
        [StringLength(200)]
        public string ResumeURL { get; set; }

        public int? PubTime { get; set; }

        public int IsDefault { get; set; }
    }
}
