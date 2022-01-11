using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Drone
    /// </summary>
    public class Drone: BO.Drone, INotifyPropertyChanged
    {
        public uint SerialNumber { get; init; }
        public DroneModel Model { get; set; }

        public WeightCategories WeightCategory { get; set; }
        public PackageInTransfer PackageInTransfer { get; set; }

        public double ButrryStatus { get; set; }
        public DroneStatus DroneStatus { get; set; }
        public Location Location { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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
