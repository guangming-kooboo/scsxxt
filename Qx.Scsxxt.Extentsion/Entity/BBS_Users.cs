namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_Users()
        {
            BBS_Diary = new HashSet<BBS_Diary>();
            BBS_Diary_Report = new HashSet<BBS_Diary_Report>();
            BBS_DiaryReply = new HashSet<BBS_DiaryReply>();
            BBS_ForumManager = new HashSet<BBS_ForumManager>();
            BBS_Friend = new HashSet<BBS_Friend>();
            BBS_Friend1 = new HashSet<BBS_Friend>();
            BBS_Note = new HashSet<BBS_Note>();
            BBS_Photo = new HashSet<BBS_Photo>();
            BBS_Post = new HashSet<BBS_Post>();
            BBS_Post_Report = new HashSet<BBS_Post_Report>();
            BBS_ReplyPost = new HashSet<BBS_ReplyPost>();
            BBS_ReplyReport = new HashSet<BBS_ReplyReport>();
            BBS_Share = new HashSet<BBS_Share>();
            BBS_StepPraise = new HashSet<BBS_StepPraise>();
            BBS_Visitor = new HashSet<BBS_Visitor>();
            BBS_Visitor1 = new HashSet<BBS_Visitor>();
        }

        [Key]
        [StringLength(20)]
        public string UserID { get; set; }

        [Required]
        [StringLength(10)]
        public string UserGradeID { get; set; }

        [Required]
        [StringLength(10)]
        public string UserStateID { get; set; }

        [Required]
        [StringLength(10)]
        public string UserPoint { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RegisterDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLogin { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RecentActivite { get; set; }

        [StringLength(50)]
        public string HeadImg { get; set; }

        public virtual BBS_C_UserGrade BBS_C_UserGrade { get; set; }

        public virtual BBS_C_UserState BBS_C_UserState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Diary> BBS_Diary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Diary_Report> BBS_Diary_Report { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_DiaryReply> BBS_DiaryReply { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_ForumManager> BBS_ForumManager { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Friend> BBS_Friend { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Friend> BBS_Friend1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Note> BBS_Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Photo> BBS_Photo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Post> BBS_Post { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Post_Report> BBS_Post_Report { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_ReplyPost> BBS_ReplyPost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_ReplyReport> BBS_ReplyReport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Share> BBS_Share { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_StepPraise> BBS_StepPraise { get; set; }

        public virtual T_User T_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Visitor> BBS_Visitor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Visitor> BBS_Visitor1 { get; set; }
    }
}
