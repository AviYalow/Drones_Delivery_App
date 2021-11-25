using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// add packege
        /// </summary>
        /// <param name="package"> packege to add</param>
        /// <returns> serial number of the packege</returns>
        public uint AddPackege(Package package)
        {
            uint packegeNum = 0;
            Location locationsend = ClientLocation(package.SendClient.Id);
            Location locationGet = ClientLocation(package.RecivedClient.Id);
            var butrryWithDelvery = buttryDownPackegeDelivery(convertPackegeBlToPackegeInTrnansfer(package));
            var butrryFree = buttryDownWithNoPackege(ClosestBase(locationsend).location, locationsend) + buttryDownWithNoPackege(ClosestBase(locationGet).location, locationGet);
            if (butrryWithDelvery + butrryFree > 100)
                throw new MoreDistasThenMaximomException(package.SendClient.Id, package.RecivedClient.Id);


            try
            {

                packegeNum = dalObj.AddPackage(new IDAL.DO.Package
                {
                    SendClient = package.SendClient.Id,
                    GetingClient = package.RecivedClient.Id,
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

        /// <summary>
        /// Calculate how much percentage of battery the drone will need for full shipping
        /// </summary>
        /// <param name="drone"> drone</param>
        /// <param name="package"> package</param>
        /// <returns> percentage of battery </returns>

        double batteryCalculationForFullShipping(Location drone, Package package)
        {
            return buttryDownWithNoPackege(drone, ClientLocation(package.SendClient.Id)) + buttryDownPackegeDelivery(convertPackegeBlToPackegeInTrnansfer(package)) +
                buttryDownWithNoPackege(ClosestBase(ClientLocation(package.RecivedClient.Id)).location, ClientLocation(package.RecivedClient.Id));
        }

        /// <summary>
        /// Updating fields of a particular package in the data layer
        /// </summary>
        /// <param name="package"> particular package</param>
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


        /// <summary>
        /// Calculate the closest package to a given location
        /// </summary>
        /// <param name="location">given location</param>
        /// <param name="packages"> a list of package</param>
        /// <returns> the closest package to the location</returns>
        Package cloosetPackege(Location location, IEnumerable<IDAL.DO.Package> packages)
        {
            Package package = new Package();
            if (packages is null)
                throw new TheListIsEmptyException();
            {
                Location location1 = ClientLocation(packages.First().SendClient);


                foreach (var packege1 in packages)
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

        /// <summary>
        ///  view a package
        /// </summary>
        /// <param name="number">serial number of package</param>
        /// <returns> package in the logical layer</returns>
        public Package ShowPackage(uint number)
        {

            try
            {
                var dataPackege = dalObj.packegeByNumber(number);
                return convertPackegeDalToBl(dataPackege);
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }

        }

        /// <summary>
        /// convert packege from the data layer to the logical layer
        /// </summary>
        /// <param name="dataPackege"> package in the data layer </param>
        /// <returns>  package in the logical layer</returns>
        Package convertPackegeDalToBl(IDAL.DO.Package dataPackege)
        {
            return new Package
            {
                SerialNumber = dataPackege.SerialNumber,
                SendClient = clientInPackageFromDal(dataPackege.SendClient),
                collect_package = dataPackege.CollectPackageForShipment,
                create_package = dataPackege.ReceivingDelivery,
                drone = dataPackege.OperatorSkimmerId != 0 ? droneToDroneInPackage(dataPackege.OperatorSkimmerId) : null,
                package_arrived = dataPackege.PackageArrived,
                package_association = dataPackege.PackageAssociation,
                priority = (Priority)dataPackege.Priority,
                RecivedClient = clientInPackageFromDal(dataPackege.GetingClient),
                weightCatgory = (WeightCategories)dataPackege.WeightCatgory
            };

        }

        /// <summary>
        /// convert packege from the data layer to the logical layer
        /// </summary>
        /// <param name="dataPackege"> package in the data layer </param>
        /// <returns>  package in the logical layer</returns>
        Package convertToPackegeBl(IDAL.DO.Package dataPackege)
        {
            return new Package
            {
                SerialNumber = dataPackege.SerialNumber,
                SendClient = clientInPackageFromDal(dataPackege.SendClient),
                collect_package = dataPackege.CollectPackageForShipment,
                create_package = dataPackege.ReceivingDelivery,
                drone = droneToDroneInPackage(dataPackege.OperatorSkimmerId),
                package_arrived = dataPackege.PackageArrived,
                package_association = dataPackege.PackageAssociation,
                priority = (Priority)dataPackege.Priority,
                RecivedClient = clientInPackageFromDal(dataPackege.GetingClient),
                weightCatgory = (WeightCategories)dataPackege.WeightCatgory
            };
        }

        /// <summary>
        /// list of packages
        /// </summary>
        /// <returns>list of packages</returns>

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

        /// <summary>
        /// list of packages that don't have a drone
        /// </summary>
        /// <returns> list of packages that don't have a drone</returns>
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
        /// <summary>
        /// delete packege 
        /// </summary>
        /// <param name="number"> serial nummber of package</param>
        public void DeletePackege(uint number)
        {
            try
            {
                var packege = dalObj.packegeByNumber(number);
                //cheack the packege not send yet
                if (packege.CollectPackageForShipment != new DateTime())
                { throw new ThePackegeAlredySendException(); }
                if (packege.OperatorSkimmerId != 0)
                {
                    var drone = SpecificDrone(packege.OperatorSkimmerId);
                    drone.droneStatus = DroneStatus.Free;
                    drone.numPackage = 0;

                    for (int i = 0; i < dronesListInBl.Count; i++)
                    {
                        if (dronesListInBl[i].SerialNumber == drone.SerialNumber)
                            dronesListInBl[i] = drone;
                    }
                }
                dalObj.DeletePackege(number);
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
        }
    }
}
