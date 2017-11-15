namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_ReplyReport
    {
        [Key]
        [StringLength(20)]
        public string ReportID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string ReplyPostID { get; set; }

        public virtual BBS_ReplyPost BBS_ReplyPost { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
