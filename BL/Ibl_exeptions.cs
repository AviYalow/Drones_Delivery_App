using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

        public class ItemFoundExeption : Exception
        {
            IDAL.DO.ItemFoundException exeption { get; set; }
            public ItemFoundExeption(IDAL.DO.ItemFoundException ex) { exeption = ex; }
            public override string ToString()
            {
                string exeptionString = "";
                DateTime time = DateTime.Now;
                exeptionString += $"\aTime:{time.ToLongTimeString()}\n";
                exeptionString += exeption.ToString();
                return exeptionString;
            }
        }
        public class ItemNotFoundException : Exception
        {
            IDAL.DO.ItemNotFoundException exeption { get; set; }
            public string type { get; set; }
            public uint key { get; set; }

            public ItemNotFoundException(string type, uint unic_key):base(type)
            {
                this.type = type;
                key = unic_key;
            }
            public ItemNotFoundException(IDAL.DO.ItemNotFoundException ex):base(ex.Message,ex) { exeption = ex; }
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
