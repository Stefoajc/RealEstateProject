using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace RealEstateProject.ViewModel
{
    public class PropertyTypesViewModel:BaseMvvmAndDbContext
    {
        public bool IsDirty { get; set; }

        public RelayCommand AddPropType { get; set; }
        public RelayCommand DeletePropType { get; set; }

        public ObservableCollection<PropertyTypeViewModel> PropertyTypesList { get; set; }

        public PropertyTypeViewModel SelectedPropertyType { get; set; }

        public PropertyTypeViewModel AddedPropertyType { get; set; }

        public PropertyTypesViewModel()
        {
            DeletePropType = new RelayCommand(Delete, () => { return PropertyTypesList.Contains(SelectedPropertyType); });

            //SelectedPropertyType = new PropertyTypeViewModel();
            AddedPropertyType = new PropertyTypeViewModel();
            PropertyTypesList = new ObservableCollection<PropertyTypeViewModel>();
            ObservableCollection<PROPERTYTYPES> ptl = new ObservableCollection<PROPERTYTYPES>(DbContext.PROPERTYTYPES.ToList());
            foreach (var pT in ptl)
            {
                PropertyTypesList.Add(new PropertyTypeViewModel(pT));
            }
            IsDirty = false;
        }

        public void Delete()
        {
            if (DeleteYesNoHelper.DeleteYesNoMessageBox("Искате ли да изтриете този елемент ?", "Изтриване"))
            {
                DbContext.PROPERTYTYPES.Remove(DbContext.PROPERTYTYPES.Where(p => p.ID == SelectedPropertyType.ID).FirstOrDefault());
                DbContext.SaveChanges();
                PropertyTypesList.Remove(SelectedPropertyType);
                IsDirty = true;
            }
        }

    }
}
