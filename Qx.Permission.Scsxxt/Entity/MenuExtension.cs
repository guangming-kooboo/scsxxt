using System.ComponentModel.DataAnnotations;

namespace Qx.Permission.Scsxxt.Entity
{
    public partial class MenuExtension
    {
        [Key]
        [StringLength(100)]
        public string MenuID { get; set; }

        [StringLength(50)]
        public string Controller { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        [StringLength(50)]
        public string Area { get; set; }

        [StringLength(50)]
        public string ImageClass { get; set; }

        [StringLength(50)]
        public string ActiveLi { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string ParentId { get; set; }

        [StringLength(50)]
        public string IsParent { get; set; }

        [StringLength(50)]
        public string SubSystem { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
