namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_ReplyPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_ReplyPost()
        {
            BBS_Post = new HashSet<BBS_Post>();
            BBS_ReplyParise = new HashSet<BBS_ReplyParise>();
            BBS_ReplyReport = new HashSet<BBS_ReplyReport>();
        }

        [Key]
        [StringLength(20)]
        public string ReplyPostID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Time { get; set; }

        [Required]
        [StringLength(200)]
        public string Contents { get; set; }

        [Required]
        [StringLength(20)]
        public string PostID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [StringLength(20)]
        public string ParentReplyID { get; set; }

        [Column(TypeName = "text")]
        public string Files { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Post> BBS_Post { get; set; }

        public virtual BBS_Post BBS_Post1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_ReplyParise> BBS_ReplyParise { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_ReplyReport> BBS_ReplyReport { get; set; }
    }
}
