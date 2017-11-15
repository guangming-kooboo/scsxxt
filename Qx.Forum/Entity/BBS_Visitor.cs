namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Visitor
    {
        [Required]
        [StringLength(20)]
        public string UserIDA { get; set; }

        [Required]
        [StringLength(20)]
        public string UserIDB { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Time { get; set; }

        [Key]
        [StringLength(50)]
        public string VisitorID { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }

        public virtual BBS_Users BBS_Users1 { get; set; }
    }
}
