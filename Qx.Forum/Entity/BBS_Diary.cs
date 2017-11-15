namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Diary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_Diary()
        {
            BBS_Diary_Report1 = new HashSet<BBS_Diary_Report>();
            BBS_DiaryReply = new HashSet<BBS_DiaryReply>();
        }

        [Required]
        [StringLength(50)]
        public string DiaryTitle { get; set; }

        [Column(TypeName = "text")]
        public string Contents { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Time { get; set; }

        [Key]
        [StringLength(10)]
        public string DiaryID { get; set; }

        [StringLength(10)]
        public string StateID { get; set; }

        [StringLength(10)]
        public string ReportID { get; set; }

        public virtual BBS_C_DiaryState BBS_C_DiaryState { get; set; }

        public virtual BBS_Diary_Report BBS_Diary_Report { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Diary_Report> BBS_Diary_Report1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_DiaryReply> BBS_DiaryReply { get; set; }
    }
}
