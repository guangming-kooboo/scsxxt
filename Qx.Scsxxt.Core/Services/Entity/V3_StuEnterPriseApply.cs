namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V3_StuEnterPriseApply
    {
        [Key]
        [StringLength(50)]
        public string StuEnterPriseApplyID { get; set; }

        [StringLength(20)]
        public string Ent_No { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        public int? ApplyState { get; set; }

        public int? ApplyTime { get; set; }

        [StringLength(8000)]
        public string posDesc { get; set; }

        [StringLength(50)]
        public string practiceDept { get; set; }

        [StringLength(50)]
        public string practiceDivDept { get; set; }

        public int? practiceStart { get; set; }

        public int? practiceEnd { get; set; }

        [StringLength(6)]
        public string positionId { get; set; }

        [StringLength(40)]
        public string PracBatchID { get; set; }

        public virtual T_Enterprise T_Enterprise { get; set; }

        public virtual T_PracBatch T_PracBatch { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
