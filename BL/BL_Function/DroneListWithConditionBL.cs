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

            return dronesListInBl.FindAll(x=>x.droneStatus!=DroneStatus.Delete);
        }
        /// <summary>
        /// return list of drones by spsific status
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListsByStatus(DroneStatus droneStatus)
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            return dronesListInBl.FindAll(x=>x.droneStatus==droneStatus);
        }

        /// <summary>
        /// return list of drones by maximum weight
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListsByWhight(WeightCategories weight)
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            return dronesListInBl.FindAll(x => x.weightCategory <=weight);
        }

        /// <summary>
        /// return list of drones by they can make delivery for packege
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToListPasibalForPackege(Package package)
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();
           
            return dronesListInBl.FindAll(x => x.weightCategory >=package.weightCatgory&&x.droneStatus==DroneStatus.Free&&x.butrryStatus>batteryCalculationForFullShipping(x.location,package));
        }
    }
}
