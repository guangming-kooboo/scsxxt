namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_UserRole
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string RoleID { get; set; }

        [StringLength(10)]
        public string Note { get; set; }

        public virtual T_Role T_Role { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
