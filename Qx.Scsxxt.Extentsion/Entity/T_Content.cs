namespace Qx.Scsxxt.Extentsion.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Content
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Content()
        {
            T_HomePageContent = new HashSet<T_HomePageContent>();
        }

        [Key]
        [StringLength(50)]
        public string ContentID { get; set; }

        public int ContentTypeID { get; set; }

        [StringLength(100)]
        public string ContentTitle { get; set; }

        [StringLength(50)]
        public string ContentPublisher { get; set; }

        public int? PubDate { get; set; }

        [StringLength(20)]
        public string UnitID { get; set; }

        public virtual C_ContentType C_ContentType { get; set; }

        public virtual T_ADs T_ADs { get; set; }

        public virtual T_DownLoadFiles T_DownLoadFiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_HomePageContent> T_HomePageContent { get; set; }

        public virtual T_News T_News { get; set; }

        public virtual T_Signature T_Signature { get; set; }

        public virtual T_Stamps T_Stamps { get; set; }
    }
}
