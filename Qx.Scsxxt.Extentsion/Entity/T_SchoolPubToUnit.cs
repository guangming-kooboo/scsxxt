namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_SchoolPubToUnit
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string PracBatchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string UnitID { get; set; }

        public int? StartTime { get; set; }

        public int? EndTime { get; set; }

        public int ApplyStatusID { get; set; }

        public virtual C_ApplyStatus C_ApplyStatus { get; set; }
    }
}
