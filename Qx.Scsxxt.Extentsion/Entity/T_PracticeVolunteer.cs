namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_PracticeVolunteer
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PracticeNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string EntPracNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(6)]
        public string PosID { get; set; }

        public int VolunteerSequence { get; set; }

        public int PosSequence { get; set; }

        public int VolunteerStatus { get; set; }

        public int InterviewTime { get; set; }

        [Column(TypeName = "text")]
        public string InterviewRecord { get; set; }

        public double? InterviewScore { get; set; }

        [StringLength(20)]
        public string Interviewee { get; set; }

        [Required]
        [StringLength(10)]
        public string PreVolStatus { get; set; }

        public virtual C_VolPosStatus C_VolPosStatus { get; set; }
    }
}
