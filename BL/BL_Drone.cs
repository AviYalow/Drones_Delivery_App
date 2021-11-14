using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL:IBL
    {
        public void AddDrone(Drone drone,uint base_)
        {
            try
            {
                
                drone.location = Base_location(base_);
                
            }
            catch(IDAL.DO.Item_not_found_exception ex)
            {
                throw (new Item_not_found_exception(ex));
            }
            drone.droneStatus = Drone_status.Maintenance;

            Random random = new Random();
            try
            {
                dalObj.Add_drone(drone.SerialNum, drone.Model, (uint)drone.weightCategory);
            }
            catch(IDAL.DO.Item_found_exception ex)
            {
                throw (new Item_found_exeption(ex));
            }
            drone.butrryStatus = random.Next(20, 41);
            DroneToCharge(drone.SerialNum, base_);
            drones.Add(drone);

        }

        public void updateDronelocation(uint drone ,Location location)
        {
            int i = drones.FindIndex(x => x.SerialNum == drone);
            if (i == -1)
                throw (new Item_not_found_exception("Drone", drone));
            drones[i].location = location;

        }

    }
}
