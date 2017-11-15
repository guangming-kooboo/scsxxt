namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_DownLoadFiles
    {
        [Key]
        [StringLength(50)]
        public string DLFileID { get; set; }

        public int DLFileColumnID { get; set; }

        [Required]
        [StringLength(200)]
        public string DLFileUrl { get; set; }

        public int? InnerID { get; set; }

        public virtual C_ContentColumn C_ContentColumn { get; set; }

        public virtual T_Content T_Content { get; set; }
    }
}
