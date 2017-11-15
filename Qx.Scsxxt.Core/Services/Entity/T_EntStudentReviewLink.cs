namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_EntStudentReviewLink
    {
        [Key]
        [StringLength(60)]
        public string LinkID { get; set; }

        [Required]
        [StringLength(60)]
        public string TaskID { get; set; }

        [Required]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        public double? ReviewScore { get; set; }

        [Column(TypeName = "text")]
        public string ReviewComment { get; set; }

        public virtual T_EntReviewerTask T_EntReviewerTask { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }
    }
}
