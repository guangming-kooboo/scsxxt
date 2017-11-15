namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_T_Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPQ_T_Question()
        {
            CPQ_T_AnswerSheet = new HashSet<CPQ_T_AnswerSheet>();
            CPQ_T_AttachQuestionToQuestionnaire = new HashSet<CPQ_T_AttachQuestionToQuestionnaire>();
            CPQ_T_QuestionOption = new HashSet<CPQ_T_QuestionOption>();
        }

        [Key]
        [StringLength(50)]
        public string QuestionID { get; set; }

        public int QuestionType { get; set; }

        [Required]
        [StringLength(255)]
        public string QuestionName { get; set; }

        public int QuestionDomain { get; set; }

        public int? share { get; set; }

        public int? Reference { get; set; }

        public virtual CPQ_C_QuestionDomain CPQ_C_QuestionDomain { get; set; }

        public virtual CPQ_C_QuestionType CPQ_C_QuestionType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_AnswerSheet> CPQ_T_AnswerSheet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_AttachQuestionToQuestionnaire> CPQ_T_AttachQuestionToQuestionnaire { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_QuestionOption> CPQ_T_QuestionOption { get; set; }
    }
}
