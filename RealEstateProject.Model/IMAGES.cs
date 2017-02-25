namespace RealEstateProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMAGES
    {
        [Key]
        public int IMAGEID { get; set; }

        public string IMAGE { get; set; }

        public int? PID { get; set; }

        public virtual PROPERTIES PROPERTIES { get; set; }
    }
}
