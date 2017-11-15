namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_School
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_School()
        {
            T_Faculty = new HashSet<T_Faculty>();
            T_PracBatch = new HashSet<T_PracBatch>();
        }

        [Key]
        [StringLength(20)]
        public string SchoolID { get; set; }

        [StringLength(100)]
        public string SchoolName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Contact { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        public int Status { get; set; }

        public int InfoLocked { get; set; }

        [StringLength(100)]
        public string SchoolLogo { get; set; }

        public virtual C_UnitStatus C_UnitStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Faculty> T_Faculty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_PracBatch> T_PracBatch { get; set; }
    }
}
