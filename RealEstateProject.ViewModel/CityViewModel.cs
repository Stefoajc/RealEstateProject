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
    public class CityViewModel : BaseMvvmAndDbContext
    {

        private int cityId;
        public int CITYID
        {
            get { return cityId; }
            set
            {
                cityId = value;
                RaisePropertyChanged("CITYID");
            }
        }

        private string cityName;
        public string CITYNAME
        {
            get { return cityName; }
            set
            {
                cityName = value;
                RaisePropertyChanged("CITYNAME");
            }
        }

        public RegionViewModel AddRegion { get; set; }
        public RegionViewModel SelectedRegion { get; set; }

        public int SelectedRegionId { get; set; }

        private ObservableCollection<RegionViewModel> regions;

        public ObservableCollection<RegionViewModel> REGIONS
        {
            get { return regions; }
            set
            {
                regions = value;
                RaisePropertyChanged("REGIONS");
            }
        }


        public RelayCommand<CitiesViewModel> SaveCityCommand { get; set; }
        public RelayCommand<CitiesViewModel> DeleteCityCommand { get; set; }
        public RelayCommand DeleteRegion { get; set; }

        public CityViewModel()
        {
            SaveCityCommand = new RelayCommand<CitiesViewModel>(p => SaveCity(p), p => { return !string.IsNullOrEmpty(CITYNAME); });
        }
        public CityViewModel(CITIES city)
        {
            this.CITYID = city.CITYID;
            this.CITYNAME = city.CITYNAME;
            GetRegions();
            //DeleteCityCommand = new RelayCommand<CitiesViewModel>(p => DeleteCity(p), (p)=> { return p.CitiesList.Contains(this) && p.SelectedCity!=null; });
            AddRegion = new RegionViewModel();
            DeleteRegion = new RelayCommand(DeleteReg, () => { return REGIONS.Contains(SelectedRegion); });
        }

        internal ObservableCollection<RegionViewModel> GetRegions()
        {
            REGIONS = new ObservableCollection<RegionViewModel>();
            ObservableCollection<REGIONS> regs = new ObservableCollection<Model.REGIONS>(DbContext.REGIONS.Where(b => b.CITYID == this.CITYID).ToList());
            foreach (var reg in regs)
            {
                RegionViewModel r = new RegionViewModel(reg);
                r.City = this;
                REGIONS.Add(r);
            }
            return null;
        }

        public void SaveCity(CitiesViewModel p)
        {
            CITIES city = new CITIES();
            city.CITYNAME = this.CITYNAME;
            this.CITYNAME = "";
            DbContext.CITIES.Add(city);
            DbContext.SaveChanges();
            p.CitiesList.Add(new CityViewModel(city));
        }

        public void DeleteCity(CitiesViewModel p)
        {

            DbContext.CITIES.Remove(DbContext.CITIES.Where(c => this.CITYID == c.CITYID).FirstOrDefault());
            DbContext.SaveChanges();
            p.CitiesList.Remove(this);

        }





        public void DeleteReg()
        {
            if (DeleteYesNoHelper.DeleteYesNoMessageBox("Искате ли да изтриете: " + this.SelectedRegion.REGIONNAME, "Изтриване"))
            {
                DbContext.REGIONS.Remove(DbContext.REGIONS.Where(r => r.REGIONID == SelectedRegionId).FirstOrDefault());
                DbContext.SaveChanges();
                this.REGIONS.Remove(REGIONS.Where(r => r.REGIONID == this.SelectedRegionId).FirstOrDefault());
            }
        }
    }
}
