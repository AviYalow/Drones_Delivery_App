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
        /// <summary>
        /// clculate how match butrry drone need for dleviry point to point
        /// </summary>
        /// <param name="packegeNumber"></param>
        /// <returns></returns>
        double buttryDownPackegeDelivery(int packegeNumber)
        {
            double[] elctricity = dalObj.Elctrtricity();
            double battery_drop = 0, distans = 0;

            IDAL.DO.Package package = dalObj.packege_by_number(packegeNumber);
            Location sending_location = Client_location(package.sendClient), geting_location = Client_location(package.getingClient);

            distans = Distans(geting_location, sending_location);

            switch ((Weight_categories)package.weightCatgory)//להשלים פונקציה ע"י חישוב לכל משקל
            {
                case Weight_categories.Easy:
                    battery_drop = ((distans / (double)Speed_drone.Easy) / (double)butrryPer__.Minute);
                    battery_drop *= elctricity[1];
                    break;
                case Weight_categories.Medium:
                    battery_drop = ((distans / (double)Speed_drone.Easy) / (double)butrryPer__.Minute);
                    battery_drop *= elctricity[2];
                    break;
                case Weight_categories.Heavy:
                    battery_drop = ((distans / (double)Speed_drone.Easy) / (double)butrryPer__.Minute);
                    battery_drop *= elctricity[3];
                    break;
                default:
                    break;

            }
            return battery_drop;
        }

        /// <summary>
        /// clculate how match butrry drone need for to get from base to client or client to base
        /// </summary>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        double buttryDownWithNoPackege(Location fromLocation, Location toLocation)
        {
            double[] elctricity = dalObj.Elctrtricity();
            double distans = Distans(fromLocation, toLocation);
            double buttry = ((distans / (double)Speed_drone.Free) / (double)butrryPer__.Minute) * elctricity[0];
            return buttry;
        }

        public void DroneToCharge(int drone,int base_)
        {
            try
            {
                dalObj.Drone_To_charge(drone, base_);
            }
            catch(IDAL.DO.Item_not_found_exception ex)
            {
                throw (new Item_not_found_exeption(ex));
            }
        }
    }
}
