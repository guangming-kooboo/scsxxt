namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_T_AnswerSheet
    {
        [Key]
        [StringLength(150)]
        public string AnswerSheetID { get; set; }

        [Required]
        [StringLength(50)]
        public string AnswererIP { get; set; }

        public int AnswerTime { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionnaireID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionID { get; set; }

        [Required]
        [StringLength(255)]
        public string Answer { get; set; }

        public virtual CPQ_T_Question CPQ_T_Question { get; set; }

        public virtual CPQ_T_Questionnaire CPQ_T_Questionnaire { get; set; }
    }
}
