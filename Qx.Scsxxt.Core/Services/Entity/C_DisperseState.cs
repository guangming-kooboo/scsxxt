namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_DisperseState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DisperseStateID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
