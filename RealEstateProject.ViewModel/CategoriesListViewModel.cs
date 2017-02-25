using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject.ViewModel
{
    public class CategoriesListViewModel:BaseMvvmAndDbContext
    {
        public RelayCommand DeleteCommand { get; set; }

        private ObservableCollection<CategoryViewModel> categoriesList;
        public ObservableCollection<CategoryViewModel> CategoriesList
        {
            get { return categoriesList; }
            set { categoriesList = value; }
        }

        private CategoryViewModel selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged();
            }
        }

        public CategoryViewModel AddCategory { get; set; }

        public CategoriesListViewModel()
        {
            DeleteCommand = new RelayCommand(Delete, () => { return CategoriesList.Contains(SelectedCategory); });
            AddCategory = new CategoryViewModel();
            CategoriesList = new ObservableCollection<CategoryViewModel>();
            var catsList = DbContext.CATEGORY.ToList();
            foreach (var cat in catsList)
            {
                CategoriesList.Add(new CategoryViewModel(cat));
            }
        }

        public void Delete()
        {
            if (DeleteYesNoHelper.DeleteYesNoMessageBox("Искате ли да изтриете: " + this.SelectedCategory.PARAM +"/n Важно: Това ще изтрие и всички негови подкатегории", "Изтриване"))
            {
                DbContext.CATEGORY.Remove(DbContext.CATEGORY.Where(c => SelectedCategory.CATEGORYID == c.CATEGORYID).FirstOrDefault());
                DbContext.SaveChanges();
                CategoriesList.Remove(SelectedCategory);
            }
        }
    }
}
