using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RealEstateProject.ViewModel
{
    public class  ValidationRules
    {
    }

    public class AgeRangeRule : ValidationRule
    {
        private int _min;
        private int _max;

        public AgeRangeRule()
        {
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false,
                  "Please enter an age in the range: " + Min + " - " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    public class RegExRule : ValidationRule
    {
        private string pattern;

        public string Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            try
            {
                if (!Regex.IsMatch((string)value, pattern))
                {
                    throw new ArgumentException();
                }
            }catch(Exception e)
            {
                return new ValidationResult(false, "Шаблона е нарушен!");
            }

            return new ValidationResult(true, null);
        }
    }

}
