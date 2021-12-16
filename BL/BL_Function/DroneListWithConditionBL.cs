using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// return list of drones
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToLists()
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            return dronesListInBl.FindAll(x => x.DroneStatus != DroneStatus.Delete);
        }
        /// <summary>
        /// return list of drones by spsific status
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListsByStatus(DroneStatus droneStatus)
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            return dronesListInBl.FindAll(x => x.DroneStatus == droneStatus);
        }

        /// <summary>
        /// return list of drones by maximum weight
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListsByWhight(WeightCategories weight)
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            return dronesListInBl.FindAll(x => x.WeightCategory <= weight);
        }

        /// <summary>
        /// return list of drones by they can make delivery for packege
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListPasibalForPackege(Package package)
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            //return dronesListInBl.FindAll(x => x.WeightCategory >=package.weightCatgory&&x.DroneStatus==DroneStatus.Free&&x.ButrryStatus>batteryCalculationForFullShipping(x.Location,package));
            return from x in dronesListInBl
                   where x.WeightCategory >= package.weightCatgory && x.DroneStatus == DroneStatus.Free && x.ButrryStatus > batteryCalculationForFullShipping(x.Location, package)
                   select new DroneToList { ButrryStatus = x.ButrryStatus, DroneStatus = x.DroneStatus, Location = x.Location, Model = x.Model, NumPackage = x.NumPackage, SerialNumber = x.SerialNumber, WeightCategory = x.WeightCategory };
        }

        public IEnumerable<DroneToList> DroneSortListBySiral(string obj,IEnumerable<DroneToList> drones = null)
        {
            DroneToList drone = new DroneToList();
            int i = 0;
            foreach(var type in drone.GetType().GetProperties())
            {
                if (type.Name != obj)
                    i++;
                else
                    break;

            }
            {
                if (drones is null)
                    return from x in dronesListInBl
                           orderby x.GetType().GetProperties()[i].GetValue(x)
                           select x;
                return from x in drones
                       orderby x.GetType().GetProperties()[i].GetValue(x)
                       select x;
            }
           
        }
        public IEnumerable<DroneToList> DroneSortListByModel(IEnumerable<DroneToList> drones = null)
        {

            {
                if (drones is null)
                    return from x in dronesListInBl
                           orderby x.Model
                           select x;
                return from x in drones
                       orderby x.Model
                       select x;
            }

        }
        public IEnumerable<DroneToList> DroneSortListByWeight(IEnumerable<DroneToList> drones = null)
        {

            {
                if (drones is null)
                    return from x in dronesListInBl
                           orderby x.WeightCategory
                           select x;
                return from x in drones
                       orderby x.WeightCategory
                       select x;
            }

        }
        public IEnumerable<DroneToList> DroneSortListByStatus(IEnumerable<DroneToList> drones = null)
        {

            {
                if (drones is null)
                    return from x in dronesListInBl
                           orderby x.DroneStatus
                           select x;
                return from x in drones
                       orderby x.DroneStatus
                       select x;
            }

        }
        public IEnumerable<DroneToList> DroneSortListByButtry(IEnumerable<DroneToList> drones = null)
        {

            {
                if (drones is null)
                    return from x in dronesListInBl
                           orderby x.ButrryStatus
                           select x;
                return from x in drones
                       orderby x.ButrryStatus
                       select x;
            }

        }
    }
}
