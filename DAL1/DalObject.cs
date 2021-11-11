using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL;
using IDAL.DO;


namespace DalObject
{

    public partial class DalObject : IDAL.IDal
    {

        /// <summary>
        ///Creating entities with initial initialization
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }
        public double Distance(double Longitude1, double Latitude1, double Longitude2, double Latitude2)
        {
            return IDAL.DO.Point.Distance(Longitude1, Latitude1, Longitude2, Latitude2);
        }

        
        /// <summary>
        /// Returns a point in the form of degrees
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public string Point_to_degree(double point)
        {
            return Point.Degree(point);
        }

    }
}
