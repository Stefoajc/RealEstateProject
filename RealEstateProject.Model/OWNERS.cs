namespace RealEstateProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OWNERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OWNERS()
        {
            PROPERTIES = new HashSet<PROPERTIES>();
        }

        [Key]
        public int OWNERID { get; set; }

        [StringLength(50)]
        [Required]
        public string FNAME { get; set; }

        [StringLength(50)]
        [Required]
        public string LNAME { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Phone Number is Requered")]
        public string PHONE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROPERTIES> PROPERTIES { get; set; }
    }
}
