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
        /// <summary>
        /// ctor
        /// </summary>
        public BL()
        {
            Random random = new Random();
            dalObj = new DalObject.DalObject();
            foreach (IDAL.DO.Drone drone in dalObj.Drone_list())
            {
                //data from dorn list in data source
                Drone new_drone = new Drone { SerialNum = drone.serialNumber };
                new_drone.weightCategory = (Weight_categories)drone.weightCategory;
                new_drone.Model = drone.Model;

                //chcking for pacege connection to this drone
                IDAL.DO.Package package = new IDAL.DO.Package();
                foreach (IDAL.DO.Package chcking_packege in dalObj.Packages_with_drone())
                {
                    if (chcking_packege.operator_skimmer_ID == drone.serialNumber)
                    {

                        //cheak if drone in middel  trnsfer
                        if (chcking_packege.package_arrived != new DateTime())
                        {
                            new_drone.packageInTransfer = chcking_packege.serialNumber;
                            package = chcking_packege;
                            new_drone.droneStatus = Drone_status.Work;
                            break;
                        }
                    }
                }
                if (new_drone.droneStatus == Drone_status.Work)
                {

                    double min_butrry;

                    //drone need to collect shipment
                    if (package.collect_package_for_shipment != new DateTime())
                    {
                        new_drone.location = Client_location(package.sendClient);
                        min_butrry = buttryDownPackegeDelivery(package.serialNumber) +
                           buttryDownWithNoPackege(Clloset_base(package.getingClient), Client_location(package.getingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);
                    }
                    //drone need to dliver shipment
                    else if (package.package_association != new DateTime())
                    {

                        new_drone.location = Clloset_base(package.sendClient);
                        min_butrry = buttryDownWithNoPackege(new_drone.location, Client_location(package.sendClient)) +
                            buttryDownPackegeDelivery(package.serialNumber) +
                            buttryDownWithNoPackege(Clloset_base(package.getingClient), Client_location(package.getingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);

                    }



                }
                else
                {
                    new_drone.droneStatus = (Drone_status)random.Next(2);
                    //dorne lottery in mainteance
                    if (new_drone.droneStatus == Drone_status.Maintenance)
                    {
                        new_drone.butrryStatus = random.Next(21);
                        int i = random.Next(2);
                        foreach (IDAL.DO.Base_Station base_ in dalObj.Base_station_list())
                        {
                            if (i == 0)
                            {
                                new_drone.packageInTransfer = 0;
                                new_drone.location = Base_location(base_.baseNumber);
                                break;
                            }
                            i--;
                        }
                    }
                    //drone lottery in free
                    if (new_drone.droneStatus == Drone_status.Free)
                    {
                        //lottry drone what is lost shipment
                        int i = random.Next(10);
                        int j = 0;
                        for (; j != i;)

                            foreach (IDAL.DO.Package package1 in dalObj.Packages_arrive_list())
                            {
                                if (j == i)
                                {
                                    new_drone.packageInTransfer = package1.serialNumber;
                                    new_drone.location = Client_location(package1.getingClient);
                                    random.Next((int)buttryDownWithNoPackege(new_drone.location, Clloset_base(package1.getingClient)) + 1, 100);
                                    break;
                                }
                                else
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

        






    }
}
