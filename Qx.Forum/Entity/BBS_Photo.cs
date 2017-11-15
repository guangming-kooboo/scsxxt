namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Photo
    {
        [StringLength(50)]
        public string Img { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Time { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        [Key]
        [StringLength(20)]
        public string PhtotID { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
