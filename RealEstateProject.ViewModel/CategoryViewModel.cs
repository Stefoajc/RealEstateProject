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
    public class CategoryViewModel:BaseMvvmAndDbContext
    {
        

        private int categoryId;
        public int CATEGORYID
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                RaisePropertyChanged();
            }
        }

        private string param;
        public string PARAM
        {
            get { return param; }
            set
            {
                param = value;
                RaisePropertyChanged();
            }
        }

        public CategoryTypeViewModel AddCategoryValue { get; set; }
        public CategoryTypeViewModel DeleteCategoryValue { get; set; }


        public RelayCommand DeleteCategoryValueCommand { get; set; }

        public ObservableCollection<CategoryTypeViewModel> CategoryTypesListViewModel { get; set; }

        public RelayCommand<CategoriesListViewModel> SaveCategoryCommand { get; set; }
        public RelayCommand<CategoriesListViewModel> DeleteCategoryCommand { get; set; }

        public CategoryViewModel()
        {
            SaveCategoryCommand = new RelayCommand<CategoriesListViewModel>(p => SaveCategory(p) , p => { return !string.IsNullOrEmpty(PARAM); });
        }

        public CategoryViewModel(CATEGORY category)
        {
            this.CATEGORYID = category.CATEGORYID;
            this.PARAM = category.PARAMTYPE;
            this.CategoryTypesListViewModel = new ObservableCollection<CategoryTypeViewModel>();
            var catTypeList = DbContext.CATEGORYTYPES.Where(b => b.CATEGORYID == this.CATEGORYID).ToList();
            foreach (var catType in catTypeList)
            {
                CategoryTypesListViewModel.Add(new CategoryTypeViewModel(catType));
            }
            DeleteCategoryCommand = new RelayCommand<CategoriesListViewModel>(p => DeleteCategory(p), p => { return p.CategoriesList.Contains(this); });

            AddCategoryValue = new CategoryTypeViewModel();
            DeleteCategoryValueCommand = new RelayCommand(DeleteCategoryValuеAct, () => {return CategoryTypesListViewModel.Contains(DeleteCategoryValue); });
        }

        public void SaveCategory(CategoriesListViewModel p)
        {
            CATEGORY cat = new CATEGORY();
            cat.PARAMTYPE = this.PARAM;
            DbContext.CATEGORY.Add(cat);
            DbContext.SaveChanges();
            p.CategoriesList.Add(new CategoryViewModel(cat));
        }

        public void DeleteCategory(CategoriesListViewModel p)
        {
            DbContext.CATEGORY.Remove(DbContext.CATEGORY.Where(c => this.CATEGORYID == c.CATEGORYID).FirstOrDefault());
            DbContext.SaveChanges();
            p.CategoriesList.Remove(this);
        }



        public void DeleteCategoryValuеAct()
        {
            if (DeleteYesNoHelper.DeleteYesNoMessageBox("Искате ли да изтриете: " + this.DeleteCategoryValue.PARAMVALUE, "Изтриване"))
            {
                DbContext.CATEGORYTYPES.Remove(DbContext.CATEGORYTYPES.Where(c => c.CATEGORYTYPEID == DeleteCategoryValue.CATEGORYTYPEID).FirstOrDefault());
                DbContext.SaveChanges();
                this.CategoryTypesListViewModel.Remove(DeleteCategoryValue);
            }
        }
    }
}
