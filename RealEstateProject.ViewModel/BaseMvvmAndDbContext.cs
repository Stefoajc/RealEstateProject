using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using RealEstateProject.Data;

namespace RealEstateProject.ViewModel
{
    public class BaseMvvmAndDbContext:ViewModelBase
    {
        public dbContext DbContext { get; set; }

        public BaseMvvmAndDbContext():this(new dbContext())
        {
        }

        public BaseMvvmAndDbContext(dbContext dbCon)
        {
            DbContext = dbCon;
        }
    }
}
