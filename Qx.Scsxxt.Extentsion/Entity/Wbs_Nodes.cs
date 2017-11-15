namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wbs_Nodes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wbs_Nodes()
        {
            Wbs_UserNodes = new HashSet<Wbs_UserNodes>();
        }

        [Key]
        [StringLength(30)]
        public string NodeID { get; set; }

        [StringLength(30)]
        public string NodeName { get; set; }

        [StringLength(30)]
        public string FatherNodeID { get; set; }

        [StringLength(200)]
        public string Decription { get; set; }

        public double? NodeWeight { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BeginTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndTime { get; set; }

        [StringLength(30)]
        public string Place { get; set; }

        public int? NodeSequence { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wbs_UserNodes> Wbs_UserNodes { get; set; }
    }
}
