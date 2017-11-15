namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Platformer
    {
        [Required]
        [StringLength(20)]
        public string PFNo { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string PFName { get; set; }

        [Key]
        [StringLength(20)]
        public string UserID { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
