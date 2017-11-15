namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_SchoolReviewItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_SchoolReviewItem()
        {
            T_SchoolReviewerTask = new HashSet<T_SchoolReviewerTask>();
        }

        [Key]
        [StringLength(60)]
        public string ItemID { get; set; }

        [Required]
        [StringLength(40)]
        public string PracBatchID { get; set; }

        [Required]
        [StringLength(1024)]
        public string ItemName { get; set; }

        public int ItemWeight { get; set; }

        public int ItemSequence { get; set; }

        public virtual T_PracBatch T_PracBatch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SchoolReviewerTask> T_SchoolReviewerTask { get; set; }
    }
}
