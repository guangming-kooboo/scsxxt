namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_PhoneMsg
    {
        [StringLength(8000)]
        public string MsgContent { get; set; }

        [StringLength(8000)]
        public string Phone { get; set; }

        [StringLength(2)]
        public string Flag { get; set; }

        [StringLength(50)]
        public string ID { get; set; }
    }
}
