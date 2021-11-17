using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStationToList
    {
        public uint SerialNum { get; init; }
        public string Name { get; set; }
        public uint FreeState { get; set; }
        public uint BusyState { get; set; }

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number is {SerialNum},\n";
            print += $"The Name is {Name},\n";
            print += $"Number of free state: {FreeState},\n";
            print += $"Number of busy state: {FreeState}\n";
            return print;
        }
    }
}
