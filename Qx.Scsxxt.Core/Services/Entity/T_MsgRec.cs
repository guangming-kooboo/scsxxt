namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_MsgRec
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string MsgID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Receiver { get; set; }

        public int ReadTime { get; set; }

        public virtual T_SysMsg T_SysMsg { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
