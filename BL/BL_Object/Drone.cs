using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Drone
    /// </summary>
    public class Drone
    {
        public uint SerialNumber { get; init; }
        public DroneModel Model { get; set; }

        public WeightCategories WeightCategory { get; set; }
        public PackageInTransfer PackageInTransfer { get; set; }

        public double ButrryStatus { get; set; }
        public DroneStatus DroneStatus { get; set; }
        public Location Location { get; set; }
        public override string ToString()
        {
            String print = "";
            print += $"Serial Number: {SerialNumber},\n";
            print += $"model: {Model},\n";
            print+= $"Weight Category: {WeightCategory},\n";
            print += $"Package in transfer:\n";
            print +=(PackageInTransfer != null)?  $" {PackageInTransfer,3}":'0';
            print += $"Butrry status: {ButrryStatus},\n";
            print += $"Drone status: {DroneStatus},\n";
            print += $"Location:\n{Location}\n";
           
            return print;
        }

    }
}
