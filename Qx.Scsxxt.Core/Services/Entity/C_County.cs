namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_County
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_County()
        {
            T_Enterprise = new HashSet<T_Enterprise>();
        }

        [StringLength(6)]
        public string CityID { get; set; }

        [Key]
        [StringLength(6)]
        public string CountyID { get; set; }

        [StringLength(50)]
        public string CountyName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Enterprise> T_Enterprise { get; set; }
    }
}
