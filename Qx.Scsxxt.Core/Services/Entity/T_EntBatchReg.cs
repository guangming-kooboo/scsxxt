namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_EntBatchReg
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_EntBatchReg()
        {
            T_EntReviewItem = new HashSet<T_EntReviewItem>();
            T_PracticePosition = new HashSet<T_PracticePosition>();
        }

        [Key]
        [StringLength(100)]
        public string EntPracNo { get; set; }

        [Required]
        [StringLength(40)]
        public string PracBatchID { get; set; }

        [Required]
        [StringLength(20)]
        public string Ent_No { get; set; }

        public int? RegistTime { get; set; }

        public int ApplyStatus { get; set; }

        public virtual C_ApplyStatus C_ApplyStatus { get; set; }

        public virtual C_UnitStatus C_UnitStatus { get; set; }

        public virtual T_Enterprise T_Enterprise { get; set; }

        public virtual T_PracBatch T_PracBatch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntReviewItem> T_EntReviewItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_PracticePosition> T_PracticePosition { get; set; }
    }
}
