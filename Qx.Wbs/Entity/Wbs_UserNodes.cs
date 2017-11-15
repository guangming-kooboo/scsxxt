namespace Qx.Wbs.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wbs_UserNodes
    {
        [Key]
        [StringLength(50)]
        public string SerialID { get; set; }

        [StringLength(30)]
        public string NodeID { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        public double? UserWeight { get; set; }

        public double? RealWeight { get; set; }

        [StringLength(50)]
        public string Materal { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? FinishTime { get; set; }

        public virtual Wbs_Nodes Wbs_Nodes { get; set; }

        public virtual Wbs_Users Wbs_Users { get; set; }
    }
}
