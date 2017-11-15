using System;
using System.ComponentModel.DataAnnotations;

namespace Qx.Permission.Scsxxt.Entity
{
    public partial class RoleMenu
    {
        [StringLength(120)]
        public string RoleMenuID { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuID { get; set; }

        [StringLength(10)]
        public string Note { get; set; }

        public int IncludeChildren { get; set; }

        public DateTimeOffset? ExpireTime { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual Role Role { get; set; }
    }
}
