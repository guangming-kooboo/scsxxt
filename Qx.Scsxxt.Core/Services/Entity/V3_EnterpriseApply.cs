namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V3_EnterpriseApply
    {
        [Key]
        [StringLength(20)]
        public string Ent_No { get; set; }

        [StringLength(1024)]
        public string Ent_Name { get; set; }

        [StringLength(6)]
        public string EntCategoryID { get; set; }

        [StringLength(6)]
        public string EntRankID { get; set; }

        [StringLength(1024)]
        public string EntAddress { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        [StringLength(10)]
        public string SchoolID { get; set; }

        public int? RegisterTime { get; set; }

        public int? UpdateTime { get; set; }

        public int? ApplyState { get; set; }

        [StringLength(1024)]
        public string Contectinfo { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(8000)]
        public string EntResume { get; set; }

        public virtual C_EntRank C_EntRank { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
