namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Share
    {
        [Key]
        [StringLength(50)]
        public string ShareID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Time { get; set; }

        [Required]
        [StringLength(20)]
        public string PostID { get; set; }

        [StringLength(20)]
        public string StatusID { get; set; }

        public virtual BBS_C_Share BBS_C_Share { get; set; }

        public virtual BBS_Post BBS_Post { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
