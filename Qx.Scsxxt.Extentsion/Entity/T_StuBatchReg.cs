namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_StuBatchReg
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_StuBatchReg()
        {
            T_EntEvaluateStu = new HashSet<T_EntEvaluateStu>();
            T_EntStudentReviewLink = new HashSet<T_EntStudentReviewLink>();
            T_PracticeAttendance = new HashSet<T_PracticeAttendance>();
            T_PracticeCase = new HashSet<T_PracticeCase>();
            T_PracticeIdentification = new HashSet<T_PracticeIdentification>();
            T_SchoolStudentReviewLink = new HashSet<T_SchoolStudentReviewLink>();
            T_SelfEvaluation = new HashSet<T_SelfEvaluation>();
            T_StuEvaluateEnt = new HashSet<T_StuEvaluateEnt>();
            T_StuEvaluateSchool = new HashSet<T_StuEvaluateSchool>();
            T_WeekRecord = new HashSet<T_WeekRecord>();
        }

        [Key]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Required]
        [StringLength(40)]
        public string PracBatchID { get; set; }

        public double? WeekRecordScore { get; set; }

        public double? PracticeCaseScore { get; set; }

        [StringLength(1024)]
        public string WeekRecordComment { get; set; }

        [StringLength(1024)]
        public string PracticeCaseComment { get; set; }

        [Column(TypeName = "text")]
        public string PracticeContent { get; set; }

        [Column(TypeName = "text")]
        public string PracticeReport { get; set; }

        [StringLength(200)]
        public string PracticeReportFile { get; set; }

        [StringLength(50)]
        public string PracticeDept { get; set; }

        [StringLength(50)]
        public string PracticeDivDept { get; set; }

        [StringLength(50)]
        public string PracticeStartEnd { get; set; }

        public double? ReviewScore { get; set; }

        [StringLength(1024)]
        public string PracUnitComment { get; set; }

        [StringLength(1024)]
        public string SchoolComment { get; set; }

        [StringLength(1024)]
        public string TutorComment { get; set; }

        public int? CareerStatusID { get; set; }

        public double? StuEvaEntScore { get; set; }

        public double? StuEvaSchoolScore { get; set; }

        public virtual C_CareerStatus C_CareerStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntEvaluateStu> T_EntEvaluateStu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntStudentReviewLink> T_EntStudentReviewLink { get; set; }

        public virtual T_PracBatch T_PracBatch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_PracticeAttendance> T_PracticeAttendance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_PracticeCase> T_PracticeCase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_PracticeIdentification> T_PracticeIdentification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SchoolStudentReviewLink> T_SchoolStudentReviewLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SelfEvaluation> T_SelfEvaluation { get; set; }

        public virtual T_StuBatchReg_Extent T_StuBatchReg_Extent { get; set; }

        public virtual T_Student T_Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_StuEvaluateEnt> T_StuEvaluateEnt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_StuEvaluateSchool> T_StuEvaluateSchool { get; set; }

        public virtual T_StuScoreApply T_StuScoreApply { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_WeekRecord> T_WeekRecord { get; set; }
    }
}
