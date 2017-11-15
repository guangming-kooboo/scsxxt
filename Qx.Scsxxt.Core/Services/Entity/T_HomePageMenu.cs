namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_HomePageMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HPMID { get; set; }

        [Required]
        [StringLength(50)]
        public string HPMName { get; set; }

        public int FatherID { get; set; }

        [StringLength(100)]
        public string ActionLink { get; set; }

        [Required]
        [StringLength(20)]
        public string UnitID { get; set; }
    }
}
