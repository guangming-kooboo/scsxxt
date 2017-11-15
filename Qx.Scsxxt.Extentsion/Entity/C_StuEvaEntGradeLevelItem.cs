namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_StuEvaEntGradeLevelItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_StuEvaEntGradeLevelItem()
        {
            T_StuEvaluateEnt = new HashSet<T_StuEvaluateEnt>();
        }

        [Key]
        [StringLength(50)]
        public string ItemNo { get; set; }

        [Required]
        [StringLength(1024)]
        public string ItemName { get; set; }

        [StringLength(20)]
        public string Note { get; set; }

        public int? Weight { get; set; }

        [StringLength(40)]
        public string PracBatchID { get; set; }

        public int? ItemSequence { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_StuEvaluateEnt> T_StuEvaluateEnt { get; set; }
    }
}
