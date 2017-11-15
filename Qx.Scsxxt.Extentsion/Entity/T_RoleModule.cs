namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_RoleModule
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(6)]
        public string RoleID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ModuleID { get; set; }

        public int IncludeAllSubMod { get; set; }

        [StringLength(100)]
        public string DataDomain { get; set; }

        public virtual T_Module T_Module { get; set; }

        public virtual T_Role T_Role { get; set; }
    }
}
