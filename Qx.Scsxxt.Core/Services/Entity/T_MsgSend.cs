namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_MsgSend
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string MsgID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Sender { get; set; }

        public int SendTime { get; set; }

        public int SendTypeID { get; set; }

        [StringLength(50)]
        public string Receiver { get; set; }

        public virtual C_MsgSendType C_MsgSendType { get; set; }

        public virtual T_SysMsg T_SysMsg { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
