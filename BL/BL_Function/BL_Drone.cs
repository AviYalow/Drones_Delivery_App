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
        public void AddDrone(DroneToList drone, uint base_)
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
                dalObj.AddDrone(new IDAL.DO.Drone { SerialNumber = drone.SerialNumber, Model = drone.Model, WeightCategory = (IDAL.DO.WeightCategories)drone.weightCategory });
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
            drone.butrryStatus = random.Next(20, 41);

            
            dronesListInBl.Add(drone);
            dalObj.DroneToCharge(drone.SerialNumber, base_);

        }
        /// <summary>
        /// update new location for drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="location"></param>
        public void UpdateDronelocation(uint drone, Location location)
        {

            int i = dronesListInBl.FindIndex(x => x.SerialNumber == drone);
            if (i == -1)
                throw (new ItemNotFoundException("Drone", drone));
            dronesListInBl[i].location = location;

        }
        /// <summary>
        /// update new model for drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="newName"></param>
        public void UpdateDroneName(uint droneId, string newName)
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
            int i = dronesListInBl.FindIndex(x => x.SerialNumber == droneId);
            if (i == -1)
                throw new ItemNotFoundException("Drone", droneId);
            dronesListInBl[i].Model = newName;
            droneInData.Model = newName;
            dalObj.UpdateDrone(droneInData);
        }

        public DroneToList SpecificDrone(uint siralNuber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == siralNuber);
            if (drone is null)
                throw new ItemNotFoundException("drone", siralNuber);
            return drone;

        }

        public IEnumerable<DroneToList> DroneToLists()
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            return dronesListInBl.ToList();
        }

        DroneToList droneFromDal(IDAL.DO.Drone drone)
        {
            return new DroneToList { SerialNumber = drone.SerialNumber, Model = drone.Model, weightCategory = (WeightCategories)drone.WeightCategory };
        }

        public Drone GetDrone(uint droneNum)
        {

            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNum);
            if (drone is null)
                throw new ItemNotFoundException("Drone", droneNum);
            try
            {
                var pacege = drone.numPackage != 0 ? convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(drone.numPackage)) : null;
                return new Drone
                {
                    SerialNum = drone.SerialNumber,
                    Model = drone.Model,
                    weightCategory = drone.weightCategory,
                    droneStatus = drone.droneStatus,
                    location = drone.location,
                    butrryStatus = drone.butrryStatus,
                    packageInTransfer = pacege
                };
            }
            catch(IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }




        }
        /// <summary>
        /// delete drone 
        /// </summary>
        /// <param name="droneNum"></param>
        public void DeleteDrone(uint droneNum)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNum);
            if (drone is null)
                throw new ItemNotFoundException("Drone", droneNum);
            //chack the drone not in middel of delivery
            if (drone.droneStatus == DroneStatus.Work&&dalObj.packegeByNumber(drone.numPackage).CollectPackageForShipment!=new DateTime())
                throw new DroneStillAtWorkException();

                dalObj.DeleteDrone(droneNum);
            
            dronesListInBl.Remove(drone);
            

        }

        DroneInPackage droneToDroneInPackage(uint number)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == number);
            if (drone is null)
                throw new ItemNotFoundException("Drone", number);
            return new DroneInPackage { SerialNum = drone.SerialNumber, butrryStatus = drone.butrryStatus, location = drone.location };
        }

    }
}
