using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    /// <summary>
    /// Drone In Charge
    /// </summary>
    public class DroneInCharge
    {
        public uint SerialNum { get; set; }
        public double butrryStatus { get; set; }

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number: {SerialNum},\n";
            print += $"Butrry Status: {butrryStatus}\n";
            return print;
        }

    }
}
