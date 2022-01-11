using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO 
{
    /// <summary>
    /// Drone To List
    /// </summary>
    public class DroneToList: BO.DroneToList, INotifyPropertyChanged
    {
        
        public uint SerialNumber { get; set; }
        public DroneModel Model { get; set; }
        public WeightCategories WeightCategory { get; set; }

        public double? ButrryStatus { get; set; }
        public DroneStatus DroneStatus { get; set; }
        public Location Location { get; set; }
        public uint NumPackage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number: {SerialNumber},\n";
            print += $"model: {Model},\n";
            print += $"Weight Category: {WeightCategory},\n";
            print += $" Butrry status: {ButrryStatus},\n";
            print += $" Drone status: {DroneStatus},\n";
            print += Location;
            print += $"Number Package: {NumPackage}\n";

            return print;
        }


    }
}
