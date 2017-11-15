using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qx.Permission.Scsxxt.Entity
{
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [StringLength(20)]
        public string UserID { get; set; }

        [StringLength(100)]
        public string NickName { get; set; }

        [Required]
        [StringLength(100)]
        public string UserPwd { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string UserTypeId { get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RegisterDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLoginDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
