using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject.ViewModel.Comands
{
    public class SaveToDBCommand:BaseMvvmAndDbContext
    {
        public RelayCommand SaveCommand { get; set; }

        public void Save()
        {
            PropertyViewModel proper = new PropertyViewModel();
            
            int i;
            i = 10;
            i--;
        }

        public SaveToDBCommand()
        {
            
            SaveCommand = new RelayCommand(() => Save());
        }
    }
}
