namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_EntEvaluateStuItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_EntEvaluateStuItem()
        {
            T_EntEvaluateStu = new HashSet<T_EntEvaluateStu>();
        }

        [Key]
        [StringLength(50)]
        public string ItemNo { get; set; }

        [Required]
        [StringLength(40)]
        public string PracBatchID { get; set; }

        [Required]
        [StringLength(1024)]
        public string ItemName { get; set; }

        public int ItemSequence { get; set; }

        public int? ItemPower { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntEvaluateStu> T_EntEvaluateStu { get; set; }
    }
}
