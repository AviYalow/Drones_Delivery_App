using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    /// <summary>
    /// Location
    /// </summary>
    public class Location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override string ToString()
        {

            string print = "";
            print += $"Location: Latitude:{IDAL.DO.Point.Degree( Latitude)} Longitude:{IDAL.DO.Point.Degree(Longitude)}\n";
            return print;
        }
        
           

    }
}
