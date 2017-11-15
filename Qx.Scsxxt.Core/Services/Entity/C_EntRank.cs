namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_EntRank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_EntRank()
        {
            T_Enterprise = new HashSet<T_Enterprise>();
            V3_EnterpriseApply = new HashSet<V3_EnterpriseApply>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(6)]
        public string EntRankID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string EntCategoryID { get; set; }

        [Required]
        [StringLength(20)]
        public string EntRankName { get; set; }

        public virtual C_EntCategory C_EntCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Enterprise> T_Enterprise { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<V3_EnterpriseApply> V3_EnterpriseApply { get; set; }
    }
}
