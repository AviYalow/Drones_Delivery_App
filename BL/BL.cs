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
            Random random = new Random();
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
                if (new_drone.droneStatus == Drone_status.Work)
                {
                    
                    double min_butrry;
                    if (package.package_association != new DateTime())
                    {

                        new_drone.location = Clloset_base(package.sendClient);
                        min_butrry = ButtryDownWithNoPackege(new_drone.location, Client_location(package.sendClient)) +
                            ButtryDownPackegeDelivery(package.serialNumber) +
                            ButtryDownWithNoPackege(Clloset_base(package.getingClient), Client_location(package.getingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);

                    }
                    else if (package.collect_package_for_shipment != new DateTime())
                    {
                        new_drone.location = Client_location(package.sendClient);
                        min_butrry = ButtryDownPackegeDelivery(package.serialNumber) +
                           ButtryDownWithNoPackege(Clloset_base(package.getingClient), Client_location(package.getingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);
                    }


                }
                else
                {
                    new_drone.droneStatus = (Drone_status)random.Next(2);
                    if (new_drone.droneStatus ==Drone_status.Maintenance)
                    {
                        new_drone.butrryStatus = random.Next(21);
                        int i = random.Next(2);
                        foreach(IDAL.DO.Base_Station base_ in dalObj.Base_station_list())
                        {
                            if(i==0)
                            {
                                new_drone.location = Base_location(base_.baseNumber);
                                break;
                            }
                            i--;
                        }
                    }
                    if (new_drone.droneStatus == Drone_status.Free)
                    {
                       // new_drone.butrryStatus = random.Next(21);
                        int i = random.Next(10);
                        int j = 0;
                        foreach (IDAL.DO.Package package1 in dalObj.Packages_arrive_list() )
                        {
                            if (j == i)
                            {
                                new_drone.location =Client_location( package1.getingClient) ;
                                random.Next((int)ButtryDownWithNoPackege(new_drone.location, Clloset_base(package1.getingClient))+1, 100);
                                break;
                            }
                            j++;
                        }
                    }

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
        public Location Base_location(int base_number)
        {
            Location base_location = new Location();
            IDAL.DO.Base_Station base_Station = dalObj.Base_station_by_number(base_number);
            base_location.Latitude = base_Station.latitude;
            base_location.Longitude = base_Station.longitude;
            return base_location;
        }


        public double ButtryDownPackegeDelivery(int packegeNumber)
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
        public double ButtryDownWithNoPackege(Location fromLocation, Location toLocation)
        {
            double[] elctricity = dalObj.Elctrtricity();
            double distans = Distans(fromLocation, toLocation);
            double buttry = ((distans / (double)Speed_drone.Free) / (double)butrryPer__.Minute) * elctricity[0];
            return buttry;
        }

    }
}
