using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList
    {
        public uint SerialNumber { get; init; }
        public string Model { get; set; }
        public WeightCategories weightCategory { get; set; }

        public double butrryStatus { get; set; }
        public DroneStatus droneStatus { get; set; }
        public Location location { get; set; }
        public uint numPackage { get; set; }

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number: {SerialNumber},\n";
            print += $"model: {Model},\n";
            print += $"Weight Category: {weightCategory},\n";
            print += $" Butrry status: {butrryStatus},\n";
            print += $" Drone status: {droneStatus},\n";
            print += $"Location: Latitude:{location.Latitude} Longitude:{location.Longitude},\n";
            print += $"Number Package: {numPackage}\n";

            return print;
        }


    }
}
