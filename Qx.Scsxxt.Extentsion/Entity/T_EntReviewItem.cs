namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_EntReviewItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_EntReviewItem()
        {
            T_EntReviewerTask = new HashSet<T_EntReviewerTask>();
        }

        [Key]
        [StringLength(60)]
        public string ItemID { get; set; }

        [Required]
        [StringLength(100)]
        public string EntPracNo { get; set; }

        [StringLength(1024)]
        public string ItemName { get; set; }

        public int? ItemWeight { get; set; }

        public int? ItemSequence { get; set; }

        public virtual T_EntBatchReg T_EntBatchReg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntReviewerTask> T_EntReviewerTask { get; set; }
    }
}
