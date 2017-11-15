namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_ADColumn
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ADColumnID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ADColumnName { get; set; }

        [StringLength(10)]
        public string SybSystem { get; set; }
    }
}
