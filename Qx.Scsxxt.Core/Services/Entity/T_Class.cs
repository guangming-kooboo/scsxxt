namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Class()
        {
            T_Student = new HashSet<T_Student>();
        }

        [Key]
        [StringLength(50)]
        public string ClassID { get; set; }

        [StringLength(100)]
        public string ClassName { get; set; }

        [Required]
        [StringLength(20)]
        public string SpeNo { get; set; }

        public int EntryYear { get; set; }

        [Required]
        [StringLength(20)]
        public string SchoolID { get; set; }

        public virtual C_Specialty C_Specialty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Student> T_Student { get; set; }
    }
}
