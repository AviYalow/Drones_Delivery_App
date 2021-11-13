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
        public void AddDrone(Drone drone,int base_)
        {
            try
            {
                
                drone.location = Base_location(base_);
                
            }
            catch(IDAL.DO.Item_not_found_exception ex)
            {
                throw (new Item_not_found_exeption(ex));
            }
            drone.droneStatus = Drone_status.Maintenance;

            Random random = new Random();
            try
            {
                dalObj.Add_drone(drone.SerialNum, drone.Model, (int)drone.weightCategory);
            }
            catch(IDAL.DO.Item_found_exception ex)
            {
                throw (new Item_found_exeption(ex));
            }
            drone.butrryStatus = random.Next(20, 41);
            DroneToCharge(drone.SerialNum, base_);
            drones.Add(drone);

        }

    }
}
