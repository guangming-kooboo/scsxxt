namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_EntReviewerTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_EntReviewerTask()
        {
            T_EntStudentReviewLink = new HashSet<T_EntStudentReviewLink>();
        }

        [Key]
        [StringLength(60)]
        public string TaskID { get; set; }

        [Required]
        [StringLength(60)]
        public string ItemID { get; set; }

        [Required]
        [StringLength(20)]
        public string EmployeeID { get; set; }

        public virtual T_Employee T_Employee { get; set; }

        public virtual T_EntReviewItem T_EntReviewItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntStudentReviewLink> T_EntStudentReviewLink { get; set; }
    }
}
