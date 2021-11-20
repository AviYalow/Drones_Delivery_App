using System;
using System.Collections;
using System.Collections.Generic;
using IDAL;
using IBL;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        public IDal dalObj;
        List<Drone> dronesListInBl = new List<Drone>();
        /// <summary>
        /// ctor
        /// </summary>
        public BL()
        {
            Random random = new Random();
            dalObj = new DalObject.DalObject();
            foreach (IDAL.DO.Drone drone in dalObj.DroneList())
            {
                dronesListInBl.Add(droneFromDal(drone));
            }
            for (int i = 0; i < dronesListInBl.Count; i++)
            {
                //data from dorn list in data source
                Drone new_drone = dronesListInBl[i];


                //chcking for pacege connection to this drone
                IDAL.DO.Package package = new IDAL.DO.Package();
                foreach (IDAL.DO.Package chcking_packege in dalObj.PackagesWithDrone())
                {
                    if (chcking_packege.OperatorSkimmerId == new_drone.SerialNum)
                    {

                        //cheak if drone in middel  trnsfer
                        if (chcking_packege.PackageArrived != new DateTime())
                        {
                            new_drone.packageInTransfer = convertPackegeDalToPackegeInTrnansfer(chcking_packege);
                            package = chcking_packege;
                            new_drone.droneStatus = DroneStatus.Work;
                            break;
                        }
                    }
                }
                if (new_drone.droneStatus == DroneStatus.Work)
                {

                    double min_butrry;

                    //drone need to collect shipment
                    if (package.CollectPackageForShipment != new DateTime())
                    {
                        new_drone.location = ClientLocation(package.SendClient);
                        new_drone.packageInTransfer = convertPackegeDalToPackegeInTrnansfer(package);
                        min_butrry = buttryDownPackegeDelivery(new_drone.packageInTransfer) +
                           buttryDownWithNoPackege(ClosestBase(ClientLocation(package.GetingClient)).location, ClientLocation(package.GetingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);
                    }
                    //drone need to dliver shipment
                    else if (package.PackageAssociation != new DateTime())
                    {

                        new_drone.location = ClosestBase(ClientLocation(package.SendClient)).location;
                        min_butrry = buttryDownWithNoPackege(new_drone.location, ClientLocation(package.SendClient)) +
                            buttryDownPackegeDelivery(new_drone.packageInTransfer) +
                            buttryDownWithNoPackege(ClosestBase(ClientLocation(package.GetingClient)).location, ClientLocation(package.GetingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);

                    }



                }
                else
                {
                    new_drone.droneStatus = (DroneStatus)random.Next(2);
                    //dorne lottery in mainteance
                    if (new_drone.droneStatus == DroneStatus.Maintenance)
                    {
                        new_drone.butrryStatus = random.Next(21);
                        int k = random.Next(2);


                        foreach (IDAL.DO.Base_Station base_ in dalObj.BaseStationList())
                        {
                            if (k == 0)
                            {
                                var updateBase = base_;
                                updateBase.NumberOfChargingStations--;
                                dalObj.UpdateBase(updateBase);
                                new_drone.packageInTransfer = null;
                                new_drone.location = BaseLocation(base_.baseNumber);
                                break;

                            }
                            k--;

                        }
                    }
                    //drone lottery in free
                    else if (new_drone.droneStatus == DroneStatus.Free)
                    {
                        //lottry drone what is lost shipment
                        //int k = random.Next(10);
                        int k = 9;
                        int j = 0;
                        bool flag = false;
                       while(!flag)
                            foreach (IDAL.DO.Package package1 in dalObj.PackagesArriveList())
                            {
                                if (j == k)
                                {
                                    new_drone.packageInTransfer = convertPackegeDalToPackegeInTrnansfer(package1);
                                    new_drone.location = ClientLocation(package1.GetingClient);
                                    random.Next((int)buttryDownWithNoPackege(new_drone.location, ClosestBase(ClientLocation(package1.GetingClient)).location) + 1, 100);
                                    flag = true;
                                    break;
                                }
                                j++;

                            }
                        
                    }
                    for (int p = 0; p < dronesListInBl.Count; p++)
                    {
                        if (dronesListInBl[p].SerialNum == new_drone.SerialNum)
                            dronesListInBl[p] = new_drone;
                    }


                  


                }

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

        public string PointToDegree(double point)
        {
            return dalObj.PointToDegree(point);
        }

    }
}
