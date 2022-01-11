using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Location
    /// </summary>
    public class Location:BO.Location, INotifyPropertyChanged
    {
        public double Longitude
        {
            get
            {
                return base.Longitude;
            }
            set
            {
                base.Longitude = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));
                }
            }
        }
        public double Latitude {
            get
            {
                return base.Latitude;
            }
            set
            {
                base.Latitude = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {

            string print = "";
            print += $"{DO.Point.Degree(Latitude)} ";
            print += Latitude >= 0 ? "N \n" : "S \n";
            print += $"{DO.Point.Degree(Longitude)} ";
            print += Longitude >= 0 ? "E " : "W ";
            return print;
        }
        
           

    }
}
