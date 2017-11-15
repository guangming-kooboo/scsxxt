namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_C_UserGrade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BBS_C_UserGrade()
        {
            BBS_Users = new HashSet<BBS_Users>();
        }

        [Key]
        [StringLength(10)]
        public string UserGradeID { get; set; }

        [StringLength(10)]
        public string UserGradeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BBS_Users> BBS_Users { get; set; }
    }
}
