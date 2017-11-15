namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_ReportData
    {
        [Key]
        [StringLength(50)]
        public string ReportID { get; set; }

        [Required]
        [StringLength(500)]
        public string ReportName { get; set; }

        [Required]
        [StringLength(8000)]
        public string SqlStr { get; set; }

        [Required]
        [StringLength(500)]
        public string HeadFields { get; set; }

        public int RecordsPerPage { get; set; }

        [StringLength(8000)]
        public string ParaNames { get; set; }

        [StringLength(500)]
        public string ColunmToShow { get; set; }

        [StringLength(8000)]
        public string OperateColum { get; set; }

        [StringLength(8000)]
        public string ReportLog { get; set; }
    }
}
