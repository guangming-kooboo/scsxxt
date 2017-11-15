namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_StuBatchReg_Extent
    {
        [Key]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        public int? IsEffect { get; set; }

        [Required]
        [StringLength(20)]
        public string AuthorizedEntNo { get; set; }

        [Column(TypeName = "text")]
        public string ProofText { get; set; }

        [StringLength(50)]
        public string ProofFile { get; set; }

        public int? DisperseStateID { get; set; }

        public virtual T_StuBatchReg T_StuBatchReg { get; set; }
    }
}
