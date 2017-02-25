namespace RealEstateProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class REGIONS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REGIONS()
        {
            PROPERTIES = new HashSet<PROPERTIES>();
        }

        [Key]
        public int REGIONID { get; set; }

        [StringLength(50)]
        public string REGIONNAME { get; set; }

        public int CITYID { get; set; }

        public virtual CITIES CITIES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROPERTIES> PROPERTIES { get; set; }
    }
}
