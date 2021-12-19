using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;


namespace DalApi
{

     partial class DalObject : DalApi.IDal
    {
        
        /// <summary>
        ///Creating entities with initial initialization
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }

       
        /// <summary>
        /// show the distance between 2 locations
        /// </summary>
        /// <param name="Longitude1">the first longitude location</param>
        /// <param name="Latitude1"> the first latitude location</param>
        /// <param name="Longitude2">the second longitude location</param>
        /// <param name="Latitude2">the second latitude location</param>
        /// <returns>distance between 2 locations</returns>
        public double Distance(double Longitude1, double Latitude1, double Longitude2, double Latitude2)
        {
            return DO.Point.Distance(Longitude1, Latitude1, Longitude2, Latitude2);
        }


        /// <summary>
        /// Returns a point in the form of degrees
        /// </summary>
        /// <param name="point"></param>

        public string PointToDegree(double point)
        {
            return Point.Degree(point);
        }

        /// <summary>
        /// return list of charging drones
        /// </summary>
        /// <returns>return list of charging drones</returns>
        public IEnumerable<BatteryLoad> ChargingDroneList()
        {

            return DataSource.droneInCharge.ToList();
        }

    }
}
