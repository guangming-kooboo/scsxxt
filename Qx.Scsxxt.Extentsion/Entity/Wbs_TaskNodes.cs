namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wbs_TaskNodes
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string TaskNodeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskNodeTypeId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string FatherId { get; set; }

        [StringLength(20)]
        public string ArrangedUserId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(30)]
        public string TaskId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsLeaf { get; set; }

        [Key]
        [Column(Order = 5)]
        public double Weight { get; set; }

        public double? RealPercent { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(30)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "datetime2")]
        public DateTime BeginTime { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "datetime2")]
        public DateTime EndTime { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(30)]
        public string Place { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(200)]
        public string Decription { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NodeSequence { get; set; }

        public virtual Wbs_Tasks Wbs_Tasks { get; set; }
    }
}
