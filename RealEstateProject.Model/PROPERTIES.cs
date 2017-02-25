namespace RealEstateProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    public partial class PROPERTIES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROPERTIES()
        {
            IMAGES = new HashSet<IMAGES>();
            PARAMS = new HashSet<PARAMS>();
        }

        [Key]
        public int PROPERTYID { get; set; }

        public int SIZE { get; set; }

        public decimal? RENT { get; set; }

        public decimal? SELL { get; set; }

        [Required]
        public string DESCRIPTION { get; set; }

        [Required]
        [StringLength(50)]
        public string ADDRESS { get; set; }

        public bool IsActive { get; set; }

        public int? PROPERTYTYPEID { get; set; }

        public int REGIONID { get; set; }

        public int OWNERID { get; set; }
        
        public virtual ICollection<IMAGES> IMAGES { get; set; }

        public virtual OWNERS OWNERS { get; set; }
        
        public virtual ICollection<PARAMS> PARAMS { get; set; }

        public virtual PROPERTYTYPES PROPERTYTYPES { get; set; }

        public virtual REGIONS REGIONS { get; set; }
    }
}
