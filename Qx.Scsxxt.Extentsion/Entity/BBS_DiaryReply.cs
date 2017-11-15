namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_DiaryReply
    {
        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Time { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Contents { get; set; }

        [Required]
        [StringLength(10)]
        public string DiaryID { get; set; }

        [Key]
        [StringLength(20)]
        public string DiaryReplyID { get; set; }

        public virtual BBS_Diary BBS_Diary { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
