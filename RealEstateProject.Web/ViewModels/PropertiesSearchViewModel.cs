using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateProject.Web.ViewModels
{
    public class PropertiesSearchViewModel
    {
        public decimal RentFrom { get; set; }
        public decimal RentTo { get; set; }

        public decimal SellFrom { get; set; }
        public decimal SellTo { get; set; }

        public int SizeFrom { get; set; }
        public int SizeTo { get; set; }

        public int City { get; set; }
        public int Region { get; set; }

        public List<int> PropType { get; set; }

        public List<int> Features { get; set; }

        public PropertiesSearchViewModel(PropertiesSearchViewModel psvm)
        {
            this.RentFrom = psvm.RentFrom;
            this.RentTo = psvm.RentTo;
            this.SellFrom = psvm.SellFrom;
            this.SellTo = psvm.SellTo;
            this.SizeFrom = psvm.SizeFrom;
            this.SizeTo = psvm.SizeTo;
            this.City = psvm.City;
            this.Region = psvm.Region;
            if(psvm.PropType != null)
            {
                psvm.PropType.Concat(this.PropType);
            }
            if(psvm.Features != null)
            {
                psvm.Features.Concat(this.Features);
            }
        }

        public PropertiesSearchViewModel()
        {
            PropType = new List<int>();
            Features = new List<int>();
        }

        public override string ToString()
        {
            string[] toString = new string[100];
            int i = 0;

            toString[i++] += CheckNullOrZero(RentFrom,"RentFrom");
            toString[i++] += CheckNullOrZero(RentTo, "RentTo");
            toString[i++] += CheckNullOrZero(SellFrom, "SellFrom");
            toString[i++] += CheckNullOrZero(SellTo, "SellTo");
            toString[i++] += CheckNullOrZero(SizeFrom, "SizeFrom");
            toString[i++] += CheckNullOrZero(SizeTo, "SizeTo");
            toString[i++] += CheckNullOrZero(City, "City");
            toString[i++] += CheckNullOrZero(Region, "Region");
            if(PropType != null)
            {
                toString[i] += "PropType=";
                foreach (var item in PropType)
                {
                    toString[i] += item.ToString() + ",";
                }
                toString[i] = toString[i].TrimEnd(',');
                i++;
            }
            if (Features != null)
            {
                toString[i] += "Features=";
                foreach (var item in Features)
                {
                    toString[i] += item.ToString() + ",";
                }
                toString[i] = toString[i].TrimEnd(',');
                i++;
            }

            string filters ="";

            foreach (var str in toString)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    filters += str;
                    filters += "&";
                }
            }
            filters = filters.TrimEnd('&');
            return filters;
        }

        string CheckNullOrZero(decimal value , string name)
        {
            return value != 0 ? name + "=" + value.ToString() : "";
        }

        public PropertiesSearchViewModel StringToPSVM(string propType, string Features
            , int RentFrom = 0, int RentTo = 0, int SellFrom = 0, int SellTo = 0, int SizeFrom = 0, int SizeTo = 0, int City = 0, int Region = 0)
        {
            var prType = propType != null ? propType.Split(','): new string[1] { "" }  ;
            var fts = Features != null ? Features.Split(',') : new string[1] { "" } ;
            PropertiesSearchViewModel PSVM = new PropertiesSearchViewModel();

            foreach (var item in prType)
            {
                if(!string.IsNullOrEmpty(item))
                {
                    this.PropType.Add(int.Parse(item));
                }
            }

            foreach (var item in fts)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    this.Features.Add(int.Parse(item));
                }
            }

            this.RentFrom = RentFrom;
            this.RentTo = RentTo;
            this.SellFrom = SellFrom;
            this.SellTo = SellTo;
            this.SizeFrom = SizeFrom;
            this.SizeTo = SizeTo;
            this.City = City;
            this.Region = Region;

            return this;
        }
    }
}