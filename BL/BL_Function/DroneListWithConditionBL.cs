using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using System.Reflection;

namespace BlApi
{
    partial class BL : IBL
    {
        Func<DroneToList, bool> selectByStatus = null;
        Func<DroneToList, bool> selectByWeihgt = null;
        Func<DroneToList, bool> selectByPackege = null;
        Func<DroneToList, bool> selectBynumber = null;
        /// <summary>
        /// return list of drones
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToLists()
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            return from drone in dronesListInBl
                   where drone.DroneStatus != DroneStatus.Delete
                   select drone.Clone();

        }

        /// <summary>
        /// return list of drones by spsific status
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListsByStatus(DroneStatus? droneStatus = null)
        {

            droneToListFilter -= selectByStatus;
            selectByStatus = x => x.DroneStatus == droneStatus;
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();
            if (droneStatus != null)
                droneToListFilter += selectByStatus;

            return FilterDronesList();

        }

        /// <summary>
        /// return list of drones by maximum weight
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListsByWhight(WeightCategories? weight = null)
        {
            droneToListFilter -= selectByWeihgt;
            selectByWeihgt = x => x.WeightCategory >= weight;
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();
            if (weight != null)
                droneToListFilter += selectByWeihgt;

            return FilterDronesList();
        }

        /// <summary>
        /// return list of drones by they can make delivery for packege
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListPasibalForPackege(Package package)
        {
            droneToListFilter -= selectByPackege;
            selectByPackege = x => x.WeightCategory >= package.WeightCatgory &&
            x.DroneStatus == DroneStatus.Free &&
            x.ButrryStatus > batteryCalculationForFullShipping(x.Location, package);
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();
            if (package != null)
                droneToListFilter += selectByPackege;

            return FilterDronesList();

        }
        public IEnumerable<DroneToList> DroneToListFilterByNumber(string num)
        {
            droneToListFilter -= selectBynumber;
            selectBynumber = x => x.SerialNumber.ToString().StartsWith(num); 
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();
            if (num!="")
                droneToListFilter += selectBynumber;

            return FilterDronesList();

        }

        /// <summary>
        /// return filter list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneToList> FilterDronesList()
        {
         
            return filerList(DroneToLists(), droneToListFilter);
        }

       
    }
}
