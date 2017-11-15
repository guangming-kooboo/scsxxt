namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_ReplyParise
    {
        [Key]
        [StringLength(50)]
        public string PraiseID { get; set; }

        [Required]
        [StringLength(20)]
        public string ReplyPostID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        public virtual BBS_ReplyPost BBS_ReplyPost { get; set; }
    }
}
