using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject.ViewModel
{
    public class CitiesViewModel : BaseMvvmAndDbContext
    {
        public bool IsDirty { get; set; } = false;

        public RelayCommand DeleteCityCommand { get; set; }

        private ObservableCollection<CityViewModel> citiesList;
        public ObservableCollection<CityViewModel> CitiesList
        {
            get { return citiesList; }
            set { citiesList = value; }
        }

        private CityViewModel selectedCity;
        public CityViewModel SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                RaisePropertyChanged();
            }
        }

        public CityViewModel AddCity { get; set; }

        public CitiesViewModel()
        {
            DeleteCityCommand = new RelayCommand(Delete, () => { return CitiesList.Contains(SelectedCity); });
            AddCity = new CityViewModel();
            SelectedCity = new CityViewModel();
            CitiesList = GetCities();
        }

        internal ObservableCollection<CityViewModel> GetCities()
        {
            ObservableCollection<CITIES> cities = new ObservableCollection<CITIES>(DbContext.CITIES.ToList());
            ObservableCollection<CityViewModel> citiesVM = new ObservableCollection<CityViewModel>();

            foreach (var city in cities)
            {
                citiesVM.Add(new CityViewModel(city));
            }
            return citiesVM;
        }

        public void Delete()
        {
            if (DeleteYesNoHelper.DeleteYesNoMessageBox("Искате ли да изтриете този град \n\nВнимание: Това ще изтрие и всички негови региони ", "Изтриване"))
            {
                DbContext.CITIES.Remove(DbContext.CITIES.Where(c => SelectedCity.CITYID == c.CITYID).FirstOrDefault());
                DbContext.SaveChanges();
                CitiesList.Remove(SelectedCity);
            }
        }

    }
}
