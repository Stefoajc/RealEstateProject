using RealEstateProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject.ViewModel
{
    public class ImagesViewModel:BaseMvvmAndDbContext
    {
        public string SelectedImage { get; set; }

        public ObservableCollection<string> Images { get; set; }

        public ImagesViewModel()
        {
            Images = new ObservableCollection<string>();
        }
    }
}
