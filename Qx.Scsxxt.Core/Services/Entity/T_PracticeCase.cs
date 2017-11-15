namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_PracticeCase
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

        [Column(TypeName = "text")]
        public string ItemContent { get; set; }

        [StringLength(50)]
        public string CaseName { get; set; }

        public virtual C_PracticeCaseItem C_PracticeCaseItem { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }

        public virtual T_PracticeCaseExtension T_PracticeCaseExtension { get; set; }
    }
}
