namespace RealEstateProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PARAMS
    {
        public int PARAMSID { get; set; }

        public int PROPERTYID { get; set; }

        public int CATEGORYNAME { get; set; }

        public int CATEGORYVALUES { get; set; }

        public virtual CATEGORY CATEGORY { get; set; }

        public virtual CATEGORYTYPES CATEGORYTYPES { get; set; }

        public virtual PROPERTIES PROPERTIES { get; set; }
    }
}
