using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Drone In Charge
    /// </summary>
    public class DroneInCharge: BO.DroneInCharge, INotifyPropertyChanged
    {
        public uint SerialNum
        {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number: {SerialNum},\n";
            print += $"Butrry Status: {ButrryStatus}\n";
            return print;
        }

    }
}
