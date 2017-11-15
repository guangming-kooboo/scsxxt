namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_HomePageContent
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string HPCID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HPColID { get; set; }

        public int HPCSeq { get; set; }

        public virtual T_Content T_Content { get; set; }

        public virtual T_HomePageColumn T_HomePageColumn { get; set; }
    }
}
