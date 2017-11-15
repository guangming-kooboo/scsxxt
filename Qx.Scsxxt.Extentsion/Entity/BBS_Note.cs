namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BBS_Note
    {
        [StringLength(50)]
        public string NoteContent { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        [Key]
        [StringLength(20)]
        public string NoteID { get; set; }

        [StringLength(50)]
        public string ReceiverUserID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NoteTime { get; set; }

        public virtual BBS_Users BBS_Users { get; set; }
    }
}
