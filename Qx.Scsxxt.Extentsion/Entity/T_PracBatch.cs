namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_PracBatch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_PracBatch()
        {
            T_EntBatchReg = new HashSet<T_EntBatchReg>();
            T_PlatformPubToUnit = new HashSet<T_PlatformPubToUnit>();
            T_SchoolReviewItem = new HashSet<T_SchoolReviewItem>();
            T_StuBatchReg = new HashSet<T_StuBatchReg>();
            V3_StuEnterPriseApply = new HashSet<V3_StuEnterPriseApply>();
        }

        [Key]
        [StringLength(40)]
        public string PracBatchID { get; set; }

        [Required]
        [StringLength(20)]
        public string BatchID { get; set; }

        [Required]
        [StringLength(20)]
        public string SchoolID { get; set; }

        [Required]
        [StringLength(100)]
        public string BatchName { get; set; }

        [Required]
        [StringLength(50)]
        public string StartEnd { get; set; }

        [Required]
        [StringLength(10)]
        public string IsCurrentBatch { get; set; }

        public int IsActive { get; set; }

        public int? SchoolReviewWeight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntBatchReg> T_EntBatchReg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_PlatformPubToUnit> T_PlatformPubToUnit { get; set; }

        public virtual T_School T_School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SchoolReviewItem> T_SchoolReviewItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_StuBatchReg> T_StuBatchReg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<V3_StuEnterPriseApply> V3_StuEnterPriseApply { get; set; }
    }
}
