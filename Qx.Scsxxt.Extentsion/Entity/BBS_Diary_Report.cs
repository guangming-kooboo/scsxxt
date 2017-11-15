namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Diary_Report
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_Diary_Report()
        {
            BBS_Diary = new HashSet<BBS_Diary>();
        }

        [Key]
        [StringLength(10)]
        public string ReportID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Required]
        [StringLength(10)]
        public string DiaryID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Diary> BBS_Diary { get; set; }

        public virtual BBS_Diary BBS_Diary1 { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
