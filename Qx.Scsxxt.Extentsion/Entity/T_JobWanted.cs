namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_JobWanted
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Ent_No { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(6)]
        public string PositionID { get; set; }

        [StringLength(200)]
        public string ResumeURL { get; set; }

        public int? ReviewTime { get; set; }

        [StringLength(8000)]
        public string ReviewRecord { get; set; }

        public int? ReviewScore { get; set; }

        public int? JobStatus { get; set; }

        [StringLength(10)]
        public string Flag { get; set; }
    }
}
