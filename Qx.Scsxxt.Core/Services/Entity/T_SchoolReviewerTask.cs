namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_SchoolReviewerTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_SchoolReviewerTask()
        {
            T_SchoolStudentReviewLink = new HashSet<T_SchoolStudentReviewLink>();
        }

        [Key]
        [StringLength(60)]
        public string TaskID { get; set; }

        [Required]
        [StringLength(20)]
        public string TeacherID { get; set; }

        [Required]
        [StringLength(60)]
        public string ItemID { get; set; }

        public virtual T_Faculty T_Faculty { get; set; }

        public virtual T_SchoolReviewItem T_SchoolReviewItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SchoolStudentReviewLink> T_SchoolStudentReviewLink { get; set; }
    }
}
