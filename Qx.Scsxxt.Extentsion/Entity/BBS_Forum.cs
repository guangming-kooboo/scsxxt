namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Forum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_Forum()
        {
            BBS_Columns = new HashSet<BBS_Columns>();
        }

        [Key]
        [StringLength(50)]
        public string ForumID { get; set; }

        [StringLength(400)]
        public string ForumExplain { get; set; }

        [Required]
        [StringLength(20)]
        public string ForumName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Columns> BBS_Columns { get; set; }
    }
}
