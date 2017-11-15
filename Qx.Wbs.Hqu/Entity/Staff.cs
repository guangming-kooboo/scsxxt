namespace Qx.Wbs.Hqu.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [StringLength(50)]
        public string StaffID { get; set; }

        [StringLength(50)]
        public string StaffName { get; set; }
    }
}
