namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_FuncObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_FuncObject()
        {
            T_FuncBatchOpenSetting = new HashSet<T_FuncBatchOpenSetting>();
            T_RoleFuncObjForbid = new HashSet<T_RoleFuncObjForbid>();
        }

        [Key]
        [StringLength(60)]
        public string FuncObjID { get; set; }

        [Required]
        [StringLength(10)]
        public string ModuleID { get; set; }

        [StringLength(40)]
        public string FuncName { get; set; }

        [Required]
        [StringLength(40)]
        public string FuncID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_FuncBatchOpenSetting> T_FuncBatchOpenSetting { get; set; }

        public virtual T_Module T_Module { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_RoleFuncObjForbid> T_RoleFuncObjForbid { get; set; }
    }
}
