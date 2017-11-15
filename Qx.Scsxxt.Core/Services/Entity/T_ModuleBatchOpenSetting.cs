namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_ModuleBatchOpenSetting
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ModuleID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CloseTime { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string PracBatchID { get; set; }
    }
}
