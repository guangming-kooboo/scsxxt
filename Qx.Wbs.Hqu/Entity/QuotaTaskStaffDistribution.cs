namespace Qx.Wbs.Hqu.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuotaTaskStaffDistribution
    {
        [StringLength(50)]
        public string QuotaTaskStaffDistributionID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuotaTaskID { get; set; }

        [Required]
        [StringLength(50)]
        public string StaffID { get; set; }

        public double RelativeWeight { get; set; }

        public double? AbsoluteWeight { get; set; }

        [Required]
        [StringLength(50)]
        public string StaffName { get; set; }

        [StringLength(250)]
        public string DistributionExplain { get; set; }

        public int IsComplete { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? FinishTime { get; set; }

        [StringLength(500)]
        public string Certificate { get; set; }

        public int StaffSequence { get; set; }

        public virtual QuotaTask QuotaTask { get; set; }
    }
}
