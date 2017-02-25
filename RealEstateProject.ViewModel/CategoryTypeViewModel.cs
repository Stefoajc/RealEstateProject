using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject.ViewModel
{
    public class CategoryTypeViewModel:BaseMvvmAndDbContext
    {
        public RelayCommand<CategoryViewModel> AddCategoryValueCommand { get; set; }

        private int categoryTypeId;
        public int CATEGORYTYPEID
        {
            get { return categoryTypeId; }
            set
            {
                categoryTypeId = value;
                RaisePropertyChanged();
            }
        }

        private string paramValue;
        public string PARAMVALUE
        {
            get { return paramValue; }
            set
            {
                paramValue = value;
                RaisePropertyChanged();
            }
        }

        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }


        public CategoryTypeViewModel()
        {
            AddCategoryValueCommand = new RelayCommand<CategoryViewModel>(AddCategoryValue, (p) => { return !string.IsNullOrEmpty(PARAMVALUE); });
        }

        public CategoryTypeViewModel(CATEGORYTYPES catType)
        {
            this.PARAMVALUE = catType.PARAMVALUE;
            this.CATEGORYTYPEID = catType.CATEGORYTYPEID;
        }


        public void AddCategoryValue(CategoryViewModel SelectedCategoryVM)
        {
            CATEGORYTYPES cT = new CATEGORYTYPES();
            cT.PARAMVALUE = this.PARAMVALUE;
            cT.CATEGORYID = SelectedCategoryVM.CATEGORYID;
            DbContext.CATEGORYTYPES.Add(cT);
            DbContext.SaveChanges();
            SelectedCategoryVM.CategoryTypesListViewModel.Add(new CategoryTypeViewModel(cT));
        }


    }
}
