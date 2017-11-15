namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Faculty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Faculty()
        {
            T_SchoolReviewerTask = new HashSet<T_SchoolReviewerTask>();
        }

        [Required]
        [StringLength(20)]
        public string FacNo { get; set; }

        [Required]
        [StringLength(10)]
        public string FacName { get; set; }

        [StringLength(10)]
        public string FacSex { get; set; }

        public int? FacProPos { get; set; }

        [StringLength(20)]
        public string PhoneNo { get; set; }

        [StringLength(100)]
        public string EmailAddress { get; set; }

        [StringLength(100)]
        public string FacPhoto { get; set; }

        [StringLength(8000)]
        public string FacAbstract { get; set; }

        public int FacStatus { get; set; }

        [Required]
        [StringLength(20)]
        public string SchoolID { get; set; }

        [Key]
        [StringLength(20)]
        public string UserID { get; set; }

        public virtual C_ProPosition C_ProPosition { get; set; }

        public virtual T_School T_School { get; set; }

        public virtual T_User T_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SchoolReviewerTask> T_SchoolReviewerTask { get; set; }
    }
}
