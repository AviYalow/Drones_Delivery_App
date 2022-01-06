using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   internal static class HelpClass
    {
     /* internal static void ConvertIenmurbleToObserve<T>(this ObservableCollection<T> observ, IEnumerable<T> ienumerable)
       {
            observ.Clear();
            foreach(var item in ienumerable)
            {
                observ.Add(item);
            }
        }*/
        internal static ObservableCollection<T> ConvertIenmurbleToObserve<T>(this IEnumerable<T> ienumerable, ObservableCollection<T> ts)
        {
            int i = 0;
            foreach(var item in ienumerable)
            {
                if (i < ts.Count())
                {
                    ts[i] = item;
                    i++;
                        }
                else
                    ts.Add(item);
            }
            return ts;
           
        }
    }
}
