using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;



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
                                ts[i] = item;
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
}
