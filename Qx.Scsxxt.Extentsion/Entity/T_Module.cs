namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Module
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Module()
        {
            T_FuncObject = new HashSet<T_FuncObject>();
            T_RoleModule = new HashSet<T_RoleModule>();
        }

        [Key]
        [StringLength(10)]
        public string ModuleID { get; set; }

        [Required]
        [StringLength(100)]
        public string ModuleName { get; set; }

        [Required]
        [StringLength(10)]
        public string FartherModuleID { get; set; }

        [Required]
        [StringLength(1024)]
        public string PageName { get; set; }

        [Required]
        [StringLength(10)]
        public string ModuleLevel { get; set; }

        public int AvailableStatus { get; set; }

        public int? ModuleOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_FuncObject> T_FuncObject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_RoleModule> T_RoleModule { get; set; }
    }
}
