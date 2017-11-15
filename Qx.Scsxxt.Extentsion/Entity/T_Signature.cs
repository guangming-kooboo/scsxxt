namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Signature
    {
        [Key]
        [StringLength(50)]
        public string SignatureID { get; set; }

        [Required]
        [StringLength(100)]
        public string SignatureUrl { get; set; }

        public int? InnerID { get; set; }

        [StringLength(50)]
        public string UserID { get; set; }

        public int IsDefault { get; set; }

        public virtual T_Content T_Content { get; set; }
    }
}
