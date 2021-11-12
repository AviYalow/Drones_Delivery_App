using System;
using System.Collections;
using System.Collections.Generic;
using IDAL;
using IBL;
using IBL.BO;

namespace IBL
{
    partial class BL : IBL
    {
        public IDal dalObj;
        public List<Drone> drones = new List<Drone>();

        public BL()
        {
            dalObj = new DalObject.DalObject();
            foreach (IDAL.DO.Drone drone in dalObj.Drone_list())
            {

                Drone new_drone = new Drone { SerialNum = drone.serialNumber };

                new_drone.weightCategory = (Weight_categories)drone.weightCategory;
                new_drone.Model = drone.Model;

                //new_drone.  butrryStatus=

                //    IDAL.DO.Base_Station base_ = dalObj.Base_station_by_number(drone.base_station);
                IDAL.DO.Package package = new IDAL.DO.Package();
                foreach (IDAL.DO.Package chcking_packege in dalObj.Packages_with_drone())
                {
                    if (chcking_packege.operator_skimmer_ID == drone.serialNumber)
                    {
                        if (chcking_packege.package_arrived != new DateTime())
                        {
                            package = chcking_packege;
                            new_drone.droneStatus = Drone_status.Work;
                            break;
                        }
                    }
                }
                if (package.package_association != new DateTime())
                {
                    new_drone.location = Clloset_base(package.sendClient);

                }
                else if (package.collect_package_for_shipment != new DateTime())
                {
                    new_drone.location = Client_location(package.sendClient);
                }

                drones.Add(new_drone);





            }
        }
        /// <summary>
        /// cacolete distand betwen two points
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public double Distans(Location location1, Location location2)
        {
            return dalObj.Distance(location1.Longitude, location1.Latitude, location2.Longitude, location2.Latitude);
        }

        /// <summary>
        /// clculet the most collset base to the cilent
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Location Clloset_base(int client)
        {
            Location client_location = Client_location(client);
            Location base_location = new Location();
            Location new_location = new Location();
            double distans = 0, distans2;
            foreach (IDAL.DO.Base_Station base_ in dalObj.Base_station_list())
            {
                base_location.Latitude = base_.latitude;
                base_location.Longitude = base_.longitude;
                distans2 = Distans(client_location, base_location);
                if (distans < distans2 || distans == 0)
                {
                    distans = distans2;
                    new_location = base_location;
                }
            }
            return new_location;
        }
        /// <summary>
        /// poll out the client location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Location Client_location(int id)
        {
            IDAL.DO.Client client = dalObj.cilent_by_number(id);
            Location location_client = new Location();
            location_client.Latitude = client.Latitude;
            location_client.Longitude = client.Longitude;
            return location_client;
        }


        public double ButtryDownWithPackege(Drone drone, int id)
        {
            double new_buttry = 0;
            Location location_client = new Location();
            IDAL.DO.Client client = dalObj.cilent_by_number(id);
            location_client.Latitude = client.Latitude;
            location_client.Longitude = client.Longitude;
            double distans = Distans(drone.location, location_client);
            if (drone.droneStatus == Drone_status.Work)
                switch (drone.weightCategory)//להשלים פונקציה ע"י חישוב לכל משקל
                {
                    case Weight_categories.Easy:


                    default:
                        break;
                }


            return new_buttry;
        }

    }
}
