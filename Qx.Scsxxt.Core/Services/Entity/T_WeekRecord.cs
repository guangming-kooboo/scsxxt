namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_WeekRecord
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecordNo { get; set; }

        public int RecordDate { get; set; }

        [Required]
        [StringLength(1024)]
        public string RecordTitle { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string RecordContent { get; set; }

        [Column(TypeName = "text")]
        public string RecordComment { get; set; }

        public double? RecordScore { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }

        public virtual T_WeekRecordExtemsion T_WeekRecordExtemsion { get; set; }
    }
}
