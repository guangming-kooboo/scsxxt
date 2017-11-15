namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_RoleFuncObjForbid
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(6)]
        public string RoleID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(60)]
        public string FuncObjID { get; set; }

        [StringLength(10)]
        public string Note { get; set; }

        public virtual T_FuncObject T_FuncObject { get; set; }

        public virtual T_Role T_Role { get; set; }
    }
}
