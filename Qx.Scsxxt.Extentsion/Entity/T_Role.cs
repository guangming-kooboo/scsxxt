namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Role()
        {
            T_RoleFuncObjForbid = new HashSet<T_RoleFuncObjForbid>();
            T_RoleModule = new HashSet<T_RoleModule>();
            T_UserRole = new HashSet<T_UserRole>();
        }

        [Key]
        [StringLength(6)]
        public string RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(10)]
        public string RoleType { get; set; }

        [StringLength(20)]
        public string subSystem { get; set; }

        [StringLength(10)]
        public string IsDefault { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_RoleFuncObjForbid> T_RoleFuncObjForbid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_RoleModule> T_RoleModule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_UserRole> T_UserRole { get; set; }
    }
}
