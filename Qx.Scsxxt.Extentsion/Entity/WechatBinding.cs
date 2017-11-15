namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WechatBinding")]
    public partial class WechatBinding
    {
        [Key]
        [StringLength(50)]
        public string OpenID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        public int? RegisterTime { get; set; }

        public int? RelieveTime { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
