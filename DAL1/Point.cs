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
            public static  double Distance(Point point1,Point point2)
            {

                return (getDistanceFromLatLonInKm(point1.latitude,point1.longitude,point2.latitude,point2.longitude));
            }
            public static  double Distance(double longitude1, double longitude2,double latitude1, double latitude2)
            {

                return (getDistanceFromLatLonInKm(latitude1,longitude1,latitude2,longitude2));
            }

            public static double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
            {
                var R = 6371; // Radius of the earth in km
                var dLat = deg2rad(lat2 - lat1);

                var dLon = deg2rad(lon2 - lon1);
                var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                return R * c; // Distance in km return d;
            }
            public static double deg2rad(double deg)
            { return deg * (Math.PI / 180); }
        }
    }
}
