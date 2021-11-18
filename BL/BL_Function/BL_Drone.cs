using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// add drone to list
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="base_"></param>
        public void AddDrone(Drone drone, uint base_)
        {
            try
            {

                drone.location = BaseLocation(base_);

            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
            }
            drone.droneStatus = DroneStatus.Maintenance;

            Random random = new Random();
            try
            {
                dalObj.AddDrone(new IDAL.DO.Drone {SerialNumber= drone.SerialNum,Model= drone.Model,WeightCategory=(IDAL.DO.WeightCategories)drone.weightCategory });
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
            drone.butrryStatus = random.Next(20, 41);
           
            DroneToCharge(drone.SerialNum);
            dronesListInBl.Add(drone);

        }
        /// <summary>
        /// update new location for drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="location"></param>
        public void UpdateDronelocation(  uint drone, Location location)
        {
            
            int i = dronesListInBl.FindIndex(x => x.SerialNum == drone);
            if (i == -1)
                throw (new ItemNotFoundException("Drone", drone));
            dronesListInBl[i].location = location;
            
        }
        /// <summary>
        /// update new model for drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="newName"></param>
        public void UpdateDroneName( uint droneId, string newName)
        {
            IDAL.DO.Drone droneInData;
            try
            {

                droneInData = dalObj.DroneByNumber(droneId);
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
            int i = dronesListInBl.FindIndex(x => x.SerialNum == droneId);
            if (i == -1)
                throw new ItemNotFoundException("Drone", droneId);
            dronesListInBl[i].Model = newName;
            droneInData.Model = newName;
            dalObj.UpdateDrone(droneInData);
        }

        public Drone SpecificDrone(uint siralNuber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNum == siralNuber);
            if (drone is null)
                throw new ItemNotFoundException("drone", siralNuber);
            return drone;

        }

        public IEnumerable<DroneToList> DroneToLists()
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();
            List<DroneToList> droneToLists = new List<DroneToList>();
            foreach (var drone in dronesListInBl)
            {
                droneToLists.Add(new DroneToList
                {
                    butrryStatus = drone.butrryStatus,
                    droneStatus = drone.droneStatus,
                    location = drone.location,
                    Model = drone.Model,
                    numPackage = drone.packageInTransfer.SerialNumber,
                    SerialNumber = drone.SerialNum,
                    weightCategory = drone.weightCategory


                });
            }
            return droneToLists;
        }

        Drone droneFromDal(IDAL.DO.Drone drone)
        {
            return new Drone { SerialNum = drone.SerialNumber, Model = drone.Model, weightCategory = (WeightCategories)drone.WeightCategory };
        }

    }
}
