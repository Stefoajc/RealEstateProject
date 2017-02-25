using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using RealEstateProject.Model;
using RealEstateProject.Data;

namespace RealEstateProject.ViewModel
{
    public class TestClass : BaseMvvmAndDbContext
    {
        public string Name { get; set; }

        public TestClass()
        {
            DbContext.CATEGORY.FirstOrDefault();
        }
    }
}
