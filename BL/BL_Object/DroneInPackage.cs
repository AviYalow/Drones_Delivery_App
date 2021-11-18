using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInPackage
    {
        public uint SerialNum { get; set; }
        public double butrryStatus { get; set; }
        public Location location { get; set; }

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number: {SerialNum},\n";
            print += $"Butrry Status: {butrryStatus}\n";
            print += $"Location: Latitude-{location.Latitude} Longitude-{location.Longitude}\n";
            return print;

        }
    }
}
