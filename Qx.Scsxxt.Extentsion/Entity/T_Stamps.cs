namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Stamps
    {
        [Key]
        [StringLength(50)]
        public string StampsID { get; set; }

        public int StampsTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string StampsUrl { get; set; }

        public int? InnerID { get; set; }

        public virtual C_StampType C_StampType { get; set; }

        public virtual T_Content T_Content { get; set; }
    }
}
