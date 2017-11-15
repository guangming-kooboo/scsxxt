namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_PracticeCaseExtension
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CaseNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string ItemNo { get; set; }

        [StringLength(500)]
        public string Pic { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CaseTime { get; set; }

        public virtual T_PracticeCase T_PracticeCase { get; set; }
    }
}
