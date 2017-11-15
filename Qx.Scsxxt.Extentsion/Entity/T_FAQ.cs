namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_FAQ
    {
        [Key]
        [StringLength(100)]
        public string FAQID { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Contents { get; set; }

        public DateTime CreatTime { get; set; }

        [Required]
        [StringLength(20)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string FAQTypeId { get; set; }

        [Required]
        [StringLength(20)]
        public string StateID { get; set; }

        [StringLength(100)]
        public string Video { get; set; }

        [StringLength(100)]
        public string CreatorName { get; set; }

        [StringLength(200)]
        public string Abstract { get; set; }

        [Column(TypeName = "text")]
        public string Pic { get; set; }

        public virtual C_FAQState C_FAQState { get; set; }

        public virtual C_FAQType C_FAQType { get; set; }
    }
}
