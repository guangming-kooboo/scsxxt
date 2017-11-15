namespace Qx.CPQ.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_User()
        {
            CPQ_T_Questionnaire = new HashSet<CPQ_T_Questionnaire>();
        }

        [Key]
        [StringLength(20)]
        public string UserID { get; set; }

        [StringLength(100)]
        public string NickName { get; set; }

        [Required]
        [StringLength(100)]
        public string UserPwd { get; set; }

        public int UserType { get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CPQ_T_Questionnaire> CPQ_T_Questionnaire { get; set; }
    }
}
