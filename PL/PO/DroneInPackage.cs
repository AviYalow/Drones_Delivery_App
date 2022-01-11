using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Drone In Package
    /// </summary>
    public class DroneInPackage : BO.DroneInPackage , INotifyPropertyChanged
    {
        public uint SerialNum { get; set; }
        public double ButrryStatus { get; set; }
        public Location Location { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number: {SerialNum},\n";
            print += $"Butrry Status: {ButrryStatus}\n";
            print += Location;
            return print;

        }
    }
}
