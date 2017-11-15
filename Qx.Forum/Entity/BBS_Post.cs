namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_Post()
        {
            BBS_Post_Report = new HashSet<BBS_Post_Report>();
            BBS_ReplyPosts = new HashSet<BBS_ReplyPost>();
            BBS_Share = new HashSet<BBS_Share>();
            BBS_StepPraise = new HashSet<BBS_StepPraise>();
        }

        [Key]
        [StringLength(20)]
        public string PostID { get; set; }

        [Required]
        [StringLength(100)]
        public string PostTitle { get; set; }

        [Required]
        [StringLength(20)]
        public string ThemeID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string PostContent { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PostTime { get; set; }

        public int PClickCount { get; set; }

        [Required]
        [StringLength(10)]
        public string StateID { get; set; }

        [Required]
        [StringLength(10)]
        public string IsTop { get; set; }

        [Required]
        [StringLength(10)]
        public string IsCream { get; set; }

        [StringLength(10)]
        public string PostTypeID { get; set; }

        [StringLength(20)]
        public string BestReplyID { get; set; }

        [Column(TypeName = "text")]
        public string Files { get; set; }

        public virtual BBS_C_PostState BBS_C_PostState { get; set; }

        public virtual BBS_C_PostType BBS_C_PostType { get; set; }

        public virtual BBS_ReplyPost BBS_ReplyPost { get; set; }

        public virtual BBS_Theme BBS_Theme { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Post_Report> BBS_Post_Report { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_ReplyPost> BBS_ReplyPosts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Share> BBS_Share { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_StepPraise> BBS_StepPraise { get; set; }
    }
}
