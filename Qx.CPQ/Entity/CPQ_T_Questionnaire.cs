namespace Qx.CPQ.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_T_Questionnaire
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPQ_T_Questionnaire()
        {
            CPQ_T_AnswerSheet = new HashSet<CPQ_T_AnswerSheet>();
            CPQ_T_AttachQuestionToQuestionnaire = new HashSet<CPQ_T_AttachQuestionToQuestionnaire>();
        }

        [Key]
        [StringLength(50)]
        public string QuestionnaireID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionnaireName { get; set; }

        [Required]
        [StringLength(255)]
        public string Summarize { get; set; }

        public int QuestionnaireDomain { get; set; }

        public int CreateTime { get; set; }

        [Required]
        [StringLength(20)]
        public string OwnerID { get; set; }

        public int Status { get; set; }

        [StringLength(50)]
        public string OwnerCompany { get; set; }

        public int? share { get; set; }

        public int? Reference { get; set; }

        public int? CollectSeting { get; set; }

        public int? CollectNumber { get; set; }

        public int? IsLock { get; set; }

        public virtual CPQ_C_QuestionnaireDomain CPQ_C_QuestionnaireDomain { get; set; }

        public virtual CPQ_C_QuestionnaireStatus CPQ_C_QuestionnaireStatus { get; set; }

        public virtual CPQ_C_Share CPQ_C_Share { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_AnswerSheet> CPQ_T_AnswerSheet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_AttachQuestionToQuestionnaire> CPQ_T_AttachQuestionToQuestionnaire { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
