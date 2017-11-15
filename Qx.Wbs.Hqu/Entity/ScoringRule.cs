namespace Qx.Wbs.Hqu.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScoringRule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScoringRule()
        {
            QuantifyTaskCompletions = new HashSet<QuantifyTaskCompletion>();
        }

        [StringLength(50)]
        public string ScoringRuleID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuantifyTaskID { get; set; }

        [Required]
        [StringLength(50)]
        public string FormName { get; set; }

        public double Score { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuantifyTaskCompletion> QuantifyTaskCompletions { get; set; }

        public virtual QuantifyTask QuantifyTask { get; set; }
    }
}
