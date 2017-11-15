namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_PracticeAttendance
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ItemNo { get; set; }

        public int? ItemContent { get; set; }

        [StringLength(50)]
        public string ItemGrade { get; set; }

        public virtual C_PracAttendanceGradeItem C_PracAttendanceGradeItem { get; set; }

        public virtual C_PracAttendanceItem C_PracAttendanceItem { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }
    }
}
