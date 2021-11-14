using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

        public class Item_found_exeption : Exception
        {
            IDAL.DO.Item_found_exception exeption { get; set; }
            public Item_found_exeption(IDAL.DO.Item_found_exception ex) { exeption = ex; }
            public override string ToString()
            {
                string exeptionString = "";
                DateTime time = DateTime.Now;
                exeptionString += $"\aTime:{time.ToLongTimeString()}\n";
                exeptionString += exeption.ToString();
                return exeptionString;
            }
        }
        public class Item_not_found_exception : Exception
        {
            IDAL.DO.Item_not_found_exception exeption { get; set; }
            public string type { get; set; }
            public uint key { get; set; }

            public Item_not_found_exception(string type, uint unic_key)
            {
                this.type = type;
                key = unic_key;
            }
            public Item_not_found_exception(IDAL.DO.Item_not_found_exception ex) { exeption = ex; }
            public override string ToString()
            {
                string exeptionString = "";
                DateTime time = DateTime.Now;
                exeptionString += $"\aTime:{time.ToLongTimeString()}\n";
                exeptionString += exeption.ToString();
                return exeptionString;
            }
        }
        public class NoPlaceForChargeException:Exception
        {
            uint base_ { get; set; }
            public NoPlaceForChargeException(uint base_)
            { this.base_ = base_; }
            public override string ToString()
            {
                return $"Time:{DateTime.Now} \nIn this base number:{base_}\nno place for drone! \n" +
                    $"plase chack the chrging drone list , and relese drone whit full buttry. ";
            }
        }
    }
}
