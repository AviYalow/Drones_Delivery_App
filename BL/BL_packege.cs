using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL : IBL
    {/// <summary>
     /// add packege to list in data source 
     /// </summary>
     /// <param name="package"></param>
     /// <returns></returns>
        public uint AddPackege(Package package)
        {
            uint packegeNum = 0;
            try
            {
                packegeNum = dalObj.AddPackage(new IDAL.DO.Package
                {
                    SendClient = dalObj.CilentByNumber(package.SendClient).Id,
                    GetingClient = dalObj.CilentByNumber(package.RecivedClient).Id,
                    Priority = (IDAL.DO.Priority)package.priority,
                    WeightCatgory = (IDAL.DO.WeightCategories)package.weightCatgory,
                    PackageArrived = package.create_package

                });
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }



            return packegeNum;

        }

        public void ConnctionPackegeToDrone(uint droneNumber)//אולי כדאי לעבור למיון רשימה ואח"כ לסינון?
        {
            var drone = dronesListInBl.Find(x => x.SerialNum == droneNumber);
            if (drone == null)
            { throw new ItemNotFoundException("Drone", droneNumber); }
            if (drone.droneStatus != DroneStatus.Free)
            { throw new DroneCantMakeDliveryException(); }
            IEnumerable<IDAL.DO.Package> packege, temp;
            //locking for drone in first priorty 
            packege = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.Priority == IDAL.DO.Priority.Immediate);
            if (packege == null)
                packege = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.Priority == IDAL.DO.Priority.quick);
            if (packege == null)
                packege = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.Priority == IDAL.DO.Priority.Regular);
            //locking for packeges the drone can delivery
            switch (drone.weightCategory)
            {
                #region
                case WeightCategories.Heavy:
                    temp = packege.ToList().FindAll(x => x.WeightCatgory == IDAL.DO.WeightCategories.Heavy);
                    if (temp == null)
                        temp = packege.ToList().FindAll(x => x.WeightCatgory == IDAL.DO.WeightCategories.Medium);
                    if (temp == null)
                        temp = packege.ToList().FindAll(x => x.WeightCatgory == IDAL.DO.WeightCategories.Easy);
                    packege = temp;
                    break;
                case WeightCategories.Medium:
                    temp = packege.ToList().FindAll(x => x.WeightCatgory == IDAL.DO.WeightCategories.Medium);
                    if (temp == null)
                        temp = packege.ToList().FindAll(x => x.WeightCatgory == IDAL.DO.WeightCategories.Easy);
                    packege = temp;
                    break;
                case WeightCategories.Easy:
                    packege = packege.ToList().FindAll(x => x.WeightCatgory == IDAL.DO.WeightCategories.Easy);
                    break;
                default:
                    break;
                    #endregion
            }
            //locking for the most closest package
            var finalpackeg = cloosetPackege(drone.location, packege);
            //cheking if the buttry enough for a dlivery
            var buttry = batteryCalculationForFullShipping(drone.location, finalpackeg);
                
            if (drone.butrryStatus - buttry <= 0)
                throw new NoButrryToTripException(buttry);
            //update number packege in drone
            drone.packageInTransfer = ShowPackage(finalpackeg.SerialNumber);
            drone.droneStatus = DroneStatus.Work;
            //update the packege
            finalpackeg.drone = drone.SerialNum;
            finalpackeg.package_association = DateTime.Now;
            updatePackegInDal(finalpackeg);

        }
        double batteryCalculationForFullShipping(Location drone,Package package)
        {
            return buttryDownWithNoPackege(drone, ClientLocation(package.SendClient)) + buttryDownPackegeDelivery(package.SerialNumber) +
                buttryDownWithNoPackege(drone, ClientLocation(package.RecivedClient));
        }

        public void updatePackegInDal(Package package)
        {
            dalObj.UpdatePackege(new IDAL.DO.Package
            {
                SerialNumber = package.SerialNumber,
                SendClient = package.SendClient,
                CollectPackageForShipment = package.collect_package,
                ReceivingDelivery = package.create_package,
                OperatorSkimmerId = package.drone,
                PackageArrived = package.package_arrived,
                PackageAssociation = package.package_association,
                Priority = (IDAL.DO.Priority)package.priority,
                GetingClient = package.RecivedClient,
                WeightCatgory = (IDAL.DO.WeightCategories)package.weightCatgory
            });
        }
        Package cloosetPackege(Location location, IEnumerable<IDAL.DO.Package> packages = null)
        {
            Package package = new Package();
            if (packages == null)
                throw new TheListIsEmptyException();
            {
                Location location1 = ClientLocation(packages.ToList()[0].SendClient);


                for (int i = 1; i < packages.ToList().Count(); i++)
                {
                    uint sendig = packages.ToList()[i].SendClient;
                    Location location2 = ClientLocation(sendig);
                    if (Distans(location, location1) > Distans(location, location2))
                    {
                        location1 = location2;
                        package.SendClient = sendig;
                    }

                }

            }
            return package;

        }
        public Package ShowPackage(uint number)
        {
            Package package;
            try
            {
                var dataPackege = dalObj.packegeByNumber(number);
                package = new Package
                {
                    SerialNumber = dataPackege.SerialNumber,
                    SendClient = dataPackege.SendClient,
                    collect_package = dataPackege.CollectPackageForShipment,
                    create_package = dataPackege.ReceivingDelivery,
                    drone = dataPackege.OperatorSkimmerId,
                    package_arrived = dataPackege.PackageArrived,
                    package_association = dataPackege.PackageAssociation,
                    priority = (Priority)dataPackege.Priority,
                    RecivedClient = dataPackege.GetingClient,
                    weightCatgory = (WeightCategories)dataPackege.WeightCatgory
                };
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
            return package;
        }
        Package convertToPackegeBl(IDAL.DO.Package dataPackege)
        {
            return new Package
            {
                SerialNumber = dataPackege.SerialNumber,
                SendClient = dataPackege.SendClient,
                collect_package = dataPackege.CollectPackageForShipment,
                create_package = dataPackege.ReceivingDelivery,
                drone = dataPackege.OperatorSkimmerId,
                package_arrived = dataPackege.PackageArrived,
                package_association = dataPackege.PackageAssociation,
                priority = (Priority)dataPackege.Priority,
                RecivedClient = dataPackege.GetingClient,
                weightCatgory = (WeightCategories)dataPackege.WeightCatgory
            };
        }
        //drone start delivery
        public void CollectPackegForDelivery(uint droneNumber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNum == droneNumber);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            var pacege =ShowPackage(droneNumber);
            if (pacege.collect_package != new DateTime())
            { new FunctionErrorException("ShowPackage||AddPackege"); }

            Location location = ClientLocation(pacege.SendClient);
            drone.butrryStatus -= buttryDownWithNoPackege(drone.location,location );

            if (drone.butrryStatus<0)
            { new FunctionErrorException("BatteryCalculationForFullShipping"); }

            drone.location = location;
            pacege.collect_package = DateTime.Now;
            updatePackegInDal(pacege);
            dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNum == droneNumber)] = drone;


        }

        public void PackegArrive(uint droneNumber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNum == droneNumber);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            var packege = drone.packageInTransfer;
            if (packege.package_arrived != new DateTime())
                throw new FunctionErrorException("AddPackege||ShowPackage||updatePackegInDal||PackegArrive");
            drone.butrryStatus -= buttryDownPackegeDelivery(packege.SerialNumber);
            if (drone.butrryStatus < 0)
                throw new FunctionErrorException("batteryCalculationForFullShipping");
            drone.location = ClientLocation(packege.RecivedClient);
            drone.droneStatus = DroneStatus.Free;
            drone.packageInTransfer = null;
            packege.package_arrived = DateTime.Now;
            updatePackegInDal(packege);
            dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNum == droneNumber)] = drone;

        }
    }
}
