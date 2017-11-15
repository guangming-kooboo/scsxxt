namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_ForumManager
    {
        [Key]
        [StringLength(50)]
        public string ForumManagerID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string ForumID { get; set; }

        public virtual BBS_Columns BBS_Columns { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
