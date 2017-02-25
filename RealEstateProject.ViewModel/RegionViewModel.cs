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
    public class RegionViewModel:BaseMvvmAndDbContext
    {
        private int regionId;
        public int REGIONID
        {
            get { return regionId; }
            set
            {
                regionId = value;
                RaisePropertyChanged("REGIONID");
            }
        }

        private string regionName;
        public string REGIONNAME
        {
            get { return regionName; }
            set
            {
                regionName = value;
                RaisePropertyChanged("REGIONNAME");
            }
        }

        private int cityId;
        public int CITYID
        {
            get { return cityId; }
            set { cityId = value; }
        }


        public RelayCommand<CityViewModel> SaveRegionCommand { get; set; }
        public RelayCommand<CityViewModel> DeleteRegionCommand { get; set; }

        public CityViewModel City { get; set; }

        public RegionViewModel()
        {
            SaveRegionCommand = new RelayCommand<CityViewModel>(c => SaveRegion(c), (c) => { return !string.IsNullOrEmpty(REGIONNAME); });
        }

        public RegionViewModel(REGIONS region)
        {
            this.REGIONID = region.REGIONID;
            this.REGIONNAME = region.REGIONNAME;
            this.CITYID = region.CITYID;
            DeleteRegionCommand = new RelayCommand<CityViewModel>((c) => DeleteRegion(c));
        }

        public void SaveRegion(CityViewModel city)
        {
            RegionViewModel dublicate = (RegionViewModel) this.MemberwiseClone();
            REGIONS region = new REGIONS();
            region.REGIONNAME = dublicate.REGIONNAME;
            region.CITYID = city.CITYID;
            this.REGIONNAME = "";
            DbContext.REGIONS.Add(region);
            DbContext.SaveChanges();
            city.REGIONS.Add(new RegionViewModel(region));
        }

        public void DeleteRegion(CityViewModel city)
        {
            DbContext.REGIONS.Remove(DbContext.REGIONS.Where(r => r.REGIONID == city.SelectedRegion.REGIONID).FirstOrDefault());
            DbContext.SaveChanges();
            city.REGIONS.Remove(this);
        }
    }
}
