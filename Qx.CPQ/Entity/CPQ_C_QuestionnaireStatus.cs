namespace Qx.CPQ.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_C_QuestionnaireStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPQ_C_QuestionnaireStatus()
        {
            CPQ_T_Questionnaire = new HashSet<CPQ_T_Questionnaire>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireStatusID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionnaireStatusName { get; set; }

        [StringLength(50)]
        public string Decription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_Questionnaire> CPQ_T_Questionnaire { get; set; }
    }
}
