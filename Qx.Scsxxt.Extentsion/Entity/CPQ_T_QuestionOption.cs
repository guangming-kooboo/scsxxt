namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_T_QuestionOption
    {
        [Key]
        [StringLength(50)]
        public string QuestionOptionID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionID { get; set; }

        [Required]
        [StringLength(50)]
        public string QuestionOptionIName { get; set; }

        public int SequenID { get; set; }

        public virtual CPQ_T_Question CPQ_T_Question { get; set; }
    }
}
