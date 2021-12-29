using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DalApi;

namespace BlApi
{
    partial class BL : IBL
    {
        /// <summary>
        /// add drone to list
        /// </summary>
        /// <param name="drone"> drone to add</param>
        /// <param name="base_"> serial number of base station for first chraging</param>
        public void AddDrone(DroneToList drone, uint base_)
        {
            try
            {

                drone.Location = BaseLocation(base_);
                if (drone.WeightCategory > WeightCategories.Heavy)
                    throw new InputErrorException();

            }
            catch (DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
            }
            drone.DroneStatus = DroneStatus.Maintenance;

            Random random = new Random();
            try
            {
                dalObj.AddDrone(new DO.Drone { SerialNumber = drone.SerialNumber, Model =(DO.DroneModel) drone.Model, WeightCategory = (DO.WeightCategories)drone.WeightCategory });
            }
            catch (DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
            drone.ButrryStatus = random.Next(20, 41);

            
                dronesListInBl.Add(drone);
            
            dalObj.DroneToCharge(drone.SerialNumber, base_);

        }

        /// <summary>
        /// update new location for drone
        /// </summary>
        /// <param name="drone"> serial number of drone</param>
        /// <param name="location"> new location</param>
        public void UpdateDronelocation(uint drone, Location location)
        {

            int i = dronesListInBl.FindIndex(x => x.SerialNumber == drone&&x.DroneStatus!=DroneStatus.Delete);
            if (i == -1)
                throw (new ItemNotFoundException("Drone", drone));
            dronesListInBl[i].Location = location.Clone();

        }

        /// <summary>
        /// update new model for drone
        /// </summary>
        /// <param name="droneId"> serial number of the drone</param>
        /// <param name="newName"> new name to change</param>
        public void UpdateDroneName(uint droneId, DroneModel newName)
        {
            DO.Drone droneInData;
            try
            {

                droneInData = dalObj.DroneByNumber(droneId);
            }
            catch (DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
            int i = dronesListInBl.FindIndex(x => x.SerialNumber == droneId&&x.DroneStatus != DroneStatus.Delete);
            if (i == -1)
                throw new ItemNotFoundException("Drone", droneId);
            dronesListInBl[i].Model = newName;
            droneInData.Model = newName.covertDroneModelBlToDal();
            dalObj.UpdateDrone(droneInData);
        }

        /// <summary>
        /// search a drone by serial number
        /// </summary>
        /// <param name="droneNum"> serial number of the drone</param>
        /// <returns> drone founded</returns>
        public Drone GetDrone(uint droneNum)
        {

            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNum);
            if (drone is null)
                throw new ItemNotFoundException("Drone", droneNum);
            try
            {
                var pacege = drone.NumPackage != 0 ?  convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(drone.NumPackage)) : null;
                return new Drone
                {
                    SerialNumber = drone.SerialNumber,
                    Model = drone.Model,
                    WeightCategory = drone.WeightCategory,
                    DroneStatus = drone.DroneStatus,
                    Location = drone.Location,
                    ButrryStatus = drone.ButrryStatus,
                    PackageInTransfer = pacege
                };
            }
            catch (DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }


        }

        /// <summary>
        /// delete drone 
        /// </summary>
        /// <param name="droneNum"> serial number of the drone</param>
        public void DeleteDrone(uint droneNum)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNum && x.DroneStatus != DroneStatus.Delete);
            if (drone is null)
                throw new ItemNotFoundException("Drone", droneNum);
            //chack the drone not in middel of delivery
            if (drone.DroneStatus == DroneStatus.Work && !(dalObj.packegeByNumber(drone.NumPackage).CollectPackageForShipment is null))
                throw new DroneStillAtWorkException();

                dalObj.DeleteDrone(droneNum);

            for (int i = 0; i < dronesListInBl.Count; i++)
            {
                if(dronesListInBl[i].SerialNumber==droneNum)
                {
                    drone.DroneStatus = DroneStatus.Delete;
                    dronesListInBl[i] = drone;
                }
            }
            

        }

    

    }
}
