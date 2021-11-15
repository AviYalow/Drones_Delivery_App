using System;
using System.Collections;
using System.Collections.Generic;
using IDAL;
using IBL;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
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
            foreach (IDAL.DO.Drone drone in dalObj.DroneList())
            {
                //data from dorn list in data source
                Drone new_drone = new Drone { SerialNum = drone.SerialNumber };
                new_drone.weightCategory = (Weight_categories)drone.WeightCategory;
                new_drone.Model = drone.Model;

                //chcking for pacege connection to this drone
                IDAL.DO.Package package = new IDAL.DO.Package();
                foreach (IDAL.DO.Package chcking_packege in dalObj.PackagesWithDrone())
                {
                    if (chcking_packege.OperatorSkimmerId == drone.SerialNumber)
                    {

                        //cheak if drone in middel  trnsfer
                        if (chcking_packege.PackageArrived != new DateTime())
                        {
                            new_drone.packageInTransfer = chcking_packege.SerialNumber;
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
                    if (package.CollectPackageForShipment != new DateTime())
                    {
                        new_drone.location = Client_location(package.SendClient);
                        min_butrry = buttryDownPackegeDelivery(package.SerialNumber) +
                           buttryDownWithNoPackege(Clloset_base(package.GetingClient), Client_location(package.GetingClient));
                        new_drone.butrryStatus = random.Next((int)min_butrry + 1, 100);
                    }
                    //drone need to dliver shipment
                    else if (package.PackageAssociation != new DateTime())
                    {

                        new_drone.location = Clloset_base(package.SendClient);
                        min_butrry = buttryDownWithNoPackege(new_drone.location, Client_location(package.SendClient)) +
                            buttryDownPackegeDelivery(package.SerialNumber) +
                            buttryDownWithNoPackege(Clloset_base(package.GetingClient), Client_location(package.GetingClient));
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
                        foreach (IDAL.DO.Base_Station base_ in dalObj.BaseStationList())
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

                            foreach (IDAL.DO.Package package1 in dalObj.PackagesArriveList())
                            {
                                if (j == i)
                                {
                                    new_drone.packageInTransfer = package1.SerialNumber;
                                    new_drone.location = Client_location(package1.GetingClient);
                                    random.Next((int)buttryDownWithNoPackege(new_drone.location, Clloset_base(package1.GetingClient)) + 1, 100);
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

        public void AddClient(uint id, string name, string phone, double latitude, double londitude)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(uint siralNumber, string model, uint category)
        {
            throw new NotImplementedException();
        }

        public uint AddPackage(uint idsend, uint idget, uint kg, uint priorityByUser)
        {
            throw new NotImplementedException();
        }

        public void AddStation(uint base_num, string name, uint numOfCharging, double latitude, double longitude)
        {

            throw new NotImplementedException();
        }

        public void ConnectPackageToDrone(uint packageNumber, uint drone_sirial_number)
        {
            throw new NotImplementedException();
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

        public void DroneToCharge(uint serial)
        {
            throw new NotImplementedException();
        }

        public void Package_arrived(uint packageNumber)
        {
            throw new NotImplementedException();
        }

        public void Package_collected(uint packageNumber)
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromCharge(uint serial, uint timeInCharge)
        {
            throw new NotImplementedException();
        }
    }
}
