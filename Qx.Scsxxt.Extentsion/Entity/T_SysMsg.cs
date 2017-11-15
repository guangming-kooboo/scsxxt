namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_SysMsg
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_SysMsg()
        {
            T_MsgRec = new HashSet<T_MsgRec>();
            T_MsgSend = new HashSet<T_MsgSend>();
        }

        [Key]
        [StringLength(100)]
        public string MsgID { get; set; }

        [Required]
        [StringLength(20)]
        public string MsgOwner { get; set; }

        public int CreateTime { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string MsgContent { get; set; }

        public int MsgTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string FatherMsgID { get; set; }

        public int IsLocked { get; set; }

        public virtual C_MsgType C_MsgType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_MsgRec> T_MsgRec { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_MsgSend> T_MsgSend { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
