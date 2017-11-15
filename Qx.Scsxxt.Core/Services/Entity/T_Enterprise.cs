namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Enterprise
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Enterprise()
        {
            T_AnswerQuestion = new HashSet<T_AnswerQuestion>();
            T_Employee = new HashSet<T_Employee>();
            T_EntBatchReg = new HashSet<T_EntBatchReg>();
            V3_StuEnterPriseApply = new HashSet<V3_StuEnterPriseApply>();
        }

        [Key]
        [StringLength(20)]
        public string Ent_No { get; set; }

        [Required]
        [StringLength(1024)]
        public string Ent_Name { get; set; }

        [StringLength(6)]
        public string CountyID { get; set; }

        [Required]
        [StringLength(6)]
        public string EntCategoryID { get; set; }

        [Required]
        [StringLength(6)]
        public string EntRank { get; set; }

        [StringLength(1024)]
        public string EntAddress { get; set; }

        [Column(TypeName = "text")]
        public string EntResume { get; set; }

        [StringLength(100)]
        public string EntLogo { get; set; }

        [Column(TypeName = "text")]
        public string EntAdPics { get; set; }

        [Column(TypeName = "text")]
        public string EntPhotos { get; set; }

        [Column(TypeName = "text")]
        public string EntVideos { get; set; }

        [Column(TypeName = "text")]
        public string EntPPTs { get; set; }

        [Column(TypeName = "text")]
        public string EntFiles { get; set; }

        public int EntState { get; set; }

        public int? RegisterTime { get; set; }

        public int? UpdateTime { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(1024)]
        public string Contectinfo { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        public int InfoLocked { get; set; }

        public virtual C_County C_County { get; set; }

        public virtual C_EntRank C_EntRank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_AnswerQuestion> T_AnswerQuestion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Employee> T_Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EntBatchReg> T_EntBatchReg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<V3_StuEnterPriseApply> V3_StuEnterPriseApply { get; set; }
    }
}
