using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DO
    {
        class Point
        {
              internal string Degree (double point)
            {
                uint d = (uint)point;
                uint dd = (uint)((point-d) * 60);
                double s = (point - d - dd / 60) * 3600;
                return $"{d}\x00B0 {dd}' {s}\"";
            }
        }
    }
}
