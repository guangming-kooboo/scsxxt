namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_PracticePosition
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string EntPracNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string PositionID { get; set; }

        public int PubDate { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string PosDesc { get; set; }

        public int PosQuantity { get; set; }

        public int PosExpireDate { get; set; }

        public virtual C_Position C_Position { get; set; }

        public virtual T_EntBatchReg T_EntBatchReg { get; set; }
    }
}
