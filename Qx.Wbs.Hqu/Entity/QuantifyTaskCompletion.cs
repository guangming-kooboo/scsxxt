namespace Qx.Wbs.Hqu.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuantifyTaskCompletion
    {
        [StringLength(50)]
        public string QuantifyTaskCompletionID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuantifyTaskID { get; set; }

        [StringLength(50)]
        public string ScoringRuleID { get; set; }

        [Required]
        [StringLength(50)]
        public string StaffID { get; set; }

        [Required]
        [StringLength(50)]
        public string StaffName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? FinishTime { get; set; }

        [StringLength(500)]
        public string CompleteNote { get; set; }

        [StringLength(500)]
        public string Certificate { get; set; }

        public virtual ScoringRule ScoringRule { get; set; }

        public virtual QuantifyTask QuantifyTask { get; set; }
    }
}
