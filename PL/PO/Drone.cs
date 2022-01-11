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
        public uint SerialNumber {
            get
            {
                return base.SerialNumber;
            }
            init
            {
                base.SerialNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SerialNumber"));
                }
            }
        }
        public DroneModel Model
        {
            get
            {
                return (DroneModel)base.Model;
            }
            set
            {
                base.Model = (BO.DroneModel)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
                }
            }
        }

        public WeightCategories WeightCategory
        {
            get
            {
                return (WeightCategories)base.WeightCategory;
            }
            set
            {
                base.WeightCategory = (BO.WeightCategories)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WeightCategory"));
                }
            }
        }
        public PackageInTransfer PackageInTransfer
        {
            get
            {
                return (PackageInTransfer)base.PackageInTransfer;
            }
            set
            {
                base.PackageInTransfer = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PackageInTransfer"));
                }
            }
        }

        public double ButrryStatus {
            get
            {
                return base.ButrryStatus;
            }
            set
            {
                base.ButrryStatus = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ButrryStatus"));
                }
            }
        }
        public DroneStatus DroneStatus {
            get
            {
                return (DroneStatus)base.DroneStatus;
            }
            set
            {
                base.DroneStatus = (BO.DroneStatus)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DroneStatus"));
                }
            }
        }
        public Location Location
        {
            get
            {
                return (Location)base.Location;
            }
            set
            {
                base.Location = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Location"));
                }
            }
        }

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
