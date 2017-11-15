namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_C_QuestionnaireDomain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPQ_C_QuestionnaireDomain()
        {
            CPQ_T_Questionnaire = new HashSet<CPQ_T_Questionnaire>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionnaireDomainID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionnaireDomainName { get; set; }

        [StringLength(50)]
        public string QuestionnaireDomainDescribe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_Questionnaire> CPQ_T_Questionnaire { get; set; }
    }
}
