namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_FuncBatchOpenSetting
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(60)]
        public string FuncObjID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string PracBatchID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CloseTime { get; set; }

        public virtual T_FuncObject T_FuncObject { get; set; }
    }
}
