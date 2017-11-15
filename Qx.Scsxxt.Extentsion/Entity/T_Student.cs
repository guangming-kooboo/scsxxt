namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Student()
        {
            T_StuBatchReg = new HashSet<T_StuBatchReg>();
        }

        [Required]
        [StringLength(15)]
        public string StuNo { get; set; }

        [Required]
        [StringLength(100)]
        public string StuName { get; set; }

        [StringLength(2)]
        public string StuSex { get; set; }

        public int? StuHeight { get; set; }

        public int? StuWeight { get; set; }

        [StringLength(20)]
        public string StuCellphone { get; set; }

        [StringLength(100)]
        public string StuEMail { get; set; }

        [StringLength(20)]
        public string StuQQ { get; set; }

        [Column(TypeName = "text")]
        public string StuResume { get; set; }

        [StringLength(100)]
        public string MainPhoto { get; set; }

        [Column(TypeName = "text")]
        public string Pics { get; set; }

        [StringLength(500)]
        public string Videos { get; set; }

        [Required]
        [StringLength(50)]
        public string StuClass { get; set; }

        [Key]
        [StringLength(20)]
        public string UserID { get; set; }

        [Column(TypeName = "text")]
        public string StuResourceFile { get; set; }

        [Column(TypeName = "text")]
        public string PicMood { get; set; }

        [Column(TypeName = "text")]
        public string VideoMood { get; set; }

        [StringLength(200)]
        public string StuBirthday { get; set; }

        public virtual T_Class T_Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_StuBatchReg> T_StuBatchReg { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
