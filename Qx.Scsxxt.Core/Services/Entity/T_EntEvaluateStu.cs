namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_EntEvaluateStu
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

        [StringLength(50)]
        public string ItemGrade { get; set; }

        public virtual C_EntEvaluateStuItem C_EntEvaluateStuItem { get; set; }

        public virtual C_EntEvaStuGradeLevelItem C_EntEvaStuGradeLevelItem { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }
    }
}
