namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_SearchKeyWord
    {
        [Key]
        [StringLength(50)]
        public string KeyWord { get; set; }
    }
}