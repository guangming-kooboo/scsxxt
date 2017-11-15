namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_ADs
    {
        [Key]
        [StringLength(50)]
        public string ADID { get; set; }

        [Required]
        [StringLength(100)]
        public string ADTitle { get; set; }

        public int? ADTypeID { get; set; }

        [StringLength(50)]
        public string ADOwner { get; set; }

        [Required]
        [StringLength(50)]
        public string ADPubUser { get; set; }

        public int? PubTime { get; set; }

        public int? StartTime { get; set; }

        public int? EndTime { get; set; }

        public int IsForbid { get; set; }

        public int ADColumnID { get; set; }

        [StringLength(100)]
        public string MianPic { get; set; }

        [StringLength(100)]
        public string LinkUrl { get; set; }

        public int? InnerID { get; set; }

        [StringLength(20)]
        public string UnitID { get; set; }

        public virtual C_ContentColumn C_ContentColumn { get; set; }

        public virtual T_Content T_Content { get; set; }
    }
}
