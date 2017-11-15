namespace Qx.CPQ.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPQ_T_QuestionnaireStructure
    {
        [Key]
        [StringLength(50)]
        public string QuestionnaireID { get; set; }

        public int? hasQuesionType { get; set; }

        public int? hasQuestionNumber { get; set; }
    }
}
