using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        class Point
        {
           public static string Degree(double point)
            {
                uint d = (uint)point;
                uint dd = (uint)((point-d) * 60);
                double s = (point - d - dd / 60) * 3600;
                return $"{d}\x00B0 {dd}' {s}\"";
            }
            public static double Distans(double Longitude1, double Latitude1, double Longitude2, double Latitude2)
            {
               

                return Math.Sqrt((Longitude1 - Longitude2) * (Longitude1 - Longitude2) + (Latitude1 - Latitude2) * (Latitude1 - Latitude2));
            }
        }
    }
}
