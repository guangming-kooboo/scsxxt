namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_City
    {
        [Required]
        [StringLength(6)]
        public string ProvinceID { get; set; }

        [Key]
        [StringLength(6)]
        public string CityID { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        public virtual C_Province C_Province { get; set; }
    }
}
