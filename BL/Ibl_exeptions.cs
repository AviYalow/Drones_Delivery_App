using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

        public class Item_found_exeptin : Exception
        {
            IDAL.DO.Item_found_exception exeption { get; set; }
            public Item_found_exeptin(IDAL.DO.Item_found_exception ex) { exeption = ex; }
            public override string ToString()
            {
                string exeptionString = "";
                DateTime time = DateTime.Now;
                exeptionString += $"\aTime:{time.ToLongTimeString()}\n";
                exeptionString += exeption.ToString();
                return exeptionString;
            }
        }
        public class Item_not_found_exeptin : Exception
        {
            IDAL.DO.Item_not_found_exception exeption { get; set; }
            public Item_not_found_exeptin(IDAL.DO.Item_not_found_exception ex) { exeption = ex; }
            public override string ToString()
            {
                string exeptionString = "";
                DateTime time = DateTime.Now;
                exeptionString += $"\aTime:{time.ToLongTimeString()}\n";
                exeptionString += exeption.ToString();
                return exeptionString;
            }
        }
    }
}
