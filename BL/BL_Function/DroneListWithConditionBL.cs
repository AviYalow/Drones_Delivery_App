using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IBL.BO;

using DalApi;
namespace IBL
{
    public partial class BL : IBL
    {
        Func<DroneToList, bool> selectByStatus = null;
        Func<DroneToList, bool> selectByWeihgt = null;
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
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            //return dronesListInBl.FindAll(x => x.WeightCategory >=package.weightCatgory&&x.DroneStatus==DroneStatus.Free&&x.ButrryStatus>batteryCalculationForFullShipping(x.Location,package));
            return from x in dronesListInBl
                   where x.WeightCategory >= package.weightCatgory && x.DroneStatus == DroneStatus.Free && x.ButrryStatus > batteryCalculationForFullShipping(x.Location, package)
                   select new DroneToList { ButrryStatus = x.ButrryStatus, DroneStatus = x.DroneStatus, Location = x.Location, Model = x.Model, NumPackage = x.NumPackage, SerialNumber = x.SerialNumber, WeightCategory = x.WeightCategory };
        }
        /// <summary>
        /// sort list by spscific parameter
        /// </summary>
        /// <param name="obj">ordenry list by this parameter parameter </param>
        /// <param name="drones">ordener this list </param>
        /// <returns>ordenry list</returns>
        public IEnumerable<DroneToList> DroneSortList(string obj, IEnumerable<DroneToList> drones = null)
        {
            DroneToList drone = new DroneToList();
            int i = 0;
            foreach (var type in drone.GetType().GetProperties())
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
        /// <summary>
        /// return filter list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneToList> FilterDronesList()
        {
            var drones = DroneToLists();
            if(droneToListFilter!=null&&droneToListFilter.GetInvocationList()!=null)
            foreach (Func<DroneToList, bool> prdict in droneToListFilter.GetInvocationList())
            {
                drones = drones.Where(prdict);
            }
            return drones;
        }
    }
}
