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
        public uint SerialNum {
            get
            {
                return base.SerialNum;
            }
            set
            {
                base.SerialNum = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SerialNum"));
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
        public Location Location {
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
            print += $"Siral Number: {SerialNum},\n";
            print += $"Butrry Status: {ButrryStatus}\n";
            print += Location;
            return print;

        }
    }
}
