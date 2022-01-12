using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Globalization;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;



namespace PL
{
   internal static class HelpClass
    {
 
        internal static ObservableCollection<T> ConvertIenmurbleToObserve<T>(this IEnumerable<T> ienumerable, ObservableCollection<T> ts)
        {
            lock (ts)lock(ienumerable)
            {

                    
                    try
                    {
                        int i = 0;
                        foreach (var item in ienumerable)
                        {
                            if (i < ts.Count())
                            {
                                ts[i] = item   ;
                                i++;
                            }
                            else
                                ts.Add(item);
                        }

                        return ts;
                    }
                    catch(Exception)
                    {
                        return ts;
                    }
            }
             
        }
    }

    public class TitelBaseViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            var a = value as Label;
            var b = a.Content;
            if ((string)b == "Base Station update")
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
                return "Base Station update";

            return "Add Base Station";
        }
    }

    public class BoolToVisibaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
         {
            if (value is Boolean && (bool)value)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Visibility && (Visibility)value == Visibility.Visible)
            {
                return false;
            }
            return true;
        }
    }
}
