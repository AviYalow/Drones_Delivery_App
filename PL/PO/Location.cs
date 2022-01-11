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
        public double Longitude { get; set; }
        public double Latitude { get; set; }

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
