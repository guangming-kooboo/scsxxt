namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_AnswerQuestion
    {
        [Key]
        [StringLength(100)]
        public string AnsQueID { get; set; }

        [StringLength(500)]
        public string Answer { get; set; }

        [StringLength(100)]
        public string Question { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? QuestionTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AnswerTime { get; set; }

        [Required]
        [StringLength(20)]
        public string Ent_No { get; set; }

        public virtual T_Enterprise T_Enterprise { get; set; }
    }
}
