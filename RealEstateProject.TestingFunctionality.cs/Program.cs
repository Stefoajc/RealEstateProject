using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject.TestingFunctionality.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            //IQueryable<string> strings = (new[] { "Jon", "Tom", "Holly",
            // "Robin", "William" }).AsQueryable();

            //IQueryable<string> filter = (new[] { "ll", "om", "Ho",
            // "in" }).AsQueryable();

            //var q = PredicateBuilder.True<string>();
            //foreach (var item in filter)
            //{
            //    var temp = item;
            //}


            //foreach (string x in query)
            //{
            //    Console.WriteLine(x);
            //}

        }
    }

    class Master
    {
        public int MID { get; set; }
        public string desc { get; set; }
    }

    class Detail
    {
        public int DID { get; set; }
        public int MID { get; set; }
        public string Text { get; set; }
    }

}
