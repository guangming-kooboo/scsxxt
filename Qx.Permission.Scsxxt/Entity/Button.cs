using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Qx.Permission.Scsxxt.Entity
{
    public partial class Button
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Button()
        {
            RoleButtonForbids = new HashSet<RoleButtonForbid>();
        }

        [StringLength(60)]
        public string ButtonID { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuID { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Note { get; set; }

        [Required]
        [StringLength(40)]
        public string Value { get; set; }

        public virtual Menu Menu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleButtonForbid> RoleButtonForbids { get; set; }
    }
}
