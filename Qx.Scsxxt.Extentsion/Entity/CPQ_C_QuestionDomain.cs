namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_C_QuestionDomain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPQ_C_QuestionDomain()
        {
            CPQ_T_Question = new HashSet<CPQ_T_Question>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionDomainID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionDomainName { get; set; }

        [Required]
        [StringLength(255)]
        public string QuestionDomainDescribe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_Question> CPQ_T_Question { get; set; }
    }
}
