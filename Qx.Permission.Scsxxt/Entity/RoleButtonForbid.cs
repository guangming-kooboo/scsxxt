using System;
using System.ComponentModel.DataAnnotations;

namespace Qx.Permission.Scsxxt.Entity
{
    public partial class RoleButtonForbid
    {
        [StringLength(80)]
        public string RoleButtonForbidID { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleID { get; set; }

        [Required]
        [StringLength(60)]
        public string ButtonID { get; set; }

        public DateTimeOffset? ExpireTime { get; set; }

        [StringLength(10)]
        public string Note { get; set; }

        public virtual Button Button { get; set; }

        public virtual Role Role { get; set; }
    }
}
