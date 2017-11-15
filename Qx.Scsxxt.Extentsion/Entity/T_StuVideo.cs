namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_StuVideo
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
        public string VideoMood { get; set; }

        [StringLength(100)]
        public string VideoName { get; set; }

        [Required]
        [StringLength(100)]
        public string VideoLink { get; set; }

        public int Secquence { get; set; }
    }
}
