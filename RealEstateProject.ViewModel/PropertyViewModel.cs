using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstateProject.ViewModel
{
    public class PropertyViewModel:BaseMvvmAndDbContext
    {
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand BrowseImageCommand { get; set; }
        public RelayCommand DeleteImageCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand TestCommand { get; set; }


        public IDictionary<string,int> ChangeTracker { get; set; }

        private bool canSearch;

        public bool CanSearch
        {
            get { return canSearch; }
            set
            {
                canSearch = value;
                RaisePropertyChanged();
                SearchCommand.RaiseCanExecuteChanged();
            }
        }



        private int propId;
        public int PROPERTYID
        {
            get { return propId; }
            set
            {
                propId = value;
                RaisePropertyChanged("PROPERTYID");
            }
        }

        private int size;
        public int SIZE
        {
            get { return size; }
            set
            {
                size = value;
                RaisePropertyChanged("SIZE");
            }
        }

        private decimal rent;
        public decimal RENT
        {
            get { return rent; }
            set
            {
                rent = value;
                RaisePropertyChanged("RENT");
            }
        }

        private decimal sell;
        public decimal SELL
        {
            get { return sell; }
            set
            {
                sell = value;
                RaisePropertyChanged("SELL");
            }
        }

        private string description;
        public string DESCRIPTION
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("DESCRIPTION");
            }
        }

        private string address;
        public string ADDRESS
        {
            get { return address; }
            set
            {
                address = value;
                RaisePropertyChanged("ADDRESS");
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                RaisePropertyChanged("IsActive");
            }
        }

        private int propertyTypeId;
        public int PROPERTYTYPEID
        {
            get { return propertyTypeId; }
            set
            {
                propertyTypeId = value;
                RaisePropertyChanged("PROPERTYTYPEID");
            }
        }

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

        private int ownerId;
        public int OWNERID
        {
            get { return ownerId; }
            set
            {
                ownerId = value;
                RaisePropertyChanged("OWNERID");
            }
        }


        private CitiesViewModel cities;
        public CitiesViewModel CITIES
        {
            get { return cities; }
            set
            {
                cities = value;
                RaisePropertyChanged();
            }
        }



        //private OwnersViewModel owners;
        //public OwnersViewModel OWNERS
        //{
        //    get { return owners; }
        //    set
        //    {
        //        owners = value;
        //        RaisePropertyChanged();
        //    }
        //}


        private PropertyTypesViewModel propertyTypes;

        public PropertyTypesViewModel PROPERTYTYPES
        {
            get { return propertyTypes; }
            set
            {
                propertyTypes = value;
                RaisePropertyChanged();
            }
        }



        private CategoriesListViewModel categorie;
        public CategoriesListViewModel CATEGORIES
        {   
            get { return categorie; }
            set
            {
                categorie = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<PARAMS> PARAMS { get; set; }

        public ImagesViewModel IMAGES { get; set; }



        //public ObservableCollection<OWNERS> OWNERS { get; set; }

        public PropertyViewModel()
        {
            //TestCommand = new RelayCommand(Refresh);
            SaveCommand = new RelayCommand(Save,CanSave);
            BrowseImageCommand = new RelayCommand(Browse);
            DeleteImageCommand = new RelayCommand(Delete);
            //RefreshCommand = new RelayCommand(Refresh);
            SearchCommand = new RelayCommand(Search, () => { return !(this.PROPERTYID == 0); }); // Намери подходящи условия за вкл/изкл на бутона "Търсене"!


            CITIES = new CitiesViewModel();
            PROPERTYTYPES = new PropertyTypesViewModel();
            //OWNERS = new OwnersViewModel();
            CATEGORIES = new CategoriesListViewModel();
            IMAGES = new ImagesViewModel();
            isActive = true;
            ChangeTracker = new Dictionary<string, int>();
            ChangeTracker.Add(new KeyValuePair<string, int>(("PROPERTYTYPES"), PROPERTYTYPES.PropertyTypesList.Count));
        }

       

        public void Save()
        {
            
            PROPERTIES property = new PROPERTIES();
            property.DESCRIPTION = this.DESCRIPTION;
            property.ADDRESS = this.ADDRESS;
            property.OWNERID = this.OWNERID;
            property.SIZE = this.SIZE;
            property.SELL = decimal.Round(this.SELL,2);
            property.RENT = decimal.Round(this.RENT,2);
            property.REGIONID = this.REGIONID;
            property.IsActive = this.IsActive;
            property.PROPERTYTYPEID = this.PROPERTYTYPEID;
            DbContext.PROPERTIES.Add(property);
            DbContext.SaveChanges();

            PARAMS = GetParams(property.PROPERTYID);
            foreach (var param in this.PARAMS)
            {
                DbContext.PARAMS.Add(param);
            }

            foreach (var img in IMAGES.Images)
            {
                var str = img.Remove(0, 93);
                DbContext.IMAGES.Add(new IMAGES() { IMAGE = str, PID = property.PROPERTYID });
            }

            DbContext.SaveChanges();
        }

        public bool CanSave()
        {
            return (OWNERID != 0) && !string.IsNullOrEmpty(ADDRESS) && (SIZE != 0) && (SELL != 0 || RENT != 0) ;
        }

        public ObservableCollection<PARAMS> GetParams(int propId)
        {
            ObservableCollection<PARAMS> getParams = new ObservableCollection<Model.PARAMS>();
            foreach (var category in CATEGORIES.CategoriesList)
            {
                foreach (var categoryValue in category.CategoryTypesListViewModel)
                {
                    if (categoryValue.IsChecked == true)
                    {
                        PARAMS param = new Model.PARAMS();
                        param.CATEGORYNAME = category.CATEGORYID;
                        param.CATEGORYVALUES = categoryValue.CATEGORYTYPEID;
                        param.PROPERTYID = propId;
                        getParams.Add(param);
                    }
                }
            }
            return getParams;
        }

        public void Browse()
        {

            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "All Files (*.*)|*.*|JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();


            // Get the selected file name and display in a ListBox of image items 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;

                IMAGES.Images.Add(filename);
            }
        }
        public void Delete()
        {
            IMAGES.Images.Remove(IMAGES.SelectedImage);
        }

        public void Search()
        {
            PROPERTIES property = DbContext.PROPERTIES.Where(p => this.PROPERTYID == p.PROPERTYID).FirstOrDefault();
            if(property != null)
            { 
                SearchHelper(property);
            }
        }

        public void SearchHelper(PROPERTIES prop)
        {
            this.RENT = prop.RENT == null ? 0 : (decimal)prop.RENT;
            this.SELL = prop.SELL == null ? 0 : (decimal)prop.SELL;
            this.SIZE = prop.SIZE;
            this.IsActive = prop.IsActive;
            this.REGIONID = prop.REGIONID;
            this.PROPERTYTYPEID = prop.PROPERTYTYPEID == null ? -1 : (int)prop.PROPERTYTYPEID;
            this.DESCRIPTION = prop.DESCRIPTION;
            this.ADDRESS = prop.ADDRESS;
            this.OWNERID = prop.OWNERID;
            this.CITIES.SelectedCity = this.CITIES.CitiesList.Where(c => c.CITYID == prop.REGIONS.CITYID).FirstOrDefault();
            foreach (var category in CATEGORIES.CategoriesList)
            {
                foreach (var param in prop.PARAMS)
                {
                    if (param.CATEGORYNAME == category.CATEGORYID)
                    {
                        foreach (var catValue in category.CategoryTypesListViewModel)
                        {
                            if (catValue.CATEGORYTYPEID == param.CATEGORYVALUES)
                            {
                                catValue.IsChecked = true;
                            }
                            else
                            {
                                catValue.IsChecked = false;
                            }
                        }
                    }
                }
                
            }

            this.IMAGES.Images.Clear();
            foreach (var image in prop.IMAGES)
            {
                if(!image.IMAGE.Contains(@"C:\Users\Стефан\Documents\Visual Studio 2015\Projects\RealEstateProject\RealEstateProject.Web"))
                {
                    this.IMAGES.Images.Add(@"C:\Users\Стефан\Documents\Visual Studio 2015\Projects\RealEstateProject\RealEstateProject.Web" + image.IMAGE);
                }
            }
        }



        //public void Refresh()
        //{
        //    if (PROPERTYTYPES.IsDirty == true || ChangeTracker["PROPERTYTYPES"] != PROPERTYTYPES.PropertyTypesList.Count)
        //    {
        //        PROPERTYTYPES = new PropertyTypesViewModel();
        //    }
        //}
    }
}
