namespace Core.Services.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_News
    {
        [Key]
        [StringLength(50)]
        public string NewsID { get; set; }

        public int NewsTypeID { get; set; }

        public string NewsAuthor { get; set; }

        public int FlowState { get; set; }

        public int IsShow { get; set; }

        public int IsLocked { get; set; }

        public int? NewsColumnID { get; set; }

        public string PicUrl { get; set; }

        public string VideoUrl { get; set; }

        [Column(TypeName = "text")]
        public string Html { get; set; }

        public string LinkUrl { get; set; }

        public string Download { get; set; }

        public int? InnerID { get; set; }

        public virtual C_ContentColumn C_ContentColumn { get; set; }

        public virtual C_NewsState C_NewsState { get; set; }

        public virtual C_NewsType C_NewsType { get; set; }

        public virtual T_Content T_Content { get; set; }
    }
}
