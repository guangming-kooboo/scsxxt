namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_StuEvaluateEnt
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ItemNo { get; set; }

        [StringLength(1024)]
        public string ItemContent { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemGrade { get; set; }

        public virtual C_StuEvaEntGradeLevelItem C_StuEvaEntGradeLevelItem { get; set; }

        public virtual C_StuEvaluateEntItem C_StuEvaluateEntItem { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }
    }
}