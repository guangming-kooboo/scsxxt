namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Employee()
        {
            T_EntReviewerTask = new HashSet<T_EntReviewerTask>();
        }

        [Required]
        [StringLength(20)]
        public string Ent_No { get; set; }

        [Required]
        [StringLength(20)]
        public string EmployeeID { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [Key]
        [StringLength(20)]
        public string UserID { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string QQ { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public virtual T_Enterprise T_Enterprise { get; set; }

        public virtual T_User T_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntReviewerTask> T_EntReviewerTask { get; set; }
    }
}
