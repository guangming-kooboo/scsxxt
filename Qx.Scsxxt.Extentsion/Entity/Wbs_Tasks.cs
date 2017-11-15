namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wbs_Tasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wbs_Tasks()
        {
            Wbs_TaskNodes = new HashSet<Wbs_TaskNodes>();
        }

        [Key]
        [StringLength(30)]
        public string TaskId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BeginTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(200)]
        public string Decription { get; set; }

        [Required]
        [StringLength(50)]
        public string OwnerId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wbs_TaskNodes> Wbs_TaskNodes { get; set; }
    }
}
