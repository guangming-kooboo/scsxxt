namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Post_Report
    {
        [Key]
        [StringLength(10)]
        public string ReportID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string PostID { get; set; }

        public virtual BBS_Post BBS_Post { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
