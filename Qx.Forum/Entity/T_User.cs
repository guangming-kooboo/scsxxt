namespace Qx.Community.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_User
    {
        [Key]
        [StringLength(20)]
        public string UserID { get; set; }

        [StringLength(100)]
        public string NickName { get; set; }

        [Required]
        [StringLength(100)]
        public string UserPwd { get; set; }

        public int UserType { get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
