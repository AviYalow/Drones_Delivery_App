using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL : IBL
    {
        public void AddDrone(Drone drone, uint base_)
        {
            try
            {

                drone.location = Base_location(base_);

            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
            }
            drone.droneStatus = Drone_status.Maintenance;

            Random random = new Random();
            try
            {
                dalObj.AddDrone(drone.SerialNum, drone.Model, (uint)drone.weightCategory);
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
            drone.butrryStatus = random.Next(20, 41);
            DroneToCharge(drone.SerialNum, base_);
            drones.Add(drone);

        }

        public void UpdateDronelocation(uint drone, Location location)
        {
            int i = drones.FindIndex(x => x.SerialNum == drone);
            if (i == -1)
                throw (new ItemNotFoundException("Drone", drone));
            drones[i].location = location;

        }

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
            int i = drones.FindIndex(x => x.SerialNum == droneId);
            if (i == -1)
                throw new ItemNotFoundException("Drone", droneId);
            drones[i].Model = newName;
            droneInData.Model = newName;
            dalObj.UpdateDrone(droneInData);
        }

    }
}
