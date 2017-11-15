using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qx.Permission.Scsxxt.Entity
{
    public partial class UserRole
    {
        [StringLength(40)]
        public string UserRoleID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleID { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpireTime { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
