namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Columns
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_Columns()
        {
            BBS_ForumManager = new HashSet<BBS_ForumManager>();
            BBS_Theme = new HashSet<BBS_Theme>();
        }

        [Key]
        [StringLength(50)]
        public string ColumnID { get; set; }

        [Required]
        [StringLength(20)]
        public string ColumnName { get; set; }

        [StringLength(20)]
        public string FatherColumnID { get; set; }

        [Required]
        [StringLength(50)]
        public string ForumID { get; set; }

        [StringLength(50)]
        public string ColumnImg { get; set; }

        [StringLength(200)]
        public string ColumnExplain { get; set; }

        public virtual BBS_Forum BBS_Forum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_ForumManager> BBS_ForumManager { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Theme> BBS_Theme { get; set; }
    }
}
