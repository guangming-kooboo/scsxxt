namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_T_AttachQuestionToQuestionnaire
    {
        [Key]
        [StringLength(70)]
        public string AttachID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionnaireID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionID { get; set; }

        public int QuestionSequenceID { get; set; }

        public virtual CPQ_T_Question CPQ_T_Question { get; set; }

        public virtual CPQ_T_Questionnaire CPQ_T_Questionnaire { get; set; }
    }
}
