namespace RealEstateProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CATEGORYTYPES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATEGORYTYPES()
        {
            PARAMS = new HashSet<PARAMS>();
        }

        [Key]
        public int CATEGORYTYPEID { get; set; }

        public int CATEGORYID { get; set; }

        [StringLength(50)]
        public string PARAMVALUE { get; set; }

        public virtual CATEGORY CATEGORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PARAMS> PARAMS { get; set; }
    }
}
