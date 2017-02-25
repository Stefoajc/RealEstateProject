using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstateProject.ViewModel
{
    public class OwnersViewModel : BaseMvvmAndDbContext
    {
        public RelayCommand SaveOwner { get; set; }
        public RelayCommand DeleteOwner { get; set; }
        public RelayCommand NewOwner { get; set; }

        private ObservableCollection<OwnerViewModel> ownersList;
        public ObservableCollection<OwnerViewModel> OwnersList
        {
            get { return ownersList; }
            set { ownersList = value; }
        }

        private OwnerViewModel owner;
        public OwnerViewModel SelectedOwner
        {
            get { return owner; }
            set
            {
                owner = value;
                RaisePropertyChanged();
            }
        }


        public OwnersViewModel()
        {
            DeleteOwner = new RelayCommand(Delete, () => { return OwnersList.Contains(SelectedOwner); });
            SaveOwner = new RelayCommand(Save, () => { return !string.IsNullOrEmpty(SelectedOwner.FNAME) && !string.IsNullOrEmpty(SelectedOwner.LNAME) && !string.IsNullOrEmpty(SelectedOwner.PHONE); });
            NewOwner = new RelayCommand(New, () => { return !string.IsNullOrEmpty(SelectedOwner.FNAME) || !string.IsNullOrEmpty(SelectedOwner.LNAME) || !string.IsNullOrEmpty(SelectedOwner.PHONE); });

            SelectedOwner = new OwnerViewModel();
            OwnersList = GetOwners();
        }

        internal ObservableCollection<OwnerViewModel> GetOwners()
        {
            ObservableCollection<OwnerViewModel> OwnersLst = new ObservableCollection<OwnerViewModel>();
            ObservableCollection<OWNERS> owners = new ObservableCollection<OWNERS>(DbContext.OWNERS.ToList());
            foreach (var owner in owners)
            {
                OwnersLst.Add(new OwnerViewModel(owner));
            }

            return OwnersLst;
        }

        public void Save()
        {
            if (SelectedOwner.OWNERID == 0)
            {
                Add();

            }
            else
            {
                Update();
            }
        }

        public void Add()
        {
            OWNERS dbOwner = new OWNERS();
            dbOwner.FNAME = SelectedOwner.FNAME;
            dbOwner.LNAME = SelectedOwner.LNAME;
            dbOwner.PHONE = SelectedOwner.PHONE;
            SelectedOwner.FNAME = "";
            SelectedOwner.LNAME = "";
            SelectedOwner.PHONE = "";
            SelectedOwner.OWNERID = 0;
            DbContext.OWNERS.Add(dbOwner);
            DbContext.SaveChanges();
            OwnersList.Add(new OwnerViewModel(dbOwner));
        }

        public void Delete()
        {
            if (DeleteYesNoHelper.DeleteYesNoMessageBox("Искате ли да изтриете този собственик", "Изтриване"))
            {
                try
                {
                    DbContext.OWNERS.Remove(DbContext.OWNERS.Where(o => o.OWNERID == SelectedOwner.OWNERID).FirstOrDefault());
                    DbContext.SaveChanges();
                    OwnersList.Remove(SelectedOwner);
                    SelectedOwner = new OwnerViewModel();
                }
                catch
                {
                    MessageBox.Show("No item Selected");
                }
            }
        }

        public void Update()
        {
            OWNERS dbOwner = DbContext.OWNERS.Find(SelectedOwner.OWNERID);
            dbOwner.FNAME = SelectedOwner.FNAME;
            dbOwner.LNAME = SelectedOwner.LNAME;
            dbOwner.PHONE = SelectedOwner.PHONE;
            DbContext.SaveChanges();
        }


        public void New()
        {
            SelectedOwner = new OwnerViewModel();
        }


    }
}
