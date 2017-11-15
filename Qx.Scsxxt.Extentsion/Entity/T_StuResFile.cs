namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_StuResFile
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InnerID { get; set; }

        [StringLength(100)]
        public string ResFileMood { get; set; }

        [StringLength(100)]
        public string ResFileName { get; set; }

        [Required]
        [StringLength(100)]
        public string ResFileLink { get; set; }

        public int Secquence { get; set; }
    }
}
