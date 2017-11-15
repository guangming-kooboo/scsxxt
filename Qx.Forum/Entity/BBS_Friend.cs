namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Friend
    {
        [Key]
        [StringLength(50)]
        public string FriendID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserIDA { get; set; }

        [Required]
        [StringLength(20)]
        public string UserIDB { get; set; }

        [StringLength(20)]
        public string StatusID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Time { get; set; }

        public virtual BBS_C_Friend BBS_C_Friend { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }

        public virtual BBS_Users BBS_Users1 { get; set; }
    }
}
