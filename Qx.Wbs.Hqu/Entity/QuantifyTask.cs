namespace Qx.Wbs.Hqu.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuantifyTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuantifyTask()
        {
            QuantifyTaskCompletions = new HashSet<QuantifyTaskCompletion>();
            ScoringRules = new HashSet<ScoringRule>();
        }

        [StringLength(50)]
        public string QuantifyTaskID { get; set; }

        [Required]
        [StringLength(50)]
        public string WbsTaskID { get; set; }

        [Required]
        [StringLength(50)]
        public string TaskName { get; set; }

        public double RelativeWeight { get; set; }

        public double? AbsoluteWeight { get; set; }

        [StringLength(500)]
        public string TaskContent { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BeginTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }

        public int TaskSequence { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuantifyTaskCompletion> QuantifyTaskCompletions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScoringRule> ScoringRules { get; set; }

        public virtual WbsTask WbsTask { get; set; }
    }
}
