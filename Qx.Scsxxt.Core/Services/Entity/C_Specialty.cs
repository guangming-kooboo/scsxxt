namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_Specialty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_Specialty()
        {
            T_Class = new HashSet<T_Class>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EntryYear { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string SpeNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string SchoolID { get; set; }

        [StringLength(100)]
        public string SpeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Class> T_Class { get; set; }
    }
}
