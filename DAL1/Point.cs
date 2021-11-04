using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        /// <Point>
        /// A class that represents points on a sexagesimal 
        /// of the coordinate values
        /// </Point>
       public class Point
        {
          public  double longitude { get; set; }
           public double latitude { get; set; }

            //Conversion of points in decimal base to base sexagesimal
            public static string Degree(double point)
            {
                point = (point < 0) ? point * (-1) : point;
                uint d = (uint)point;
                uint m = (uint)((point - d) * 60);
                double mph = (double)((double)m / 60);
                double s = (point - d - mph) * 3600;
                return $"{d}\x00B0 {m}' {s}\"";
            }

            //A function that calculates distance at sea given two points
            public static double Distance(Point point1,Point point2)
            {

                return ((Math.Sqrt((point1.longitude - point2. longitude) * (point1.longitude - point2.longitude) + (point1.latitude - point2.latitude) * point1.latitude - point2.latitude)) * 100);
            }
            public static double Distance(double longitude1, double longitude2,double latitude1, double latitude2)
            {

                return ((Math.Sqrt((longitude1 - longitude2) * (longitude1 - longitude2) 
                    + (latitude1 - latitude2) * latitude1 - latitude2)) * 100);
            }
        }
    }
}
