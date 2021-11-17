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
                    SendClient =package.SendClient.Id,
                    GetingClient =package.RecivedClient.Id,
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
            finalpackeg.drone = drone;
            finalpackeg.package_association = DateTime.Now;
            UpdatePackegInDal(finalpackeg);

        }

        double batteryCalculationForFullShipping(Location drone, Package package)
        {
            return buttryDownWithNoPackege(drone, ClientLocation(package.SendClient.Id)) + buttryDownPackegeDelivery(package.SerialNumber) +
                buttryDownWithNoPackege(drone, ClientLocation(package.RecivedClient.Id));
        }

        public void UpdatePackegInDal(Package package)
        {
            dalObj.UpdatePackege(new IDAL.DO.Package
            {
                SerialNumber = package.SerialNumber,
                SendClient = package.SendClient.Id,
                CollectPackageForShipment = package.collect_package,
                ReceivingDelivery = package.create_package,
                OperatorSkimmerId = package.drone.SerialNum,
                PackageArrived = package.package_arrived,
                PackageAssociation = package.package_association,
                Priority = (IDAL.DO.Priority)package.priority,
                GetingClient = package.RecivedClient.Id,
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


                foreach(var packege1 in packages)
                { 
                    uint sendig = packege1.SendClient;
                    Location location2 = ClientLocation(sendig);
                    if (Distans(location, location1) > Distans(location, location2))
                    {
                        location1 = location2;
                        package = convertToPackegeBl(packege1);
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
                    SendClient = clientInPackageFromDal(dataPackege.SendClient),
                    collect_package = dataPackege.CollectPackageForShipment,
                    create_package = dataPackege.ReceivingDelivery,
                    drone = SpecificDrone( dataPackege.OperatorSkimmerId),
                    package_arrived = dataPackege.PackageArrived,
                    package_association = dataPackege.PackageAssociation,
                    priority = (Priority)dataPackege.Priority,
                    RecivedClient = clientInPackageFromDal( dataPackege.GetingClient),
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
                SendClient = clientInPackageFromDal(dataPackege.SendClient),
                collect_package = dataPackege.CollectPackageForShipment,
                create_package = dataPackege.ReceivingDelivery,
                drone =SpecificDrone( dataPackege.OperatorSkimmerId),
                package_arrived = dataPackege.PackageArrived,
                package_association = dataPackege.PackageAssociation,
                priority = (Priority)dataPackege.Priority,
                RecivedClient = clientInPackageFromDal(dataPackege.GetingClient),
                weightCatgory = (WeightCategories)dataPackege.WeightCatgory
            };
        }

        //drone start delivery
        public void CollectPackegForDelivery(uint droneNumber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNum == droneNumber);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            var pacege = ShowPackage(droneNumber);
            if (pacege.collect_package != new DateTime())
            { new FunctionErrorException("ShowPackage||AddPackege"); }

            Location location = ClientLocation(pacege.SendClient.Id);
            drone.butrryStatus -= buttryDownWithNoPackege(drone.location, location);

            if (drone.butrryStatus < 0)
            { new FunctionErrorException("BatteryCalculationForFullShipping"); }
            
            drone.location = location;
            
            dalObj.PackageCollected(pacege.SerialNumber);
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
            drone.location = ClientLocation(packege.RecivedClient.Id);
            drone.droneStatus = DroneStatus.Free;
            drone.packageInTransfer = null;
            packege.package_arrived = DateTime.Now;
            UpdatePackegInDal(packege);
            dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNum == droneNumber)] = drone;

        }

        public IEnumerable<PackageToList> PackageToLists()
        {
            if (dalObj.PackegeList().ToList().Count() == 0)
                throw new TheListIsEmptyException();
            List<PackageToList> packageToLists = new List<PackageToList>();
            PackageStatus packageStatus;
            foreach (var packege in dalObj.PackegeList())
            {
                if (packege.PackageArrived != new DateTime())
                    packageStatus = PackageStatus.Arrived;
                else if (packege.CollectPackageForShipment != new DateTime())
                    packageStatus = PackageStatus.Collected;
                else if (packege.PackageAssociation != new DateTime())
                    packageStatus = PackageStatus.Assign;
                else
                    packageStatus = PackageStatus.Create;
                packageToLists.Add(new PackageToList
                {
                    packageStatus = packageStatus,
                    RecivedClient = dalObj.CilentByNumber(packege.GetingClient).Name,
                    SendClient = dalObj.CilentByNumber(packege.SendClient).Name,
                    SerialNumber = packege.SerialNumber,
                    priority = (Priority)packege.Priority,
                    WeightCategories = (WeightCategories)packege.WeightCatgory


                });

            }
            return packageToLists;
        }

        public IEnumerable<PackageToList> PackageWithNoDroneToLists()
        {
            if (dalObj.PackegeListWithNoDrone().ToList().Count() == 0)
                throw new TheListIsEmptyException();
            List<PackageToList> packageToLists = new List<PackageToList>();
            PackageStatus packageStatus;
            foreach (var packege in dalObj.PackegeListWithNoDrone())
            {

                packageStatus = PackageStatus.Create;
                packageToLists.Add(new PackageToList
                {
                    packageStatus = packageStatus,
                    RecivedClient = dalObj.CilentByNumber(packege.GetingClient).Name,
                    SendClient = dalObj.CilentByNumber(packege.SendClient).Name,
                    SerialNumber = packege.SerialNumber,
                    priority = (Priority)packege.Priority,
                    WeightCategories = (WeightCategories)packege.WeightCatgory


                });

            }
            return packageToLists;
        }

    }
}
