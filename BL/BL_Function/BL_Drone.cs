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
        /// <param name="drone"> drone to add</param>
        /// <param name="base_"> serial number of base station for first chraging</param>
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

            int i = dronesListInBl.FindIndex(x => x.SerialNumber == drone.SerialNumber && x.droneStatus != DroneStatus.Delete);
            if (i != -1)
                dronesListInBl.Add(drone);
            else
                dronesListInBl[i] = drone;
            dalObj.DroneToCharge(drone.SerialNumber, base_);

        }

        /// <summary>
        /// update new location for drone
        /// </summary>
        /// <param name="drone"> serial number of drone</param>
        /// <param name="location"> new location</param>
        public void UpdateDronelocation(uint drone, Location location)
        {

            int i = dronesListInBl.FindIndex(x => x.SerialNumber == drone&&x.droneStatus!=DroneStatus.Delete);
            if (i == -1)
                throw (new ItemNotFoundException("Drone", drone));
            dronesListInBl[i].location = location;

        }

        /// <summary>
        /// update new model for drone
        /// </summary>
        /// <param name="droneId"> serial number of the drone</param>
        /// <param name="newName"> new name to change</param>
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
            int i = dronesListInBl.FindIndex(x => x.SerialNumber == droneId&&x.droneStatus != DroneStatus.Delete);
            if (i == -1)
                throw new ItemNotFoundException("Drone", droneId);
            dronesListInBl[i].Model = newName;
            droneInData.Model = newName;
            dalObj.UpdateDrone(droneInData);
        }


        /// <summary>
        /// find specific drone in the list of the drones
        /// </summary>
        /// <param name="siralNuber"> serial number of the drone</param>
        /// <returns> drone founded </returns>
        public DroneToList SpecificDrone(uint siralNuber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == siralNuber&&x.droneStatus != DroneStatus.Delete);
            if (drone is null)
                throw new ItemNotFoundException("drone", siralNuber);
            return drone;

        }

        /// <summary>
        /// return list of drones
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToLists()
        {
            if (dronesListInBl.Count == 0)
                throw new TheListIsEmptyException();

            return dronesListInBl.ToList().FindAll(x=>x.droneStatus!=DroneStatus.Delete);
        }

        /// <summary>
        /// drone request from the data layer
        /// </summary>
        /// <param name="drone"> serial number of the  drone</param>
        /// <returns> drone </returns>
        DroneToList droneFromDal(IDAL.DO.Drone drone)
        {
            return new DroneToList { SerialNumber = drone.SerialNumber, Model = drone.Model, weightCategory = (WeightCategories)drone.WeightCategory };
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
            catch (IDAL.DO.ItemNotFoundException ex)
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
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNum && x.droneStatus != DroneStatus.Delete);
            if (drone is null)
                throw new ItemNotFoundException("Drone", droneNum);
            //chack the drone not in middel of delivery
            if (drone.droneStatus == DroneStatus.Work && dalObj.packegeByNumber(drone.numPackage).CollectPackageForShipment != new DateTime())
                throw new DroneStillAtWorkException();

                dalObj.DeleteDrone(droneNum);

            for (int i = 0; i < dronesListInBl.Count; i++)
            {
                if(dronesListInBl[i].SerialNumber==droneNum)
                {
                    drone.droneStatus = DroneStatus.Delete;
                    dronesListInBl[i] = drone;
                }
            }
            

        }

        /// <summary>
        /// search drone in package
        /// </summary>
        /// <param name="number"> serial number of the drone </param>
        /// <returns>the founded drone</returns>
        DroneInPackage droneToDroneInPackage(uint number)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == number);
            if (drone is null)
                throw new ItemNotFoundException("Drone", number);
            return new DroneInPackage { SerialNum = drone.SerialNumber, butrryStatus = drone.butrryStatus, location = drone.location };
        }

    }
}
