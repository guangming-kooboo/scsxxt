namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_HomePageColumn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_HomePageColumn()
        {
            T_HomePageContent = new HashSet<T_HomePageContent>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HPColumnID { get; set; }

        [Required]
        [StringLength(100)]
        public string HPColumnName { get; set; }

        public int ColRowCount { get; set; }

        [Required]
        [StringLength(2)]
        public string OnTimeDESC { get; set; }

        [Required]
        [StringLength(10)]
        public string ContentType { get; set; }

        public int? ContentFrom { get; set; }

        [Required]
        [StringLength(20)]
        public string UnitID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_HomePageContent> T_HomePageContent { get; set; }
    }
}
