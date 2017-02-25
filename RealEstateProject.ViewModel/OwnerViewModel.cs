using GalaSoft.MvvmLight.CommandWpf;
using RealEstateProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject.ViewModel
{
    public class OwnerViewModel:BaseMvvmAndDbContext
    {


        private int id;
        public int OWNERID
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged();
            }
        }
        private string fname;
        public string FNAME
        {
            get { return fname; }
            set
            {
                fname = value;
                RaisePropertyChanged();
            }
        }
        private string lname;
        public string LNAME
        {
            get { return lname; }
            set
            {
                lname = value;
                RaisePropertyChanged();
            }
        }
        private string phone;
        public string PHONE
        {
            get { return phone; }
            set
            {
                phone = value;
                RaisePropertyChanged();
            }
        }


        public OwnerViewModel()
        {
        }

        public OwnerViewModel(OWNERS owner)
        {
            this.FNAME = owner.FNAME;
            this.LNAME = owner.LNAME;
            this.PHONE = owner.PHONE;
            this.OWNERID = owner.OWNERID;
        }


    }
}
