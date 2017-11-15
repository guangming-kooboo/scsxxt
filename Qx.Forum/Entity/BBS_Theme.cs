namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Theme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_Theme()
        {
            BBS_Post = new HashSet<BBS_Post>();
        }

        [Key]
        [StringLength(20)]
        public string ThemeID { get; set; }

        [Required]
        [StringLength(20)]
        public string ThemeName { get; set; }

        [Required]
        [StringLength(50)]
        public string ColumnID { get; set; }

        [StringLength(50)]
        public string ThemeExplain { get; set; }

        public virtual BBS_Columns BBS_Columns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Post> BBS_Post { get; set; }
    }
}
