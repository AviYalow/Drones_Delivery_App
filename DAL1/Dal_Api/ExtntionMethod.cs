using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Dal
{
  static class Cloneing 
    {
        internal static T Clone<T> (this T surce) where T: new()
        {
            
           
            T destintion = new T();
              foreach (PropertyInfo info in surce.GetType(). GetProperties())
              {
                if (info.CanWrite && info.CanRead) // (item.PropertyType.IsValueType)
                {
                    object srcValue = info.GetValue(surce, null);
                    info.SetValue(destintion, srcValue);
                }
                info.SetValue(destintion, info.GetValue(surce, null),null);

              }
           
        
        
            return destintion;
        }
    }
}
