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
        List<DroneToList> dronesListInBl = new List<DroneToList>();

        /// <summary>
        /// ctor
        /// </summary>
        public BL()
        {
            Random random = new Random();
            dalObj = new DalObject.DalObject();

            //Copy the drone list from the data layer to the logic layer
            foreach (IDAL.DO.Drone drone in dalObj.DroneList())
            {
                dronesListInBl.Add(droneFromDal(drone));
            }

            for (int i = 0; i < dronesListInBl.Count; i++)
            {
                //data from drone list in data source
                DroneToList new_drone = dronesListInBl[i];


                //Checking for packege connected to this drone

                IDAL.DO.Package package = new IDAL.DO.Package();
                foreach (IDAL.DO.Package chcking_packege in dalObj.PackagesWithDrone())
                {
                    //Check if the package is associated with this drone
                    if (chcking_packege.OperatorSkimmerId == new_drone.SerialNumber)
                    {

                        //cheak if the drone has not get to its destination yet
                        if (chcking_packege.PackageArrived != new DateTime())
                        {
                            new_drone.numPackage = chcking_packege.SerialNumber;
                            package = chcking_packege;
                            new_drone.droneStatus = DroneStatus.Work;
                            break;
                        }
                    }
                }

                if (new_drone.droneStatus == DroneStatus.Work)
                {

                    double min_butrry;
                    var packegeInTransferObject = convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(new_drone.numPackage));
                    //If the package has not been collected yet
                    //The location of the drone will be at the sender location
                    if (package.CollectPackageForShipment != new DateTime())
                    {
                        new_drone.location = ClientLocation(package.SendClient);
                        new_drone.numPackage = package.SerialNumber;
                        min_butrry = buttryDownPackegeDelivery(packegeInTransferObject) +
                           buttryDownWithNoPackege(ClosestBase(ClientLocation(package.GetingClient)).location, ClientLocation(package.GetingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);
                    }

                    //If the package was associated but not collected the location
                    //Of the drone will be at the station closest to the sender
                    else if (package.PackageAssociation != new DateTime())
                    {

                        new_drone.location = ClosestBase(ClientLocation(package.SendClient)).location;
                        min_butrry = buttryDownWithNoPackege(new_drone.location, ClientLocation(package.SendClient)) +
                            buttryDownPackegeDelivery(packegeInTransferObject) +
                            buttryDownWithNoPackege(ClosestBase(ClientLocation(package.GetingClient)).location, ClientLocation(package.GetingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);

                    }

                }

                //If the drone doesn't in delivery
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
                                dalObj.DroneToCharge(new_drone.SerialNumber, base_.baseNumber);
                                new_drone.numPackage = 0;
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
                        int k = random.Next(10);
                        int j = 0;
                        bool flag = false;
                        while (!flag)
                            foreach (IDAL.DO.Package package1 in dalObj.PackagesArriveList())
                            {
                                if (j == k)
                                {
                                    new_drone.numPackage = package1.SerialNumber;
                                    new_drone.location = ClientLocation(package1.GetingClient);
                                  new_drone.butrryStatus=  random.Next((int)buttryDownWithNoPackege(new_drone.location, ClosestBase(ClientLocation(package1.GetingClient)).location) + 1, 100);
                                    flag = true;
                                    break;
                                }
                                j++;

                            }

                    }
                    for (int p = 0; p < dronesListInBl.Count; p++)
                    {
                        if (dronesListInBl[p].SerialNumber == new_drone.SerialNumber)
                            dronesListInBl[p] = new_drone;
                    }

                }

            }

        }
        /// <summary>
        /// calculate distance between two points
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public double Distans(Location location1, Location location2)
        {
            return dalObj.Distance(location1.Longitude, location1.Latitude, location2.Longitude, location2.Latitude);
        }

        /// <summary>
        /// Returns a point in degree form
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public string PointToDegree(double point)
        {
            return dalObj.PointToDegree(point);
        }

    }
}
