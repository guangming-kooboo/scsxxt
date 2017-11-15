namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_StuScoreApply
    {
        [Key]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [StringLength(50)]
        public string UserID { get; set; }

        [Column(TypeName = "text")]
        public string EvidenceFile { get; set; }

        public double? ApplyReviewScore { get; set; }

        [Column(TypeName = "text")]
        public string ApplyContents { get; set; }

        [StringLength(50)]
        public string StateID { get; set; }

        [Column(TypeName = "text")]
        public string PracticeCaseComment { get; set; }

        public double? WeekRecordScore { get; set; }

        public double? PracticeCaseScore { get; set; }

        [Column(TypeName = "text")]
        public string WeekRecordComment { get; set; }

        [Column(TypeName = "text")]
        public string PracticeContent { get; set; }

        [Column(TypeName = "text")]
        public string PracticeReport { get; set; }

        [Column(TypeName = "text")]
        public string PracticeReportFile { get; set; }

        [StringLength(50)]
        public string PracticeStartEnd { get; set; }

        [Column(TypeName = "text")]
        public string PracUnitComment { get; set; }

        [Column(TypeName = "text")]
        public string SchoolComment { get; set; }

        [Column(TypeName = "text")]
        public string TutorComment { get; set; }

        public double? StuEvaEntScore { get; set; }

        public double? StuEvaSchoolScore { get; set; }

        public double? ReviewScore { get; set; }

        [Column(TypeName = "text")]
        public string AuditOpinion { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }
    }
}
