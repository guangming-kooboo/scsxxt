namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_PracticeIdentification
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ItemNo { get; set; }

        [Required]
        [StringLength(8000)]
        public string ItemContent { get; set; }

        public virtual C_PracticeIdentificationItem C_PracticeIdentificationItem { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }
    }
}
