using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject.ViewModel
{
    public class PropertyTypeViewModel:BaseMvvmAndDbContext
    {
        public RelayCommand<PropertyTypesViewModel> SavePropTypeCommand { get; set; }

        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged();
            }
        }

        private string type;
        public string TYPE
        {
            get { return type; }
            set
            {
                type = value;
                RaisePropertyChanged();
            }
        }

        public PropertyTypeViewModel()
        {
            SavePropTypeCommand = new RelayCommand<PropertyTypesViewModel>((p) => Save(p), (p) => { return !string.IsNullOrEmpty(TYPE); });
        }

        public PropertyTypeViewModel(PROPERTYTYPES propType)
        {
            this.ID = propType.ID;
            this.type = propType.TYPE;
        }

        public void Save(PropertyTypesViewModel p)
        {
            PROPERTYTYPES pT = new PROPERTYTYPES();
            pT.TYPE = this.TYPE;
            this.TYPE = "";
            DbContext.PROPERTYTYPES.Add(pT);
            DbContext.SaveChanges();
            p.PropertyTypesList.Add(new PropertyTypeViewModel(pT));
        }

    }
}
