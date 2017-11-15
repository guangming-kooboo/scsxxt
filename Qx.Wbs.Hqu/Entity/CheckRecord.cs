namespace Qx.Wbs.Hqu.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckRecord")]
    public partial class CheckRecord
    {
        [Key]
        [StringLength(50)]
        public string FinishID { get; set; }

        [StringLength(50)]
        public string TaskName { get; set; }

        public int? TaskType { get; set; }

        public int? State { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Auditor { get; set; }

        [StringLength(50)]
        public string CheckOpinion { get; set; }

        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string OwerID { get; set; }

        [StringLength(50)]
        public string WbsTaskID { get; set; }
    }
}
