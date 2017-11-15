namespace Core.Services.Entity
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
            T_MsgRec = new HashSet<T_MsgRec>();
            T_MsgSend = new HashSet<T_MsgSend>();
            T_SysMsg = new HashSet<T_SysMsg>();
            T_UserRole = new HashSet<T_UserRole>();
            V3_EnterpriseApply = new HashSet<V3_EnterpriseApply>();
            V3_StuEnterPriseApply = new HashSet<V3_StuEnterPriseApply>();
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

        [StringLength(8000)]
        public string Note { get; set; }

        public virtual T_Employee T_Employee { get; set; }

        public virtual T_Faculty T_Faculty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_MsgRec> T_MsgRec { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_MsgSend> T_MsgSend { get; set; }

        public virtual T_Platformer T_Platformer { get; set; }

        public virtual T_Student T_Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SysMsg> T_SysMsg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_UserRole> T_UserRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<V3_EnterpriseApply> V3_EnterpriseApply { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<V3_StuEnterPriseApply> V3_StuEnterPriseApply { get; set; }
    }
}
