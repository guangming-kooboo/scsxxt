namespace Qx.Wbs.Hqu.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WbsTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WbsTask()
        {
            QuantifyTasks = new HashSet<QuantifyTask>();
            QuotaTasks = new HashSet<QuotaTask>();
        }

        [StringLength(50)]
        public string WbsTaskID { get; set; }

        [Required]
        [StringLength(50)]
        public string TaskName { get; set; }

        public double QuotaTaskWeight { get; set; }

        public double QuantifyTaskWeight { get; set; }

        public double MotorTaskWeight { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BeginTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndTime { get; set; }

        [Required]
        [StringLength(50)]
        public string OwnerID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuantifyTask> QuantifyTasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuotaTask> QuotaTasks { get; set; }
    }
}
