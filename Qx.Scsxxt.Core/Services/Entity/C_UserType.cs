namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class C_UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserTypeID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserTypeName { get; set; }
    }
}
