using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BO;
using System.Windows.Shapes;
using System.Globalization;
using System.Collections.ObjectModel;
using BlApi;


namespace PL
{


    public class NotEmptyValidationRule : ValidationRule
    {
     
        

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (string.IsNullOrWhiteSpace((value ?? "").ToString()) || ((value ?? "0").ToString() == "0"))
                return new ValidationResult(false, "Field is required.");
                return ValidationResult.ValidResult;
        }
    }
    public class NotEmptyByItemValidationRule : ValidationRule
    {



        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (value is null)
                return new ValidationResult(false, "Field is required.");
            return ValidationResult.ValidResult;
        }
    }
    public class VisibilityToBooleanConverter :IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    public class InputERRORWithPointValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
           
            if ((value ?? "").ToString().EndsWith('.'))
                return new ValidationResult(false, "You cant end with point!"); 
            if((value ?? "").ToString().Count(x=>x=='.')>1)
                return new ValidationResult(false, "Cant by more the one point!");
            if ((value ?? "").ToString().StartsWith('.'))
                return new ValidationResult(false, "You cant start with point!");

            if ((value ?? "").ToString().Any(x => (x < '0' || x > '9')&&x!='.'))
                return new ValidationResult(false, "only a digit number!");

            return ValidationResult.ValidResult;
        }
    }

    public class InputERRORValidationRule : ValidationRule
    {
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if ((value ?? "").ToString().Any(x=>x<'0'||x>'9'))
                return new ValidationResult(false, "Input ERROR!");
            return ValidationResult.ValidResult;
        }
    }

}
